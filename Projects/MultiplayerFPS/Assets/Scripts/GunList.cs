using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "List/Gun List")]
public class GunList : ScriptableObject
{
    [System.Serializable]
    public struct Gun
    {
        public string gunName;
        public GameObject gunPrefab;
    }

    [SerializeField]
    public Gun[] gunArray;
}