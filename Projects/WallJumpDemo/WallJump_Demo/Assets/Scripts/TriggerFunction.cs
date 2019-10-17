﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFunction : MonoBehaviour
{
    Animator anim;
    [SerializeField] GameObject triggerObject;
    [SerializeField] TriggerType triggerType;
    [SerializeField] bool destroyed;
    enum TriggerType
    {
        ANIMATOR,
        ACTIVE,
    }

    void Start ()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        switch (triggerType)
        {
            case TriggerType.ACTIVE:
                if (destroyed) triggerObject.SetActive(false);
                else triggerObject.SetActive(true);
                break;
            case TriggerType.ANIMATOR:
                anim.SetTrigger("Triggered");
                break;
        }
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        anim.SetTrigger("Triggered");
    }

    public void Destroy ()
    {
        Destroy(this.gameObject);
    }
}
