using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMapGanerater : MonoBehaviour
{
    [SerializeField] int mapIndex;
    public int wherePlayerIs;
    [System.Serializable]
    public struct Level
    {
        public string levelName;
        public GameObject[] maps;
    };
    public Level[] levels;
    public string spawnLevel;
    private Vector3 mapSpawnPosition;
    [SerializeField] List<GameObject> mapList;

    void Start()
    {
        mapSpawnPosition = Vector3.zero;
        for (int i = 0; i < 3; i++)
        {
            MapInstantiate(spawnLevel, -1);
        }
    }

    public void MapInstantiate (string lv, int index)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (lv == levels[i].levelName)
            {
                GameObject map;
                if (index == -1)
                {
                    map = Instantiate(levels[i].maps[Random.Range(0, levels[i].maps.Length)], mapSpawnPosition, Quaternion.identity, this.transform);
                }
                else
                {
                    map = Instantiate(levels[i].maps[index], mapSpawnPosition, Quaternion.identity, this.transform);
                }
                MapInfo mapInfo = map.GetComponent<MapInfo>();
                mapInfo.mapgan = this;
                mapSpawnPosition = mapInfo.endPos.position;
                mapList.Add(map);
                mapInfo.mapIndex = mapIndex++;
                break;
            }
        }
    }

    public void MapSpawnDelete()
    {
        MapInstantiate(spawnLevel, -1);
        if ((wherePlayerIs - mapList[0].GetComponent<MapInfo>().mapIndex) > 2)
        {
            Destroy(mapList[0]);
            //mapList[0] = null;
            mapList.RemoveAt(0);
        }
    }
}
