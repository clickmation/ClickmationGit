using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public static Board board;

    public int width;
    public int height;

    public int borderSize;

    public GameObject tilePrefab;
    public GameObject gamePiecePrefab;
    public PieceColor[] pieceColors;
    public PieceType[] pieceTypes;
    //public List<GamePiece> movingPieces = new List<GamePiece>();
    public List<GamePiece> clearingPieces = new List<GamePiece>();

    const float longTick = 0.5f;
    const float normTick = 0.25f;
    const float shortTick = 0.1f;
    float maxMovingTime;

    Tile[,] m_allTiles;
    GamePiece[,] m_allGamePieces;

    Tile m_clickedTile;
    Tile m_targetTile;

    ParticleManager m_particleManager;

    public GameState currentState;
    public GameState defaultState;
    public GameState movingState;

    const float hexOffset = 0.5f;

    public delegate float InterTypeMethod(float t);
    public InterTypeMethod interTypeMethod;

    public InterpType interpolation = InterpType.SmootherStep;

    public enum InterpType
    {
        Linear,
        EaseIn,
        EaseOut,
        SmoothStep,
        SmootherStep,
    };

    void Awake()
    {
        board = this;
    }

    void Start()
    {
        m_allTiles = new Tile[width, height];
        m_allGamePieces = new GamePiece[width, height];
        switch (interpolation)
        {
            case InterpType.Linear:
                interTypeMethod = new InterTypeMethod(Linear);
                break;
            case InterpType.EaseIn:
                interTypeMethod = new InterTypeMethod(EaseIn);
                break;
            case InterpType.EaseOut:
                interTypeMethod = new InterTypeMethod(EaseOut);
                break;
            case InterpType.SmoothStep:
                interTypeMethod = new InterTypeMethod(SmoothStep);
                break;
            case InterpType.SmootherStep:
                interTypeMethod = new InterTypeMethod(SmootherStep);
                break;
        }

        SetupTiles();
        SetupCamera();

        m_particleManager = GameObject.FindWithTag("ParticleManager").GetComponent<ParticleManager>();
        
        if (m_particleManager == null)
        {
            Debug.Log("Board    : Particle Manager Missing!!");
        }

        FillBoard(height + 2, longTick);
        StartCoroutine(StateSetCoroutine(longTick));
    }

    void SetupTiles()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j< height; j++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(i, j, 0) + HexOffset(i), Quaternion.identity) as GameObject;

                tile.name = "Tile (" + i + "," + j + ")";

                m_allTiles [i, j] = tile.GetComponent<Tile>();

                tile.transform.parent = transform;

                m_allTiles[i, j].Init(i, j, this);
            }
        }
    }

    int OffColumn(int x)
    {
        return (x % 2 > 0) ? 1 : -1;
    }

    float HexYOffset(int x)
    {
        return x % 2 * hexOffset;
    }

    Vector3 HexOffset(int x)
    {
        return new Vector3(0, HexYOffset(x), 0);
    }

    void SetupCamera()
    {
        Camera.main.transform.position = new Vector3((float)(width-1)/2f, (float)(height-1)/2f, -10f);

        float aspectRatio = (float)Screen.width / (float)Screen.height;

        float verticalSize = (float)height / 2f + (float)borderSize;

        float horizontalSize = ((float)width / 2f + (float)borderSize) / aspectRatio;

        Camera.main.orthographicSize = (verticalSize > horizontalSize) ? verticalSize: horizontalSize;
    }

    PieceColor GetRandomGamePiece(List<PieceColor> pool)
    {
        int randomIdx = Random.Range(0, pool.Count);

        if (pool[randomIdx] == null)
        {
            Debug.LogWarning("BOARD:    " + randomIdx + "does not contain a valid GamePiece prefab!");
        }

        return pool[randomIdx];
    }

    public void PlaceGamePiece(GamePiece gamePiece, int x, int y)
    {
        if (gamePiece == null)
        {
            Debug.LogWarning("BOARD:    Invalid GamePiece!");
            return;
        }

        gamePiece.transform.position = new Vector3(x, y, 0) + HexOffset(x);
        gamePiece.transform.rotation = Quaternion.identity;

        if (IsWithinBounds(x, y))
        {
            m_allGamePieces[x, y] = gamePiece;
        }
        gamePiece.SetCoord(x, y);
    }

    bool IsWithinBounds(int x, int y)
    {
        return (x >= 0 && x < width && y >= 0 && y < height);
    }

    GamePiece FillRandomAt(int x, int y, List<PieceColor> pool, int falseYOffset = 0, float moveTime = shortTick)
    {
        GameObject randomPiece = Instantiate(gamePiecePrefab, Vector3.zero, Quaternion.identity) as GameObject;

        if (randomPiece != null)
        {
            randomPiece.GetComponent<GamePiece>().Init(this, GetRandomGamePiece(pool), pieceTypes[0]);
            PlaceGamePiece(randomPiece.GetComponent<GamePiece>(), x, y);

            if (falseYOffset != 0)
            {
                randomPiece.transform.position = new Vector3(x, y + falseYOffset + HexYOffset(x), 0);
                randomPiece.GetComponent<GamePiece>().Move(x, y, moveTime, HexYOffset(x));
            }

            randomPiece.transform.parent = transform;
            return randomPiece.GetComponent<GamePiece>();
        }
        return null;
    }

    void FillBoard(int falseYOffset = 0, float moveTime = shortTick)
    {
        if (pieceColors.Length < 5)
        {
            Debug.LogWarning("BOARD:   Color count below minimum! High chance of unintentional match on Board!!!!");
        }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (m_allGamePieces[i, j] == null)
                {
                    List<PieceColor> pool = new List<PieceColor>(pieceColors);

                    GamePiece piece = FillRandomAt(i, j, pool, falseYOffset, moveTime);

                    while (HasMatchOnFill(i, j))
                    {
                        for (int k = 0; k < pool.Count; k++)
                        {
                            if (piece != null)
                            {
                                if (piece.pieceColor == pool[k])
                                {
                                    Debug.Log("removed pool");
                                    pool.RemoveAt(k);
                                    //movingPieces.Remove(piece);
                                    break;
                                }
                            }
                        }

                        if (!pool.Any())
                        {
                            Debug.Log("break =====================");
                            break;
                        }

                        ClearPieceAt(i, j);
                        piece = FillRandomAt(i, j, pool, falseYOffset, moveTime);
                    }
                }
            }
        }
    }

    IEnumerator StateSetCoroutine(float movingTime)
    {
        yield return new WaitForSeconds(movingTime);
        currentState = defaultState;
    }

    bool HasMatchOnFill(int x, int y, int minLength = 3)
    {
        List<GamePiece> match = FindMatchesAt(x, y, minLength);

        return (match.Any());
    }

    public void ClickTile(Tile tile)
    {
        if (m_clickedTile == null)
        {
            m_clickedTile = tile;
        }
    }

    public void DragToTile(Tile tile)
    {
        if (m_clickedTile != null)
        {
            m_targetTile = tile;
        }
    }

    public void ReleaseTile()
    {
        if (m_clickedTile != null && m_targetTile != null)
        {
            m_targetTile = GetRealTargetTile(m_clickedTile, m_targetTile);
            SwitchTiles(m_clickedTile, m_targetTile);
        }

        m_clickedTile = null;
        m_targetTile = null;
    }

    void SwitchTiles(Tile clickedTile, Tile targetTile)
    {
        StartCoroutine(SwitchTilesRoutine(clickedTile, targetTile));
    }

    IEnumerator SwitchTilesRoutine(Tile clickedTile, Tile targetTile)
    {
        if (currentState == defaultState)
        {
            currentState = movingState;

            GamePiece clickedPiece = m_allGamePieces[clickedTile.xIndex, clickedTile.yIndex];
            GamePiece targetPiece = m_allGamePieces[targetTile.xIndex, targetTile.yIndex];

            if (targetPiece != null && clickedPiece != null)
            {
                clickedPiece.Move(targetTile.xIndex, targetTile.yIndex, longTick, HexYOffset(targetTile.xIndex));
                targetPiece.Move(clickedTile.xIndex, clickedTile.yIndex, longTick, HexYOffset(clickedTile.xIndex));

                yield return new WaitForSeconds(longTick);

                List<GamePiece> clickedPieceMatches = FindMatchesAt(clickedTile.xIndex, clickedTile.yIndex);
                List<GamePiece> targetPieceMatches = FindMatchesAt(targetTile.xIndex, targetTile.yIndex);

                if (!targetPieceMatches.Any() && !clickedPieceMatches.Any())
                {
                    clickedPiece.Move(clickedTile.xIndex, clickedTile.yIndex, longTick, HexYOffset(clickedTile.xIndex));
                    targetPiece.Move(targetTile.xIndex, targetTile.yIndex, longTick, HexYOffset(targetTile.xIndex));
                    yield return new WaitForSeconds(longTick);
                    currentState = defaultState;
                }
                else
                {
                    ClearAndRefillBoard(clickedPieceMatches.Union(targetPieceMatches).ToList());
                }
            }
        }
    }

    Tile GetRealTargetTile(Tile start, Tile end)
    {
        Tile real = end;
        int sx = start.xIndex, sy = start.yIndex;
        float angle = Mathf.Atan2(end.transform.position.y - start.transform.position.y, end.transform.position.x - start.transform.position.x) * Mathf.Rad2Deg;

        if (angle >= 0)
        {
            if (angle >= 0 && angle < 60) real = m_allTiles[sx + 1, sy + (sx % 2)];
            else if (angle >= 60 && angle < 120) real = m_allTiles[sx, sy + 1];
            else real = m_allTiles[sx - 1, sy + (sx % 2)];
        }
        else
        {
            if (angle < 0 && angle > -60) real = m_allTiles[sx + 1, sy - 1 + (sx % 2)];
            else if (angle <= -60 && angle > -120) real = m_allTiles[sx, sy - 1];
            else real = m_allTiles[sx - 1, sy - 1 + (sx % 2)];
        }

        return real;
    }

    public List<GamePiece> GetLine(int startX, int startY, Vector2 searchDirection)
    {
        List<GamePiece> pieces = new List<GamePiece>();
        GamePiece startPiece = null;

        Debug.Log("getting line");

        if (IsWithinBounds(startX, startY))
        {
            Debug.Log("got startpiece");
            startPiece = m_allGamePieces[startX, startY];
        }

        if (startPiece == null)
        {
            Debug.Log("no startpiece");
            return new List<GamePiece>();
        }

        int nextX = startX;
        int nextY = startY;

        int maxValue = (width > height) ? width : height;

        for (int i = 1; i < maxValue - 1; i++)
        {
            nextY = nextY + (int)Mathf.Clamp(OffColumn(nextX), -1 + searchDirection.y, searchDirection.y);
            nextX = nextX + (int)Mathf.Clamp(searchDirection.x, -1, 1);

            if (!IsWithinBounds(nextX, nextY))
            {
                break;
            }

            GamePiece nextPiece = m_allGamePieces[nextX, nextY];

            if (nextPiece != null)
            {
                pieces.Add(nextPiece);
            }
        }

        Debug.Log("got line");

        return pieces;
    }

    List<GamePiece> FindMatches(int startX, int startY, Vector2 searchDirection, int minLength = 3)
    {
        List<GamePiece> matches = new List<GamePiece>();
        GamePiece startPiece = null;

        if (IsWithinBounds(startX, startY))
        {
            startPiece = m_allGamePieces[startX, startY];
        }

        if (startPiece != null)
        {
            matches.Add(startPiece);
        }
        else
        {
            return new List<GamePiece>();
        }

        int nextX = startX;
        int nextY = startY;

        int maxValue = (width > height) ? width : height;

        for (int i = 1; i < maxValue - 1; i++)
        {
            nextY = nextY + (int)Mathf.Clamp(OffColumn(nextX), -1 + searchDirection.y, searchDirection.y);
            nextX = nextX + (int)Mathf.Clamp(searchDirection.x, -1, 1);

            if (!IsWithinBounds(nextX, nextY))
            {
                break;
            }

            GamePiece nextPiece = m_allGamePieces[nextX, nextY];

            if (nextPiece == null)
            {
                break;
            }
            else
            {
                if (nextPiece.pieceColor == startPiece.pieceColor && !matches.Contains(nextPiece))
                {
                    matches.Add(nextPiece);
                }
                else
                {
                    break;
                }
            }
        }

        if (matches.Count >= minLength)
        {
            return matches;
        }

        return new List<GamePiece>();
    }

    List<GamePiece> FindVerticalMatches(int startX, int startY, int minLength = 3)
    {
        List<GamePiece> upwardMatches = FindMatches(startX, startY, new Vector2(0, 2), 2);
        List<GamePiece> downwardMatches = FindMatches(startX, startY, new Vector2(0, -1), 2);

        var combinedMatches = upwardMatches.Union(downwardMatches).ToList();

        return (combinedMatches.Count >= minLength) ? combinedMatches : new List<GamePiece>();
    }  

    List<GamePiece> FindHorizontalUpMatches(int startX, int startY, int minLength = 3)
    {
        List<GamePiece> rightMatches = FindMatches(startX, startY, new Vector2(1, 1), 2);
        List<GamePiece> leftMatches = FindMatches(startX, startY, new Vector2(-1, 0), 2);

        var combinedMatches = rightMatches.Union(leftMatches).ToList();

        return (combinedMatches.Count >= minLength) ? combinedMatches : new List<GamePiece>();
    }

    List<GamePiece> FindHorizontalDownMatches(int startX, int startY, int minLength = 3)
    {
        List<GamePiece> rightMatches = FindMatches(startX, startY, new Vector2(1, 0), 2);
        List<GamePiece> leftMatches = FindMatches(startX, startY, new Vector2(-1, 1), 2);

        var combinedMatches = rightMatches.Union(leftMatches).ToList();

        return (combinedMatches.Count >= minLength) ? combinedMatches : new List<GamePiece>();
    }

    List<GamePiece> FindMatchesAt(int x, int y, int minLength = 3)
    {
        List<GamePiece> horizUpMatches = FindHorizontalUpMatches(x, y, 3);
        List<GamePiece> horizDownMatches = FindHorizontalDownMatches(x, y, 3);
        List<GamePiece> vertMatches = FindVerticalMatches(x, y, 3);

        var combinedHorMatches = horizUpMatches.Union(horizDownMatches).ToList();
        var combinedMatches = combinedHorMatches.Union(vertMatches).ToList();
        return combinedMatches;
    }

    List<GamePiece> FindMatchesAt(List<GamePiece> gamePieces, int minLength = 3)
    {
        List<GamePiece> matches = new List<GamePiece>();

        foreach (GamePiece piece in gamePieces)
        {
            matches = matches.Union(FindMatchesAt(piece.xIndex, piece.yIndex, minLength)).ToList();
        }

        return matches;
    }

    List<GamePiece> FindAllMatches()
    {
        List<GamePiece> combinedMatches = new List<GamePiece>();

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                List<GamePiece> matches = FindMatchesAt(i, j);
                combinedMatches = combinedMatches.Union(matches).ToList();
            }
        }

        return combinedMatches;
    }

    void HighlightTileOff(int x, int y)
    {
        SpriteRenderer spriteRenderer = m_allTiles[x, y].GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
    }

    void HighlightTileOn(int x, int y, Color col)
    {
        SpriteRenderer spriteRenderer = m_allTiles[x, y].GetComponent<SpriteRenderer>();
        spriteRenderer.color = col;
    }

    void HighlightMatchesAt(int x, int y)
    {
        //HighlightTileOff(x, y);
                
        var combinedMatches = FindMatchesAt (x, y);

        if (combinedMatches.Any())
        {
            foreach (GamePiece piece in combinedMatches)
            {
                HighlightTileOn(piece.xIndex, piece.yIndex, piece.GetComponent<SpriteRenderer>().color);
            }
        }
    }

    void HighlightMatches()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                HighlightMatchesAt(i, j);
            }
        }
    }

    void HighlightPieces(List<GamePiece> gamePieces)
    {
        foreach (GamePiece piece in gamePieces)
        {
            if (piece != null)
            {
                HighlightTileOn(piece.xIndex, piece.yIndex, piece.GetComponent<SpriteRenderer>().color);
            }
        }
    }

    void ClearPieceAt(int x, int y)
    {
        if (m_allGamePieces[x, y] != null)
        {
            m_allGamePieces[x, y].pieceType.OnClear(m_allGamePieces[x, y], this);
            m_allGamePieces[x, y] = null;
        }

        //HighlightTileOff(x, y);
    }

    public void ClearPieceAt(List<GamePiece> gamePieces)
    {
        foreach (GamePiece piece in gamePieces)
        {
            if (piece != null)
            {
                ClearPieceAt(piece.xIndex, piece.yIndex);
                if (m_particleManager != null)
                {
                    m_particleManager.ClearParticleAt(piece.xIndex, piece.yIndex + HexYOffset(piece.xIndex));
                }
            }
        }
    }

    void ClearBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                ClearPieceAt(i, j);
            }
        }
    }

    void CollapseColumn(int column, float collapseTime = shortTick)
    {
        int cnt = 0;

        for (int i = 0; i < height - 1; i++)
        {
            if (m_allGamePieces[column, i] == null)
            {
                for (int j = i + 1; j < height; j++)
                {
                    if (m_allGamePieces[column, j] != null)
                    {
                        m_allGamePieces[column, j].Move(column, i, collapseTime * (j - i), HexYOffset(column));
                        m_allGamePieces[column, i] = m_allGamePieces[column, j];
                        m_allGamePieces[column, i].SetCoord(column, i);
                        m_allGamePieces[column, j] = null;
                        break;
                    }
                }
            }
        }

        for (int i = height - 1; i >= 0; i--)
        {
            if (m_allGamePieces[column, i] != null) break;
            cnt++;
        }

        for (int i = cnt; i > 0; i--)
        {
            m_allGamePieces[column, height - i] = FillRandomAt(column, height - i, new List<PieceColor>(pieceColors), cnt, collapseTime * cnt);
        }

        if (collapseTime * cnt > maxMovingTime)
        {
            maxMovingTime = collapseTime * cnt;
        }
    }

    void CollapseColumn(List<GamePiece> gamePieces)
    {
        List<int> columnsToCollapse = GetColumns(gamePieces);

        foreach (int column in columnsToCollapse)
        {
            CollapseColumn(column);
        }
    }

    List<int> GetColumns(List<GamePiece> gamePieces)
    {
        List<int> columns = new List<int>();

        foreach (GamePiece piece in gamePieces)
        {
            if (!columns.Contains(piece.xIndex))
            {
                columns.Add(piece.xIndex);
            }
        }

        return columns;
    }

    public void ClearAndRefillBoard(List<GamePiece> gamePieces)
    {
        StartCoroutine(ClearAndRefillBoardRoutine(gamePieces));
    }

    IEnumerator ClearAndRefillBoardRoutine(List<GamePiece> gamePieces)
    {
        List<GamePiece> matches = gamePieces;

        do
        {
            maxMovingTime = 0;
            yield return StartCoroutine(ClearCollapseFillRoutine(matches));
            matches = FindAllMatches();

            yield return new WaitForSeconds(maxMovingTime + normTick);
        }
        while (matches.Any());

        currentState = defaultState;
    }

    IEnumerator ClearCollapseFillRoutine(List<GamePiece> gamePieces)
    {
        //HighlightPieces(gamePieces);

        yield return new WaitForSeconds(shortTick);
        ClearPieceAt(gamePieces);
        yield return new WaitForSeconds(normTick);
        CollapseColumn(gamePieces);
    }

    List<GamePiece> AllGamePieces()
    {
        List<GamePiece> allPieces = new List<GamePiece>();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (m_allGamePieces[i, j] != null)
                {
                    allPieces.Add(m_allGamePieces[i, j]);
                }
            }
        }
        
        return allPieces;
    }


    //InterType Methodes
    float Linear(float t)
    {
        return t;
    }

    float EaseIn(float t)
    {
        return 1 - Mathf.Cos(t * Mathf.PI * 0.5f);
    }

    float EaseOut(float t)
    {
        return Mathf.Sin(t * Mathf.PI * 0.5f);
    }

    float SmoothStep(float t)
    {
        return t * t * (3 - 2 * t);
    }

    float SmootherStep(float t)
    {
        return t * t * t * (t * (t * 6 - 15) + 10);
    }

    //TimeScale Function
    bool timeTriggered = false;
    [SerializeField] Image buttonImage;
    public void TimeSet(){
        timeTriggered = !timeTriggered;
        if (timeTriggered)
        {
            Time.timeScale = 0.2f;
            buttonImage.color = new Color(0, 0, 0, 255);
        }
        else
        {
            Time.timeScale = 1.0f;
            buttonImage.color = new Color(255, 255, 255, 255);
        }
    }
}

