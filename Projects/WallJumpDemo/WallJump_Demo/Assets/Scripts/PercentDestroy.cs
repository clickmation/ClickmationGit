using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentDestroy : MonoBehaviour
{
    public PercentCut[] percentCuts;

    [System.Serializable]
    public struct PercentCut
    {
        public string difficulty;
        public int percentCut;
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < percentCuts.Length; i++)
        {
            if (RandomMapGanerater.randomMapGanerater.curDifficulty == percentCuts[i].difficulty)
            {
                int r = Random.Range(0, 100);
                if (r > percentCuts[i].percentCut) Destroy(this.gameObject);
                break;
            }
        }
    }
}
