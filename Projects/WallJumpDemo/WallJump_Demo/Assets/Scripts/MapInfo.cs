using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    public RandomMapGanerater mapgan;
    public int mapIndex;
    public string nextLevel;
    public Transform endPos;
    private bool spawned;
    [SerializeField] bool neutral;
    [SerializeField] bool invert;
    public List<TriggerFunction> tf = new List<TriggerFunction>();

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!neutral && other.tag == "Player" && !spawned)
        {
            spawned = true;
            mapgan.wherePlayerIs = mapIndex;
            mapgan.MapSpawn();
            mapgan.Delete();
        }
    }

    public bool IsInvert()
    {
        return invert;
    }
}
