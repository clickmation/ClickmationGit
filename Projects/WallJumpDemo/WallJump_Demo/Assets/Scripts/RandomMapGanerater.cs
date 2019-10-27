﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMapGanerater : MonoBehaviour
{
    [SerializeField] GameMaster gm;

    [SerializeField] int mapIndex;
    public int mapDir;
    public int wherePlayerIs;

    public Level[] levels;
    [System.Serializable]
    public struct Level
    {
        public string levelName;
        public Map[] maps;
    }

    [System.Serializable]
    public struct Map
    {
        public GameObject mapObj;
        public bool invert;
        public int dir;
    }

    public GameObject[] neutrals;

    public ScoreCut[] scoreCuts;
    [System.Serializable]
    public struct ScoreCut
    {
        public int scoreCut;
        public PercentBlock[] percentBlocks;
    }

    [System.Serializable]
    public struct PercentBlock
    {
        public string difficulty;
        public int percent;
    }

    Vector2 spawnPoint = Vector2.zero;
    public List<Map> mapList = new List<Map>();
    public List<GameObject> neutralList = new List<GameObject>();

    public void Start()
    {
        GameObject neutral = Instantiate(neutrals[0], spawnPoint, Quaternion.Euler(0, 0, 0), transform);
        spawnPoint = neutral.GetComponent<MapInfo>().endPos.position;
        neutralList.Add(neutral);
        Spawn();
    }

    public void Spawn()
    {
        string difficulty = null;
        int sum = 0;
        int r = Random.Range(0, sum);//0~100
        int tmp = 0;
        for (int i = 0; i < scoreCuts.Length - 1; i++)
        {
            if (scoreCuts[i].scoreCut <= gm.score && gm.score < scoreCuts[i + 1].scoreCut)
            {
                sum = 0;
                for (int j = 0; j < scoreCuts[i].percentBlocks.Length; j++)
                {
                    sum += scoreCuts[i].percentBlocks[j].percent;
                }
                r = Random.Range(0, sum);//0~100
                tmp = 0;
                for (int j = 0; j < scoreCuts[i].percentBlocks.Length; j++)
                {
                    if (tmp <= r && r < tmp + scoreCuts[i].percentBlocks[j].percent)
                    {
                        difficulty = scoreCuts[i].percentBlocks[j].difficulty;
                        break;
                    }
                    else
                    {
                        tmp += scoreCuts[i].percentBlocks[j].percent;
                    }
                }
                break;
            }
        }
        if (difficulty == null)
        {
            sum = 0;
            for (int j = 0; j < scoreCuts[scoreCuts.Length - 1].percentBlocks.Length; j++)
            {
                sum += scoreCuts[scoreCuts.Length - 1].percentBlocks[j].percent;
            }
            r = Random.Range(0, sum);//0~100
            tmp = 0;
            for (int j = 0; j < scoreCuts[scoreCuts.Length - 1].percentBlocks.Length; j++)
            {
                if (tmp <= r && r < tmp + scoreCuts[scoreCuts.Length - 1].percentBlocks[j].percent)
                {
                    difficulty = scoreCuts[scoreCuts.Length - 1].percentBlocks[j].difficulty;
                    break;
                }
                else
                {
                    tmp += scoreCuts[scoreCuts.Length - 1].percentBlocks[j].percent;
                }
            }
        }
        for (int i = 0; i < levels.Length; i++)
        {
            if (difficulty == levels[i].levelName)
            {
                if (mapList.Count != 0 && mapList[mapList.Count - 1].invert)
                {
                    int rn = Random.Range(1, neutrals.Length);
                    GameObject neutral = Instantiate(neutrals[rn], spawnPoint, Quaternion.Euler(0, 0, 0), transform);
                    neutral.transform.localScale = new Vector3(mapDir, 1, 1);
                    spawnPoint = neutral.GetComponent<MapInfo>().endPos.position;
                    neutralList.Add(neutral);
                }
                Map tmpMap = new Map();
                MapInfo mapInfo;
                int rm = Random.Range(0, levels[i].maps.Length);
                GameObject map = Instantiate(levels[i].maps[rm].mapObj, spawnPoint, Quaternion.Euler(0, 0, 0), transform);
                map.transform.localScale = new Vector3(mapDir, 1, 1);
                mapInfo = map.GetComponent<MapInfo>();
                mapInfo.mapgan = this;
                mapInfo.mapIndex = mapIndex++;
                spawnPoint = mapInfo.endPos.position;
                tmpMap.mapObj = map;
                tmpMap.invert = levels[i].maps[rm].invert;
                tmpMap.dir = mapDir;
                mapList.Add(tmpMap);
                if (!levels[i].maps[rm].invert)
                {
                    int rn = Random.Range(1, neutrals.Length);
                    GameObject neutral = Instantiate(neutrals[rn], spawnPoint, Quaternion.Euler(0, 0, 0), transform);
                    neutral.transform.localScale = new Vector3(mapDir, 1, 1);
                    spawnPoint = neutral.GetComponent<MapInfo>().endPos.position;
                    neutralList.Add(neutral);
                }
                else
                {
                    mapDir = mapDir == 1 ? -1 : 1;
                }
                break;
            }
        }
    }

    public void Delete ()
    {
        if ((wherePlayerIs - mapList[0].mapObj.GetComponent<MapInfo>().mapIndex) > 0)
        {
            Destroy(neutralList[0]);
            Destroy(mapList[0].mapObj);
            neutralList.RemoveAt(0);
            mapList.RemoveAt(0);
        }
    }
}
