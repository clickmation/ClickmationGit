﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    public RandomMapGanerater mapgan;
    public int mapIndex;
    public string nextLevel;
    public Transform endPos;
    private bool spawned;

    public Level[] levels;
    [System.Serializable]
    public struct Level
    {
        public string levelName;
        public int[] maps;
    };

    public void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player" && !spawned)
        {
            spawned = true;
            mapgan.wherePlayerIs = mapIndex;
            MapInstantiate(nextLevel, -1);
            mapgan.MapSpawnDelete();
        }
    }

    public void MapInstantiate(string lv, int index)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (lv == levels[i].levelName)
            {
                int r = Random.Range(0, levels[i].maps.Length);
                GameObject map;
                if (index == -1)
                {
                    map = Instantiate(mapgan.levels[i].maps[levels[i].maps[r]], endPos.position, Quaternion.identity, mapgan.transform);
                }
                else
                {
                    map = Instantiate(mapgan.levels[i].maps[levels[i].maps[r]], endPos.position, Quaternion.identity, mapgan.transform);
                }
                MapInfo mapInfo = map.GetComponent<MapInfo>();
                mapInfo.mapgan = this.mapgan;
                mapInfo.mapIndex = (mapIndex + 1);
                mapgan.mapList.Add(map);
                break;
            }
        }
    }
}