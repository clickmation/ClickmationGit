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

    public GameObject startingNeutral;

    public ScoreCut[] mapScoreCuts;

    //public ScoreCut[] neutralScoreCuts;
    [System.Serializable]
    public struct ScoreCut
    {
        public int scoreCut;
        public PercentBlock[] mapPercentBlocks;
        public PercentBlock[] neutralPercentBlocks;
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

    public PercentCut[] percentCuts;
    [System.Serializable]
    public struct PercentCut
    {
        public int scoreCut;
        public int percentCut;
    }

    void Awake()
    {
        RandomMapGanerater.randomMapGanerater = this;
    }

    public void Start()
    {
        GameObject neutral = Instantiate(startingNeutral, spawnPoint, Quaternion.Euler(0, 0, 0), transform);
        spawnPoint = neutral.GetComponent<MapInfo>().endPos.position;
        neutralList.Add(neutral);
        MapSpawn();
    }

    public void StartSpawn()
    {
        GameObject neutral = Instantiate(startingNeutral, mapList[0].mapObj.transform.position, Quaternion.Euler(0, 0, 0), transform);
        neutral.transform.localScale = new Vector3(-mapList[0].dir, 1, 1);
        gm.playerSpawnPoint = neutral.transform.GetChild(0).position;
        neutral.transform.SetParent(neutralList[0].transform);
    }

    public void MapSpawn()
    {
        string difficulty = null;
        int sum;
        int r;
        int tmp;
        for (int i = 0; i < mapScoreCuts.Length - 1; i++)
        {
            if (mapScoreCuts[i].scoreCut <= gm.realScore && gm.realScore < mapScoreCuts[i + 1].scoreCut)
            {
                sum = 0;
                for (int j = 0; j < mapScoreCuts[i].mapPercentBlocks.Length; j++)
                {
                    sum += mapScoreCuts[i].mapPercentBlocks[j].percent;
                }
                r = Random.Range(0, sum);//0~100
                tmp = 0;
                for (int j = 0; j < mapScoreCuts[i].mapPercentBlocks.Length; j++)
                {
                    if (tmp <= r && r < tmp + mapScoreCuts[i].mapPercentBlocks[j].percent)
                    {
                        difficulty = mapScoreCuts[i].mapPercentBlocks[j].difficulty;
                        break;
                    }
                    else
                    {
                        tmp += mapScoreCuts[i].mapPercentBlocks[j].percent;
                    }
                }
                break;
            }
        }
        if (difficulty == null)
        {
            sum = 0;
            for (int j = 0; j < mapScoreCuts[mapScoreCuts.Length - 1].mapPercentBlocks.Length; j++)
            {
                sum += mapScoreCuts[mapScoreCuts.Length - 1].mapPercentBlocks[j].percent;
            }
            r = Random.Range(0, sum);//0~100
            tmp = 0;
            for (int j = 0; j < mapScoreCuts[mapScoreCuts.Length - 1].mapPercentBlocks.Length; j++)
            {
                if (tmp <= r && r < tmp + mapScoreCuts[mapScoreCuts.Length - 1].mapPercentBlocks[j].percent)
                {
                    difficulty = mapScoreCuts[mapScoreCuts.Length - 1].mapPercentBlocks[j].difficulty;
                    break;
                }
                else
                {
                    tmp += mapScoreCuts[mapScoreCuts.Length - 1].mapPercentBlocks[j].percent;
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
                //Vector3 tmpVec = spawnPoint;
                spawnPoint = mapInfo.endPos.position;
                //gm.SetDeathYPosition(tmpVec.y, spawnPoint.y);
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
        int _sum;
        int _r;
        int _tmp;
        for (int i = 0; i < mapScoreCuts.Length - 1; i++)
        {
            if (mapScoreCuts[i].scoreCut <= gm.realScore && gm.realScore < mapScoreCuts[i + 1].scoreCut)
            {
                _sum = 0;
                for (int j = 0; j < mapScoreCuts[i].neutralPercentBlocks.Length; j++)
                {
                    _sum += mapScoreCuts[i].neutralPercentBlocks[j].percent;
                }
                _r = Random.Range(0, _sum);//0~100
                _tmp = 0;
                for (int j = 0; j < mapScoreCuts[i].neutralPercentBlocks.Length; j++)
                {
                    if (_tmp <= _r && _r < _tmp + mapScoreCuts[i].neutralPercentBlocks[j].percent)
                    {
                        _difficulty = mapScoreCuts[i].neutralPercentBlocks[j].difficulty;
                        break;
                    }
                    else
                    {
                        _tmp += mapScoreCuts[i].neutralPercentBlocks[j].percent;
                    }
                }
                break;
            }
        }
        if (_difficulty == null)
        {
            _sum = 0;
            for (int j = 0; j < mapScoreCuts[mapScoreCuts.Length - 1].neutralPercentBlocks.Length; j++)
            {
                _sum += mapScoreCuts[mapScoreCuts.Length - 1].neutralPercentBlocks[j].percent;
            }
            _r = Random.Range(0, _sum);//0~100
            _tmp = 0;
            for (int j = 0; j < mapScoreCuts[mapScoreCuts.Length - 1].neutralPercentBlocks.Length; j++)
            {
                if (_tmp <= _r && _r < _tmp + mapScoreCuts[mapScoreCuts.Length - 1].neutralPercentBlocks[j].percent)
                {
                    _difficulty = mapScoreCuts[mapScoreCuts.Length - 1].neutralPercentBlocks[j].difficulty;
                    break;
                }
                else
                {
                    _tmp += mapScoreCuts[mapScoreCuts.Length - 1].neutralPercentBlocks[j].percent;
                }
            }
        }
        for (int i = 0; i < levels.Length; i++)
        {
            if (_difficulty == levels[i].levelName)
            {
                int _rn = Random.Range(0, levels[i].neutrals.Length);
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

    public bool CoinSActive()
    {
        int _r;
        for (int i = 0; i < percentCuts.Length - 1; i++)
        {
            if (percentCuts[i].scoreCut <= gm.realScore && gm.realScore < percentCuts[i + 1].scoreCut)
            {
                _r = Random.Range(0, 100);
                if (_r <= percentCuts[i].percentCut) return true;
                else return false;
            }
        }
        return false;
    }
}
