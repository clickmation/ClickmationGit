using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public string username;
    public CharacterController controller;
    public Transform shootOrigin;
    public Health_Player playerHealth;
    public float gravity = -9.81f;
    private float moveSpeed;
    public float walkSpeed = 2.5f;
    public float runSpeed = 5f;
    public float jumpSpeed = 5f;
    public float throwForce = 600f;
    public float damage = 25f;
    public int itemAmount = 0;
    public int maxItemAmount = 3; 

    public Gun curGun;
    private int currentAmmo;
    private float nextTimeToFire = 0f;

    private bool[] inputs;
    private Vector3 viewDirection;
    private float yVelocity = 0;

    private void Start()
    {
        gravity *= Time.fixedDeltaTime * Time.fixedDeltaTime;
        walkSpeed *= Time.fixedDeltaTime;
        runSpeed *= Time.fixedDeltaTime;
        jumpSpeed *= Time.fixedDeltaTime;
    }

    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;
        playerHealth.Init();
        playerHealth.InputPlayer(this);

        inputs = new bool[7];
    }

    void Update()
    {
        if (inputs[6] && Time.time >= nextTimeToFire)
        {
            Shoot(viewDirection);
        }
    }

    public void FixedUpdate()
    {
        if (playerHealth.IsDead())
        {
            return;
        }

        Vector2 _inputDirection = Vector2.zero;
        if (inputs[0])
        {
            _inputDirection.y += 1;
        }
        if (inputs[1])
        {
            _inputDirection.y -= 1;
        }
        if (inputs[2])
        {
            _inputDirection.x -= 1;
        }
        if (inputs[3])
        {
            _inputDirection.x += 1;
        }

        Move(_inputDirection);
    }

    private void Move(Vector2 _inputDirection)
    {
        if (inputs[5])
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }

        Vector3 _moveDirection = transform.right * _inputDirection.x + transform.forward * _inputDirection.y;
        _moveDirection *= moveSpeed;

        if (controller.isGrounded)
        {
            yVelocity = 0f;
            if (inputs[4])
            {
                yVelocity = jumpSpeed;
            }
        }
        yVelocity += gravity;

        _moveDirection.y = yVelocity;
        controller.Move(_moveDirection);

        ServerSend.PlayerPosition(this);
        ServerSend.PlayerRotation(this);
    }

    public void SetInput(bool[] _inputs, Quaternion _rotation, Vector3 _viewDirection)
    {
        viewDirection = _viewDirection;
        inputs = _inputs;
        transform.rotation = _rotation;
    }

    public void Shoot(Vector3 _viewDirection)
    {
        if (playerHealth.IsDead() || curGun == null)
        {
            return;
        }

        nextTimeToFire = Time.time + 1f / curGun.GetFireRate();
        curGun.Shoot(this, _viewDirection);
    }

    public void ThrowItem(Vector3 _viewDirection)
    {
        if (playerHealth.IsDead())
        {
            return;
        }

        if (itemAmount > 0)
        {
            itemAmount--;
            NetworkManager.instance.InstantiateProjectile(shootOrigin).Initialize(_viewDirection, throwForce, id);
        }
    }

    public void Respawn()
    {
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        transform.position = new Vector3(0f, 25f, 0f);
        ServerSend.PlayerPosition(this);

        yield return new WaitForSeconds(5f);
        
        playerHealth.Init();
        controller.enabled = true;
        ServerSend.PlayerRespawned(this);
    }

    public bool AttemptPickupItem()
    {
        if (itemAmount >= maxItemAmount)
        {
            return false;
        }

        itemAmount++;
        return true;
    }
}
