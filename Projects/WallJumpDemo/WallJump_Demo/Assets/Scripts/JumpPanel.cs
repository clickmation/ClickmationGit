using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPanel : MonoBehaviour
{
    private Movement mov;
    [SerializeField] PanelType panelType;
    enum PanelType
    {
        GROUND,
        WALL,
    }
    public float xVel;
    public float height;
    [SerializeField] float dir;
    [SerializeField] float vecX;
    [SerializeField] float vecY;
    [SerializeField] float force;

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("Triggered");
            mov = other.transform.GetComponent<Movement>();
            mov.jumping = false;
            mov.jumpable = true;
            if (panelType == PanelType.GROUND)
            {
                mov.Jump(1, SetVec(mov.dir * Mathf.Sign(xVel), xVel, height));
            }
            else if (panelType == PanelType.WALL)
            {
                mov.panelJumped = true;
                mov.Jump(1, SetVec(dir, vecX, vecY));
            }
            AudioManager.PlaySound("jumpPanel");
        }
    }

    public Vector2 SetVec(float dir, float x, float y)
    {
        Vector2 tmp;
        if (panelType == PanelType.GROUND)
        {
            tmp = new Vector2(dir * Mathf.Abs(x), y);
            return tmp;
        }
        else if (panelType == PanelType.WALL)
        {
            if (x != 0 && y != 0)
            {
                vecX = x;
                vecY = y;
                tmp = new Vector2(Mathf.Abs(vecX), vecY);
                tmp = new Vector2(dir * tmp.normalized.x, tmp.normalized.y) * force;
                return tmp;
            }
            else
            {
                return Vector2.zero;
            }
        }
        else
        {
            return Vector2.zero;
        }
    }
}
