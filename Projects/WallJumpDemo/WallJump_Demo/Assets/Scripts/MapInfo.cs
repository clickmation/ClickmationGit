using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    public RandomMapGanerater mapgan;
    public int mapIndex;
    public string level;
    public Transform endPos;
    private bool spawned;

    public void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player" && !spawned)
        {
            spawned = true;
            mapgan.wherePlayerIs = mapIndex;
            mapgan.MapSpawnDelete();
        }
    }
}
