using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    GameMaster gm;
    public int wallStaminaCount;
    public float wallSlideSpeed;
    [SerializeField] float vecX;
    [SerializeField] float vecY;
    [SerializeField] float force;

    void Start ()
    {
        if (GameMaster.gameMaster != null)
        {
            gm = GameMaster.gameMaster;
            Debug.Log(wallStaminaCount);
            for (int i = gm.addJumpScores.Length - 1; i >= 0; i--) {
                if (gm.score >= gm.addJumpScores[i].score)
                {
                    wallStaminaCount += gm.addJumpScores[i].addJump;
                    Debug.Log(gm.addJumpScores[i].addJump);
                    break;
                }
            }
        }
    }

    public Vector2 SetVec (float dir, float x, float y)
    {
        if (x != 0 && y != 0)
        {
            vecX = x;
            vecY = y;
            Vector2 tmp = new Vector2(Mathf.Abs(vecX), vecY);
            tmp = new Vector2(dir * tmp.normalized.x, tmp.normalized.y) * force;
            return tmp;
        }
        else
        {
            return Vector2.zero;
        }
    }
}
