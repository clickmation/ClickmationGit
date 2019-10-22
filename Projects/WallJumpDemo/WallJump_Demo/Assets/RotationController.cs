using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField] private Movement mov;

    // Update is called once per frame
    //void FixedUpdate()
    //{
        
    //}

    public void SetRotation (Vector2 vec)
    {
        if (vec == Vector2.zero)
        {
            mov.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            mov.transform.rotation = Quaternion.Euler(0, 0, -mov.dir * Mathf.Rad2Deg * Mathf.Asin(vec.normalized.y));
            Debug.Log(Mathf.Asin(0.5f));
            Debug.Log(vec + ", " + mov.dir * Mathf.Asin(vec.normalized.y));
        }
    }
}
