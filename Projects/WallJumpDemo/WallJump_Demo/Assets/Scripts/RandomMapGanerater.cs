using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMapGanerater : MonoBehaviour
{
    [SerializeField] GameMaster gm;

    [SerializeField] int mapIndex;
    public int mapDir;
    public int wherePlayerIs;
    private int rot;

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
    }

    public GameObject[] neutrals;

    public string spawnLevel;
    public List<GameObject> mapList = new List<GameObject>();
    public List<GameObject> neutralList = new List<GameObject>();

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

    public void Start()
    {
        GameObject neutral = Instantiate(neutrals[0], spawnPoint, Quaternion.Euler(0, rot * 180, 0), transform);
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
                MapInfo mapInfo;
                int rm = Random.Range(0, levels[i].maps.Length);
                GameObject map = Instantiate(levels[i].maps[rm].mapObj, spawnPoint, Quaternion.Euler(0, rot * 180, 0), transform);
                mapInfo = map.GetComponent<MapInfo>();
                mapInfo.mapgan = this;
                mapInfo.mapIndex = mapIndex++;
                spawnPoint = mapInfo.endPos.position;
                if (levels[i].maps[rm].invert) rot = rot == 0 ? 1 : 0;
                if (levels[i].maps[rm].invert) mapDir = mapDir == 1 ? -1 : 1;
                int rn = Random.Range(0, neutrals.Length);
                GameObject neutral = Instantiate(neutrals[rn], spawnPoint, Quaternion.Euler(0, rot * 180, 0), transform);
                spawnPoint = neutral.GetComponent<MapInfo>().endPos.position;
                neutralList.Add(neutral);
                mapList.Add(map);
                break;
            }
        }
    }

    public void Delete ()
    {
        if ((wherePlayerIs - mapList[0].GetComponent<MapInfo>().mapIndex) > 0)
        {
            Destroy(neutralList[0]);
            Destroy(mapList[0]);
            neutralList.RemoveAt(0);
            mapList.RemoveAt(0);
        }
    }
}
