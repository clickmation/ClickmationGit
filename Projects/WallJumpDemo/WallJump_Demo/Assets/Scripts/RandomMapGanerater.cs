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
    public List<GameObject> mapList;

    void Start()
    {
        mapSpawnPosition = Vector3.zero;
        for (int i = 0; i < levels.Length; i++)
        {
            if (spawnLevel == levels[i].levelName)
            {
                GameObject map = Instantiate(levels[i].maps[Random.Range(0, levels[i].maps.Length)], mapSpawnPosition, Quaternion.identity, this.transform);
                MapInfo mapInfo = map.GetComponent<MapInfo>();
                mapInfo.mapgan = this;
                mapSpawnPosition = mapInfo.endPos.position;
                mapList.Add(map);
                mapInfo.mapIndex = mapIndex;
                break;
            }
        }
    }

    public void MapSpawnDelete()
    {
        if ((wherePlayerIs - mapList[0].GetComponent<MapInfo>().mapIndex) > 2)
        {
            Destroy(mapList[0]);
            mapList.RemoveAt(0);
        }
    }
}
