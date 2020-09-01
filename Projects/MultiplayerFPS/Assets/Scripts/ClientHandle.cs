using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        //순서 중요 : 서버에서 한 순서랑 맞추기
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt(); 

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;

        ClientSend.WelcomeReceived();
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation);
    }

    public static void PlayerPosition(Packet _packet)
    {
        int _playerId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        Vector3 _velocity = _packet.ReadVector3();
        bool _isGrounded = _packet.ReadBool();

        if (GameManager.players.TryGetValue(_playerId, out PlayerManager _player))
        {
            _player.transform.position = _position;
            if (_player.animator != null)
            {
                _player.animator.SetFloat("VelX", _velocity.x);
                _player.animator.SetFloat("VelY", _velocity.y);
                _player.animator.SetFloat("VelZ", _velocity.z);
                _player.animator.SetBool("Grounded", _isGrounded);
            }
        }
    }

    public static void PlayerRotation(Packet _packet)
    {
        int _playerId = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();

        if (GameManager.players.TryGetValue(_playerId, out PlayerManager _player))
        {
            _player.transform.rotation = _rotation;
        }
    }

    public static void PlayerDisconnected(Packet _packet)
    {
        int _id = _packet.ReadInt();

        Destroy(GameManager.players[_id].gameObject);
        GameManager.players.Remove(_id);
    }

    public static void PlayerHealth(Packet _packet)
    {
        int _id = _packet.ReadInt();
        float _health = _packet.ReadFloat();

        GameManager.players[_id].SetHealth(_health);
    }

    public static void PlayerRespawned(Packet _packet)
    {
        int _id = _packet.ReadInt();

        GameManager.players[_id].Respawn();
    }

    public static void MessageToAll(Packet _packet)
    {
        int _senderId = _packet.ReadInt();
        int _index = _packet.ReadInt(); 
        string _msg = _packet.ReadString();

        OverlayManager.instance.AddChat(_senderId, _index, _msg);
    }

    public static void CreateItemSpawner(Packet _packet)
    {
        int _spawnerId = _packet.ReadInt();
        Vector3 _spawnerPosition = _packet.ReadVector3();
        bool _hasItem = _packet.ReadBool();

        GameManager.instance.CreateItemSpawner(_spawnerId, _spawnerPosition, _hasItem);
    }

    public static void ItemSpawned(Packet _packet)
    {
        int _spawnerId = _packet.ReadInt();

        GameManager.itemSpawners[_spawnerId].ItemSpawned();
    }

    public static void ItemPickedUp(Packet _packet)
    {
        int _spawnerId = _packet.ReadInt();
        int _byPlayer = _packet.ReadInt();

        GameManager.itemSpawners[_spawnerId].ItemPickedUp();
        GameManager.players[_byPlayer].itemCount++;
    }

    public static void SpawnProjectile(Packet _packet)
    {
        int _projectileId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        int _thrownByPlayer = _packet.ReadInt();

        GameManager.instance.SpawnProjectile(_projectileId, _position);
        GameManager.players[_thrownByPlayer].itemCount--;
    }

    public static void ProjectilePosition(Packet _packet)
    {
        int _projectileId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        if (GameManager.projectiles.TryGetValue(_projectileId, out ProjectileManager _projectile))
        {
            _projectile.transform.position = _position;
        }
    }

    public static void ProjectileExploded(Packet _packet)
    {
        int _projectileId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        if (GameManager.projectiles.TryGetValue(_projectileId, out ProjectileManager _projectile))
        {
           GameManager.projectiles[_projectileId].Explode(_position);
        }
    }

    public static void SpawnGun(Packet _packet)
    {
        int _gunId = _packet.ReadInt();
        string _gunName = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();

        GameManager.instance.SpawnGun(_gunId, _gunName, _position);
    }

    public static void GunPosition(Packet _packet)
    {
        int _gunId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        if (GameManager.guns.TryGetValue(_gunId, out GunManager _gun))
        {
            _gun.transform.position = _position;
            _gun.transform.rotation = _rotation;
        }
    }

    public static void EquipGun(Packet _packet)
    {
        
    }

    public static void UnEquipGun(Packet _packet)
    {
        
    }

    public static void SpawnEnemy(Packet _packet)
    {
        int _enemyId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        GameManager.instance.SpawnEnemy(_enemyId, _position);
    }
    
    public static void EnemyPosition(Packet _packet)
    {
        int _enemyId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        if (GameManager.enemies.TryGetValue(_enemyId, out EnemyManager _enemy))
        {
            _enemy.transform.position = _position;
        }
    }
    
    public static void EnemyHealth(Packet _packet)
    {
        int _enemyId = _packet.ReadInt();
        float _health = _packet.ReadFloat();

        GameManager.enemies[_enemyId].SetHealth(_health);
    }
}