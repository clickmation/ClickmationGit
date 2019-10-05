using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPanel : MonoBehaviour
{
    private Movement mov;
    //public float 
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            mov = other.transform.parent.parent.GetComponent<Movement>();
            mov.jumpButtonDown = false;
            mov.jumpable = true;
            mov.panelJumped = true;
            mov.Jump(1);
            AudioManager.PlaySound("jumpPanel");
        }
    }
}
