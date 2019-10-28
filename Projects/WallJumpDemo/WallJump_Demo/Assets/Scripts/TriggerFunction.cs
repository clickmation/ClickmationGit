using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFunction : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject triggerObject;
    [SerializeField] TriggerType triggerType;
    public bool destroy;
    private bool added;
    enum TriggerType
    {
        ANIMATOR,
        ACTIVE,
    }

    //void Start ()
    //{
    //    anim = triggerObject.transform.GetComponent<Animator>();
    //}

    void OnTriggerEnter2D (Collider2D other)
    {
        if (!added)
        {
            added = true;
            GameMaster.gameMaster.triggerFunctions.Add(this);
        }
        switch (triggerType)
        {
            case TriggerType.ACTIVE:
                if (!destroy) triggerObject.SetActive(true);
                else triggerObject.SetActive(false);
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

    public void Trigger()
    {
        if (!destroy) triggerObject.SetActive(false);
        else triggerObject.SetActive(true);
    }

    public void Destroy ()
    {
        Destroy(this.gameObject);
    }
}
