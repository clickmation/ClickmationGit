using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact(Player _player);
    public abstract void SpawnObject(Object _object, int _spawnByPlayer);
}
