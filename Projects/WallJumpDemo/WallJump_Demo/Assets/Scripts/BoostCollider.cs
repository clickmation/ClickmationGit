using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostCollider : MonoBehaviour
{
    [SerializeField] Movement movement;

    void OnMouseDown()
    {
        movement.boost = true;
    }

    void OnMouseUp()
    {
        movement.boost = false;
    }
}
