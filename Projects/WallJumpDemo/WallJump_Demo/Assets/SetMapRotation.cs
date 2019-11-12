using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMapRotation : MonoBehaviour
{
    void Start()
    {
        float x = transform.localScale.x;
        transform.localScale = new Vector3(RandomMapGanerater.randomMapGanerater.curDir * x, transform.localScale.y, 1);
    }
}
