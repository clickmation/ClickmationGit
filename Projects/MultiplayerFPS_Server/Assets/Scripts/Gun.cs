using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : ScriptableObject
{
    public abstract void Shoot(Player _player, Vector3 _shootDirection);
    public abstract void Reload(Player _player);
    public abstract float GetFireRate();
}
