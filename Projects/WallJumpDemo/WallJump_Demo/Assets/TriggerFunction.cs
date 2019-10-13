using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFunction : MonoBehaviour
{
    Animator anim;

    void Start ()
    {
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        anim.SetTrigger("Triggered");
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        anim.SetTrigger("Triggered");
    }

    public void Destroy ()
    {
        Destroy(this.gameObject);
    }
}
