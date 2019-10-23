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

    public void SetRotation (float time, Vector2 vec)
    {
        //if (vec == Vector2.zero)
        //{
        //    mov.transform.rotation = Quaternion.Euler(0, 0, 0);
        //}
        //else
        //{
            StartCoroutine(SetRotationCoroutine(time, vec));
            //mov.transform.rotation = Quaternion.Euler(0, 0, -mov.dir * Mathf.Rad2Deg * Mathf.Asin(vec.normalized.y));
            //Debug.Log(Mathf.Asin(0.5f));
            //Debug.Log(vec + ", " + mov.dir * Mathf.Asin(vec.normalized.y));
        //}
    }

    IEnumerator SetRotationCoroutine (float time, Vector2 vec)
    {
        if (time == 0f)
        {
            mov.transform.rotation = Quaternion.Euler(0, 0, -mov.dir * Mathf.Rad2Deg * Mathf.Asin(vec.normalized.y));
            Debug.Log(-mov.dir * Mathf.Rad2Deg * Mathf.Asin(vec.normalized.y));
        }
        else
        {
            float z = Mathf.Rad2Deg * mov.transform.rotation.z;
            float r = vec == Vector2.zero ? 0 : -mov.dir * Mathf.Rad2Deg * Mathf.Asin(vec.normalized.y);
            //Debug.Log(z + ", " + r);
            for (float t = 0; t < time; t += Time.deltaTime)
            {
                mov.transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(z, r, t / time));
                yield return new WaitForSeconds(Time.deltaTime);
            }
            mov.transform.rotation = Quaternion.Euler(0, 0, r);
        }
    }
}
