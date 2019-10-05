using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPanel : MonoBehaviour
{
    private Movement movement;
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            movement = other.transform.parent.parent.GetComponent<Movement>();
            movement.panelJumped = true;
            movement.Jump(1);
            AudioManager.PlaySound("jumpPanel");
        }
    }
}
