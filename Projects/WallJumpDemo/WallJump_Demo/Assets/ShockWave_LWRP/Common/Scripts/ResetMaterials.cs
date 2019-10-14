using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetMaterials : MonoBehaviour
{

    public List<Material> ShockWaves = new List<Material>();

    // Start is called before the first frame update
    void Awake()
    {
        foreach (var m in ShockWaves)
        {
            m.SetInt("_active", 0);
        }
    }

}
