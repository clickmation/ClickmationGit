using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField] private Movement mov;
    [SerializeField] private Transform movSprite;
    [SerializeField] private Transform _movSprite;
    private IEnumerator setRotationCoroutine;

    void Start ()
    {
        
    }

    public void SetRotation (float time, Vector2 vec)
    {
        if (setRotationCoroutine != null) StopCoroutine(setRotationCoroutine);
        setRotationCoroutine = SetRotationCoroutine(time, vec);
        StartCoroutine(setRotationCoroutine);
    }

    IEnumerator SetRotationCoroutine (float time, Vector2 vec)
    {
        if (time == 0f)
        {
            movSprite.rotation = Quaternion.Euler(0, 0, -mov.dir * Mathf.Rad2Deg * Mathf.Asin(vec.normalized.y));
            //Debug.Log(-mov.dir * Mathf.Rad2Deg * Mathf.Asin(vec.normalized.y));
        }
        else
        {
            float z = Mathf.Rad2Deg * movSprite.rotation.z;
            float r = vec == Vector2.zero ? 0 : -mov.dir * Mathf.Rad2Deg * Mathf.Asin(vec.normalized.y);
            //Debug.Log(z + ", " + r);
            for (float t = 0; t < time; t += Time.deltaTime)
            {
                movSprite.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(z, r, t / time));
                yield return new WaitForSeconds(Time.deltaTime);
            }
            movSprite.rotation = Quaternion.Euler(0, 0, r);
        }
    }
}
