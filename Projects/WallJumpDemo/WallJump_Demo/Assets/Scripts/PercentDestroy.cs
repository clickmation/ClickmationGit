using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentDestroy : MonoBehaviour
{
    void Start()
    {
        if (!RandomMapGanerater.randomMapGanerater.CoinSActive()) Destroy(this.gameObject);
    }
}
