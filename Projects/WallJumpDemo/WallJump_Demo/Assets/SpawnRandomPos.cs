using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomPos : MonoBehaviour
{
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private Transform[] spawnPos;

    public void SpawnRandomPosition ()
    {
        GameObject _spawnObject = Instantiate(spawnObject, spawnPos[Random.Range(0, spawnPos.Length)].position, Quaternion.Euler(0, 0, 0));
    }
}
