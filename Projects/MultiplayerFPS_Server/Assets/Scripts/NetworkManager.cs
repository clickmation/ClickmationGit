﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject projectilePrefab;
    

    private int _chatIndex = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object");
            Destroy(this);
        }
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
        
        Server.Start(10, 26950);
    }

    private void OnApplicationQuit()
    {
        Server.Stop();
    }

    public Player InstantiatePlayer ()
    {
        return Instantiate(playerPrefab, new Vector3(0f, 12f, 0f), Quaternion.identity).GetComponent<Player>();
    }

    public void InstantiateEnemy (Vector3 _position)
    {
        Instantiate(enemyPrefab, _position, Quaternion.identity);
    }

    public Projectile InstantiateProjectile(Transform _shootOrigin)
    {
        return Instantiate(projectilePrefab, _shootOrigin.position + _shootOrigin.forward * 0.7f, Quaternion.identity).GetComponent<Projectile>();
    }

    public Projectile InstantiateGun(Transform _shootOrigin, string _gunName)
    {
        return null;
    }

    public int ChatIndex()
    {
        _chatIndex++;

        return _chatIndex;
    }
}
