using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public static Dictionary<int, EnemyManager> enemies = new Dictionary<int, EnemyManager>();
    public static Dictionary<int, ItemSpawner> itemSpawners = new Dictionary<int, ItemSpawner>();
    public static Dictionary<int, GunManager> guns = new Dictionary<int, GunManager>();
    public static Dictionary<int, ProjectileManager> projectiles = new Dictionary<int, ProjectileManager>();

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
    public GameObject EnemyPrefab;
    public GameObject ItemSpawnerPrefab;
    public Dictionary<string, GameObject> gunPrefabs;
    public GunList gunList;
    public GameObject projectilePrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
            for (int i = 0; i < gunList.gunArray.Length; i++)
            {
                gunPrefabs.Add(gunList.gunArray[i].gunName, gunList.gunArray[i].gunPrefab);
            }
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object");
            Destroy(this);
        }
    }

    public void SpawnPlayer(int _id, string _username, Vector3 _position, Quaternion _rotation)
    {
        GameObject _player;
        if (_id == Client.instance.myId)
        {
            _player = Instantiate(localPlayerPrefab, _position, _rotation);
        }
        else
        {
            _player = Instantiate(playerPrefab, _position, _rotation);
        }

        _player.GetComponent<PlayerManager>().Initialize(_id, _username);
        players.Add(_id, _player.GetComponent<PlayerManager>());
    }

    public void SpawnEnemy(int _id, Vector3 _position)
    {
        GameObject _enemy = Instantiate(EnemyPrefab, _position, Quaternion.identity);
        _enemy.GetComponent<EnemyManager>().Initialize(_id);
        enemies.Add(_id, _enemy.GetComponent<EnemyManager>());
    }

    public void CreateItemSpawner(int _spawnerId, Vector3 _position, bool _hasItem)
    {
        GameObject _spawner = Instantiate(ItemSpawnerPrefab, _position, ItemSpawnerPrefab.transform.rotation);
        _spawner.GetComponent<ItemSpawner>().Initialize(_spawnerId, _hasItem);
        itemSpawners.Add(_spawnerId, _spawner.GetComponent<ItemSpawner>());
    }

    public void SpawnGun(int _id, string _name, Vector3 _position)
    {
        GameObject _gun = Instantiate(gunPrefabs[_name], _position, Quaternion.identity);
        _gun.GetComponent<GunManager>().Initialize(_id);
        guns.Add(_id, _gun.GetComponent<GunManager>());
    }

    public void SpawnProjectile(int _id, Vector3 _position)
    {
        GameObject _projectile = Instantiate(projectilePrefab, _position, Quaternion.identity);
        _projectile.GetComponent<ProjectileManager>().Initialize(_id);
        projectiles.Add(_id, _projectile.GetComponent<ProjectileManager>());
    }
}