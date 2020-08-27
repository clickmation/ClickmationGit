using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gun/Gun_Raycast")]
public class Gun_Raycast : Gun
{
    public string gunName;
    public float damage;
    public float range;
    public float fireRate;
    public float impactForce;

    public int maxAmmo;
    public float reloadTime;

    public override void Shoot(Player _player, Vector3 _shootDirection)
    {
        if (Physics.Raycast(_player.shootOrigin.position, _shootDirection, out RaycastHit _hit, range))
        {
            if (_hit.collider.GetComponent<HealthManager>() != null)
            {
                _hit.collider.GetComponent<HealthManager>().TakeDamage(damage);
            }
        }
    }

    public override void Reload(Player _player)
    {

    }

    public override float GetFireRate()
    {
        return fireRate;
    }
}
