using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPanel : MonoBehaviour
{
    private Movement mov;
    public float xVel;
    public float height;
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            mov = other.transform.GetComponent<Movement>();
            mov.jumpButtonDown = false;
            mov.jumpable = true;
            //mov.panelJumped = true;
            mov.Jump(1, SetVec());
            AudioManager.PlaySound("jumpPanel");
        }
    }

    public Vector2 SetVec()
    {
        Vector2 tmp = new Vector2(mov.dir * Mathf.Abs(xVel), height);
        return tmp;
    }
}
