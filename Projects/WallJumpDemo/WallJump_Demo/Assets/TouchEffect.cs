using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCoroutine());
    }

    IEnumerator StartCoroutine ()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
