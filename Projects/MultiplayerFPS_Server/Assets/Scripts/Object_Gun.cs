using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Gun : Interactable
{
    Gun thisGun;

    public override void Interact(Player _player)
    {
        if (_player.curGun != null)
        {

        }

        _player.curGun = thisGun;
        Destroy(this.gameObject);
    }

    public override void SpawnObject(Object _object, int _spawnByPlayer)
    {

    }
}
