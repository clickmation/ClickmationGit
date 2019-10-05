using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float wallStaminaEater;
    public float wallSlideSpeed;
    public float vecX;
    public float vecY;
    public float force;

    public Vector2 SetVec (float dir)
    {
        Vector2 tmp = new Vector2 (Mathf.Abs(vecX), vecY);
        tmp = new Vector2(dir * tmp.normalized.x, tmp.normalized.y) * force;
        return tmp;
    }
}
