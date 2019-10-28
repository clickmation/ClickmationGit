using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelayMap : MonoBehaviour
{
    [SerializeField] MapInfo mapInfo;
    [SerializeField] private GameObject[] oriMaps;
    private List<GameObject> tmpMaps = new List<GameObject>();
    [SerializeField] private List<MapInfo> spawnMaps = new List<MapInfo>();
    Vector3 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        int r;
        spawnPoint = transform.position;
        for (int i = 0; i < oriMaps.Length; i++) tmpMaps.Add(oriMaps[i]);
        for (int i = 0; i < oriMaps.Length; i++)
        {
            r = Random.Range(0, tmpMaps.Count);
            GameObject tmpMap = Instantiate(tmpMaps[r], spawnPoint, Quaternion.Euler(0, 0, 0), transform);
            tmpMap.transform.parent = transform;
            spawnPoint = tmpMap.GetComponent<MapInfo>().endPos.position;
            spawnMaps.Add(tmpMap.GetComponent<MapInfo>());
            for (int j = 0; j < spawnMaps[i].tf.Count; j++) mapInfo.tf.Add(spawnMaps[i].tf[j]);
            if (i != 0 && spawnMaps[i - 1].IsInvert()) tmpMap.transform.localScale = new Vector3 (-spawnMaps[i - 1].transform.localScale.x, 1, 1);
            Destroy(tmpMap.GetComponent<BoxCollider2D>());
            tmpMaps.RemoveAt(r);
        }
        mapInfo.endPos = spawnMaps[spawnMaps.Count - 1].endPos;
        RandomMapGanerater.randomMapGanerater.SetMapInfoAgain(mapInfo.endPos.position, spawnMaps[spawnMaps.Count - 1].IsInvert());
    }
}
