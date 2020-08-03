using System.Collections;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public PieceColor pieceColor;
    public PieceType pieceType;

    public int xIndex;
    public int yIndex;

    Board m_board;
    public void Init(Board board, PieceColor color, PieceType type)
    {
        m_board = board;
        pieceColor = color;
        pieceType = type;
        UpdateVis();
    }

    public void SetCoord(int x, int y)
    {
        xIndex = x;
        yIndex = y;
    }

    public void UpdateVis()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = pieceColor.color;
        spriteRenderer.sprite = pieceType.sprite;
    }

    public void Move (int destX, int destY, float timeToMove, float hexYOffset = 0f)
    {
        StartCoroutine(MoveRoutine(new Vector3(destX, destY + hexYOffset, 0), timeToMove));
    }

    IEnumerator MoveRoutine(Vector3 destination, float timeToMove)
    {
        Vector3 startPostion = transform.position;

        bool reachedDestination = false;

        float elapsedTime = 0f;

        //m_board.movingPieces.Add(this);

        while (!reachedDestination)
        {
            if (Vector3.Distance(transform.position, destination) < 0.01f)
            {
                reachedDestination = true;
                
                if (m_board != null)
                {
                    m_board.PlaceGamePiece(this, (int)destination.x, (int)destination.y);
                }
                break;
            }

            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp(elapsedTime / timeToMove, 0f, 1f);
            t = m_board.interTypeMethod(t);

            transform.position = Vector3.Lerp(startPostion, destination, t);

            yield return null;
        }

        //m_board.movingPieces.Remove(this);
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}
