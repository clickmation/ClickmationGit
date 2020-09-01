using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Gun : Interactable
{
    public static Dictionary<int, Object_Gun> guns = new Dictionary<int, Object_Gun>();
    private static int nextGunId = 1;

    public int id;
    public Rigidbody rbody;
    public Collider col;
    public bool isEquipped;
    public int holdingPlayer;
    public float weight;
    public Gun thisGun;

    private void Start()
    {
        id = nextGunId;
        nextGunId++;
        guns.Add(id, this);
        if(GetComponent<Collider>() != null)
        {
            col = GetComponent<Collider>();
        }

        ServerSend.SpawnGun(this);
    }

    private void FixedUpdate()
    {
        if(!isEquipped)
        {
            ServerSend.GunPosition(this);
        }
    }

    public override void Interact(Player _player)
    {
        if (_player.curGun != null)
        {
            _player.curGun.UnEquip(_player);
        }

        isEquipped = true;
        _player.curGun = this;
        holdingPlayer = _player.id;

        ServerSend.EquipGun(this, _player);

        this.transform.position = Vector3.zero;
        this.transform.rotation = Quaternion.identity;
        col.enabled = false;
        this.transform.SetParent(_player.transform);
    }

    public void UnEquip(Player _player)
    {
        isEquipped = false;
        col.enabled = true;
        _player.curGun = null;
        this.transform.SetParent(null);

        ServerSend.UnEquipGun(_player);

        rbody.AddForce(_player.viewDirection * _player.throwForce / weight);
    }
}
