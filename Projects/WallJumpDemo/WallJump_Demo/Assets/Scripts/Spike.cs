using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private Movement movement;
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.GetComponent<Movement>() != null)
        {
            movement = other.transform.GetComponent<Movement>();
            movement.Dead();
        }
    }
}
