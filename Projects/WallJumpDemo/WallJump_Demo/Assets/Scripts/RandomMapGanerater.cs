using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMapGanerater : MonoBehaviour
{
    public static RandomMapGanerater randomMapGanerater;
    [SerializeField] GameMaster gm;

    [SerializeField] int mapIndex;
    public int mapDir;
    public int wherePlayerIs;
    public string curDifficulty;

    public Level[] levels;
    [System.Serializable]
    public struct Level
    {
        public string levelName;
        public GameObject[] neutrals;
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

    public ScoreCut[] mapScoreCuts;

    public ScoreCut[] neutralScoreCuts;
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

    [SerializeField] Vector2 spawnPoint = new Vector2(-20, 0);
    public List<Map> mapList = new List<Map>();
    public List<GameObject> neutralList = new List<GameObject>();

    void Awake()
    {
        RandomMapGanerater.randomMapGanerater = this;
    }

    public void Start()
    {
        GameObject neutral = Instantiate(neutrals[0], spawnPoint, Quaternion.Euler(0, 0, 0), transform);
        spawnPoint = neutral.GetComponent<MapInfo>().endPos.position;
        neutralList.Add(neutral);
        MapSpawn();
    }

    public void StartSpawn()
    {
        GameObject neutral = Instantiate(neutrals[0], mapList[0].mapObj.transform.position, Quaternion.Euler(0, 0, 0), transform);
        neutral.transform.localScale = new Vector3(-mapList[0].dir, 1, 1);
        gm.playerSpawnPoint = neutral.transform.GetChild(0).position;
        neutral.transform.SetParent(neutralList[0].transform);
    }

    public void MapSpawn()
    {
        string difficulty = null;
        int sum = 0;
        int r = Random.Range(0, sum);//0~100
        int tmp = 0;
        for (int i = 0; i < mapScoreCuts.Length - 1; i++)
        {
            if (mapScoreCuts[i].scoreCut <= gm.realScore && gm.realScore < mapScoreCuts[i + 1].scoreCut)
            {
                sum = 0;
                for (int j = 0; j < mapScoreCuts[i].percentBlocks.Length; j++)
                {
                    sum += mapScoreCuts[i].percentBlocks[j].percent;
                }
                r = Random.Range(0, sum);//0~100
                tmp = 0;
                for (int j = 0; j < mapScoreCuts[i].percentBlocks.Length; j++)
                {
                    if (tmp <= r && r < tmp + mapScoreCuts[i].percentBlocks[j].percent)
                    {
                        difficulty = mapScoreCuts[i].percentBlocks[j].difficulty;
                        break;
                    }
                    else
                    {
                        tmp += mapScoreCuts[i].percentBlocks[j].percent;
                    }
                }
                break;
            }
        }
        if (difficulty == null)
        {
            sum = 0;
            for (int j = 0; j < mapScoreCuts[mapScoreCuts.Length - 1].percentBlocks.Length; j++)
            {
                sum += mapScoreCuts[mapScoreCuts.Length - 1].percentBlocks[j].percent;
            }
            r = Random.Range(0, sum);//0~100
            tmp = 0;
            for (int j = 0; j < mapScoreCuts[mapScoreCuts.Length - 1].percentBlocks.Length; j++)
            {
                if (tmp <= r && r < tmp + mapScoreCuts[mapScoreCuts.Length - 1].percentBlocks[j].percent)
                {
                    difficulty = mapScoreCuts[mapScoreCuts.Length - 1].percentBlocks[j].difficulty;
                    break;
                }
                else
                {
                    tmp += mapScoreCuts[mapScoreCuts.Length - 1].percentBlocks[j].percent;
                }
            }
        }
        for (int i = 0; i < levels.Length; i++)
        {
            if (difficulty == levels[i].levelName)
            {
                if (mapList.Count != 0 && mapList[mapList.Count - 1].invert)
                {
                    NeutralSpawn();
                }
                curDifficulty = difficulty;
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
                tmpMap.invert = mapInfo.IsInvert();
                tmpMap.dir = mapDir;
                mapList.Add(tmpMap);
                if (!levels[i].maps[rm].invert)
                {
                    NeutralSpawn();
                }
                else
                {
                    mapDir = mapDir == 1 ? -1 : 1;
                }
                break;
            }
        }
    }

    public void NeutralSpawn()
    {
        string _difficulty = null;
        int _sum = 0;
        int _r = Random.Range(0, _sum);//0~100
        int _tmp = 0;
        for (int i = 0; i < neutralScoreCuts.Length - 1; i++)
        {
            if (neutralScoreCuts[i].scoreCut <= gm.realScore && gm.realScore < neutralScoreCuts[i + 1].scoreCut)
            {
                _sum = 0;
                for (int j = 0; j < neutralScoreCuts[i].percentBlocks.Length; j++)
                {
                    _sum += neutralScoreCuts[i].percentBlocks[j].percent;
                }
                _r = Random.Range(0, _sum);//0~100
                _tmp = 0;
                for (int j = 0; j < neutralScoreCuts[i].percentBlocks.Length; j++)
                {
                    if (_tmp <= _r && _r < _tmp + neutralScoreCuts[i].percentBlocks[j].percent)
                    {
                        _difficulty = neutralScoreCuts[i].percentBlocks[j].difficulty;
                        break;
                    }
                    else
                    {
                        _tmp += neutralScoreCuts[i].percentBlocks[j].percent;
                    }
                }
                break;
            }
        }
        if (_difficulty == null)
        {
            _sum = 0;
            for (int j = 0; j < neutralScoreCuts[neutralScoreCuts.Length - 1].percentBlocks.Length; j++)
            {
                _sum += neutralScoreCuts[neutralScoreCuts.Length - 1].percentBlocks[j].percent;
            }
            _r = Random.Range(0, _sum);//0~100
            _tmp = 0;
            for (int j = 0; j < neutralScoreCuts[neutralScoreCuts.Length - 1].percentBlocks.Length; j++)
            {
                if (_tmp <= _r && _r < _tmp + neutralScoreCuts[neutralScoreCuts.Length - 1].percentBlocks[j].percent)
                {
                    _difficulty = neutralScoreCuts[neutralScoreCuts.Length - 1].percentBlocks[j].difficulty;
                    break;
                }
                else
                {
                    _tmp += neutralScoreCuts[neutralScoreCuts.Length - 1].percentBlocks[j].percent;
                }
            }
        }
        for (int i = 0; i < levels.Length; i++)
        {
            if (_difficulty == levels[i].levelName)
            {
                int _rn = Random.Range(1, levels[i].neutrals.Length);
                GameObject neutral = Instantiate(levels[i].neutrals[_rn], spawnPoint, Quaternion.Euler(0, 0, 0), transform);
                neutral.transform.localScale = new Vector3(mapDir, 1, 1);
                spawnPoint = neutral.GetComponent<MapInfo>().endPos.position;
                neutralList.Add(neutral);
                break;
            }
        }
    }

    public void Delete ()
    {
        if ((wherePlayerIs - mapList[0].mapObj.GetComponent<MapInfo>().mapIndex) > 0)
        {
            if (mapList[0].mapObj.GetComponent<MapInfo>().tf != null) gm.triggerFunctions.RemoveRange(0, mapList[0].mapObj.GetComponent<MapInfo>().tf.Count);
            Destroy(neutralList[0]);
            Destroy(mapList[0].mapObj);
            neutralList.RemoveAt(0);
            mapList.RemoveAt(0);
        }
    }

    //Only for RelayMap
    public void SetMapInfoAgain (Vector2 _spawnPoint, bool invert)
    {
        if (mapList.Count != 0 && invert)
        {
            spawnPoint = _spawnPoint;
        }
        else if (mapList.Count != 0 && !invert)
        {
            neutralList[neutralList.Count - 1].transform.position = _spawnPoint;
            spawnPoint = neutralList[neutralList.Count - 1].GetComponent<MapInfo>().endPos.position;
        }
        Map tmpMap = new Map();
        tmpMap = mapList[mapList.Count - 1];
        tmpMap.invert = invert;
        mapList[mapList.Count - 1] = tmpMap;
    }
}
