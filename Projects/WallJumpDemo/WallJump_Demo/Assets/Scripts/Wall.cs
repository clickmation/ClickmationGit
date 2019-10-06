using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float wallStaminaEater;
    public float wallSlideSpeed;
    [SerializeField] float vecX;
    [SerializeField] float vecY;
    [SerializeField] float force;

    public Vector2 SetVec (float dir, float x, float y)
    {
        if (x != 0 && y != 0)
        {
            vecX = x;
            vecY = y;
        }
        Vector2 tmp = new Vector2 (Mathf.Abs(vecX), vecY);
        tmp = new Vector2(dir * tmp.normalized.x, tmp.normalized.y) * force;
        return tmp;
    }
}
