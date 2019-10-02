using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPanel : MonoBehaviour
{
    [SerializeField] int type;
    private Movement movement;
    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.transform.GetComponent<Movement>() != null)
        {
            movement = other.transform.GetComponent<Movement>();
            //Debug.Log(movement.jumpable + ", " + movement.jumpButtonDown);
            if (type == -1)
            {
                movement.jumpable = true;
                movement.jumpButtonDown = false;
                movement.wallJumped = true;
                if (movement.GetComponent<Collision>().wall != null) movement.GetComponent<Collision>().wall = null;
                movement.stamina -= movement.staminaWallJumpEater;
                movement.ChangeCameraPosition();
                //Debug.Log(movement.jumpable + ", " + movement.jumpButtonDown);
            }
            //Debug.Log(movement.jumpable + ", " + movement.jumpButtonDown);
            movement.Jump(type);
        }
    }
}
