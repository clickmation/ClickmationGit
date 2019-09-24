using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPanel : MonoBehaviour
{
    [SerializeField] int dir;
    private Movement movement;
    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.transform.GetComponent<Movement>() != null)
        {
            movement = other.transform.GetComponent<Movement>();
            if (dir == -1)
            {
                movement.wallJumped = true;
                if (movement.GetComponent<Collision>().wall != null) movement.GetComponent<Collision>().wall = null;
                movement.stamina -= movement.staminaWallJumpEater;
                movement.ChangeCameraPosition();
            }
            movement.Jump(dir);
            //movement.jumpButtonDown = true;
        }
    }
}
