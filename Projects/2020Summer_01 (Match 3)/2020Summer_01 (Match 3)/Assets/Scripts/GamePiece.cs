using System.Collections;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public int xIndex;
    public int yIndex;

    bool m_isMoving = false;

    Board m_board;

    public InterpType interpolation = InterpType.SmootherStep;

    public enum InterpType
    {
        Linear,
        EaseOut,
        EaseIn,
        SmoothStep,
        SmootherStep,
    };

    public MatchValue matchValue;

    public enum MatchValue
    {
        Yellow,
        Blue,
        Purple,
        Indigo,
        Green,
        Teal,
        Red,
        Cyan,
        Wild
    };

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move((int)transform.position.x + 3, (int)transform.position.y, 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move((int)transform.position.x - 3, (int)transform.position.y, 0.5f);
        }
    }

    public void Init(Board board)
    {
        m_board = board;
    }

    public void SetCoord(int x, int y)
    {
        xIndex = x;
        yIndex = y;
    }

    public void Move (int destX, int destY, float timeToMove, float hexYOffset = 0f)
    {
        if (!m_isMoving)
        {
            StartCoroutine(MoveRoutine(new Vector3(destX, destY + hexYOffset, 0), timeToMove));
        }
    }

    IEnumerator MoveRoutine(Vector3 destination, float timeToMove)
    {
        Vector3 startPostion = transform.position;

        bool reachedDestination = false;

        float elapsedTime = 0f;

        m_isMoving = true;

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

            switch (interpolation)
            {
                case InterpType.Linear:
                    break;
                case InterpType.EaseOut:
                    t = Mathf.Sin(t * Mathf.PI * 0.5f);
                    break;
                case InterpType.EaseIn:
                    t = 1 - Mathf.Cos(t * Mathf.PI * 0.5f);
                    break;
                case InterpType.SmoothStep:
                    t = t*t*(3 - 2*t);
                    break;
                case InterpType.SmootherStep:
                    t = t*t*t*(t*(t*6 - 15) +10);
                    break;
            }

            transform.position = Vector3.Lerp(startPostion, destination, t);

            yield return null;
        }

        m_isMoving = false;
    }

    public bool isMoving()
    {
        return m_isMoving;
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}
