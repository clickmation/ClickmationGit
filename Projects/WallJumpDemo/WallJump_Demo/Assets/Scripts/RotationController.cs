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

    public void SetRotation (float time, Vector2 vec, bool zeroStart)
    {
        if (setRotationCoroutine != null) StopCoroutine(setRotationCoroutine);
        setRotationCoroutine = SetRotationCoroutine(time, vec, zeroStart);
        StartCoroutine(setRotationCoroutine);
    }

    IEnumerator SetRotationCoroutine (float time, Vector2 vec, bool zeroStart)
    {
        if (time == 0f)
        {
            movSprite.rotation = Quaternion.Euler(0, 0, -mov.dir * Mathf.Rad2Deg * Mathf.Asin(vec.normalized.y));
            _movSprite.rotation = Quaternion.Euler(0, 0, -movSprite.rotation.z);
            //Debug.Log(-mov.dir * Mathf.Rad2Deg * Mathf.Asin(vec.normalized.y));
        }
        else
        {
            _movSprite.localRotation = Quaternion.Euler(0, 0, 0);
            float z;
            if (zeroStart) z = 0;
            else z = Mathf.Rad2Deg * movSprite.rotation.z;
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
