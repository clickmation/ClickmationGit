﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [Space]

    [Header("Dead")]

    [SerializeField] bool dead;
    [SerializeField] Transform map;

    //[Space]

    //[Header("Bool")]

    //[SerializeField] bool onGround;
    //[SerializeField] bool onWall;

    [Space]

    [Header("Default")]

    public Rigidbody2D rb;
    public float speed;
    private float _speed;
    public float boostingTime;
    public float maxSpeed;
    public float addSpeed;
    private float _addSpeed;
    public float addingTime;
    [SerializeField]private bool adding = false;
    public float dir;
    private float speedMultiflier;
    public Transform cam;
    public float cameraMovingTime;
    [SerializeField] private float jumpForce;
    private Collision col;
    public float wallSlideSpeed;
    public bool attacking;

    public bool dragJumped;
    public bool wallJumped;
    [SerializeField] private bool boosted;
    public bool jumpButtonDown = true;
    Vector2 jumpingDir;

    public float lastVelocity;

    [Space]

    [Header("BetterJumping")]

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public bool jumpable;

    [Space]

    [Header("Stamina")]

    public float stamina;
    private float oriStamina;
    public float staminaEater;
    private float _staminaEater;
    public float staminaAdder;
    public float boostStaminaEater;
    private float curBoostStaminaEater;
    public float staminaJumpEater;
    public float staminaWallJumpEater;
    public float staminaDragJumpEater;
    public Image staminaImage;
    public Button.ButtonClickedEvent staminaFunction;
    [SerializeField] private Button.ButtonClickedEvent staminaEat;
    [SerializeField] private Button.ButtonClickedEvent staminaAdd;
    [SerializeField] private Button.ButtonClickedEvent staminaMaintain;
    //[SerializeField] private GameObject lightningParticle;

    [Space]

    [Header("Fever")]

    public float fever;
    private float oriFever;
    [SerializeField] private float feverEater;
    [SerializeField] private bool fevered;
    public Image feverImage;
    public GameObject feverEffect;

    [Space]

    [Header("Input")]

    public bool jump;
    public bool boost;
    public Transform inputColliders;
    [SerializeField] InputController inputController;

    [Space]

    [Header("Effects")]

    [SerializeField] GameObject playerParticle;
    [SerializeField] GameObject deathParticle;
    [SerializeField] GameObject jumpParticle;
    [SerializeField] GameObject dragParticle;
    [SerializeField] GameObject attackTrail;

    // Start is called before the first frame update
    void Start()
    {
        _speed = speed;
        _staminaEater = staminaEater;
        speedMultiflier = 1f;
        col = GetComponent<Collision>();
        oriStamina = stamina;
        oriFever = fever;
        fever = 0;
        StartCoroutine(StaminaCoroutine());
        StartCoroutine(FeverCoroutine());
        //Time.timeScale = 0.2f;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack") && !attacking)
        {
            StartCoroutine(AttackCoroutine());
        }
        if (!col.onWall)
        {
            //if (Input.GetButtonDown("AddSpeed") && !adding)
            //{
            //    adding = true;
            //    staminaFunction = staminaEat;
            //    StartCoroutine(AddingSpeedCoroutine(addingTime));
            //}
            //if (Input.GetButtonUp("AddSpeed") && adding)
            //{
            //    adding = false;
            //    _addSpeed = 0;
            //    curBoostStaminaEater = 0;
            //    if (jumpable) staminaFunction = staminaAdd;
            //}
            rb.velocity = (new Vector2(dir * (_speed + _addSpeed), rb.velocity.y));
            // && !Input.GetButtonDown("Jump")
            if (!jumpButtonDown && rb.velocity.y > 0 && !dragJumped)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y < 0)
            {
                if (jumpButtonDown) jumpButtonDown = false;
                if (!jumpable) jumpable = true;
            }
            //else if (Input.GetButtonUp("Jump"))
            //    jumpButtonDown = false;
            if (!wallJumped && !dragJumped) lastVelocity = rb.velocity.x;
        }
        //else if (Input.GetButtonDown("Jump") && col.wallTag == "WallJump")
        //{
        //    wallJumped = true;
        //    if (col.wall != null) col.wall = null;
        //    stamina -= staminaWallJumpEater;
        //    ChangeCameraPosition();
        //    Jump(-1);
        //    //lightningParticle.SetActive(false);
        //}
        else
        {
            if (!col.onGround && !wallJumped && !dragJumped)
            {
                if (col.wallTag == "DragJump")
                    rb.velocity = boost ? (new Vector2(lastVelocity, 0)) : (new Vector2(0, -wallSlideSpeed));
                else if (col.wallTag == "WallJump")
                    rb.velocity = new Vector2(0, -wallSlideSpeed);
                //if (Input.GetButtonDown("AddSpeed")) lightningParticle.SetActive(true);
                //else if (Input.GetButtonUp("AddSpeed")) lightningParticle.SetActive(false);
            }
        }

        if (!jumpButtonDown)
        {
            //if (Input.GetButtonDown("Jump") && col.onGround)
            //{
            //    Jump(1);
            //    stamina -= staminaJumpEater;
            //}

            if (stamina <= 0 && !dead)
            {
                staminaImage.rectTransform.localScale = new Vector3(stamina / oriStamina, 1, 1);
                dead = true;
                Debug.LogError("Dead");
                cam.SetParent(map);
                GameObject _deathParticle = Instantiate(deathParticle, this.transform.position, Quaternion.identity) as GameObject;
                Destroy(_deathParticle, 3f);
                Destroy(this.gameObject);
            }
        }

        if (Input.GetButtonDown("ChangeDir"))
        {
            ChangeDirection();
        }
    }

    IEnumerator AttackCoroutine ()
    {
        attacking = true;
        //attackParticle.SetActive(true);
        attackTrail.GetComponent<TrailRenderer>().emitting = true;
        float tmp = _speed;
        _speed = 100;
        yield return new WaitForSeconds(0.1f);
        attacking = false;
        _speed = tmp;
        //attackParticle.SetActive(false);
        attackTrail.GetComponent<TrailRenderer>().emitting = false;
    }

    public void Boost()
    {
        if (!col.onWall)
        {
            if (!adding)
            {
                adding = true;
                staminaFunction = staminaEat;
                StartCoroutine(AddingSpeedCoroutine(addingTime));
            }
            else if (adding)
            {
                adding = false;
                _addSpeed = 0;
                curBoostStaminaEater = 0;
                if (jumpable && col.onGround) staminaFunction = staminaAdd;
            }
        }
    }

    public void JumpButtonDown()
    {
        if (col.onGround && !jumpButtonDown)
        {
            Jump(1);
            stamina -= staminaJumpEater;
        }
        else if (col.onWall && col.wallTag == "WallJump")
        {
            wallJumped = true;
            if (col.wall != null) col.wall = null;
            stamina -= staminaWallJumpEater;
            Jump(-1);
            //lightningParticle.SetActive(false);
        }
    }
    public void JumpButtonUp()
    {
        if (!col.onWall)
        {

            jumpButtonDown = false;
        }
    }

    public void Jump(int side)
    {
        if (jumpable && !jumpButtonDown)
        {
            jumpButtonDown = true;
            jumpable = false;
            staminaFunction = staminaMaintain;
            if (side == -1)
            {
                lastVelocity *= -1f;
                if (Mathf.Abs(lastVelocity) > maxSpeed) lastVelocity = Mathf.Sign(lastVelocity) * maxSpeed;
                rb.velocity = new Vector2(0.75f * lastVelocity, 0);
                dir *= -1f;
                ChangeCameraPosition();
            }
            rb.velocity += jumpForce * Vector2.up;
            GameObject _jumpParticle = Instantiate(jumpParticle, this.transform.position, Quaternion.identity) as GameObject;
            Destroy(_jumpParticle, 3f);
        }
    }

    IEnumerator SpeedLerp()
    {
        //Log Function
        //boosted = true;
        //float x = Mathf.Pow(boostingTime, 1 / maxSpeed);
        //float t = Mathf.Pow(x, _speed);
        ////Debug.Log(t);

        //while (t<= boostingTime)
        //{
        //    if (col.onWall)
        //    {
        //        //Debug.Log("SpeedLerp Break");
        //        boosted = false;
        //        break;
        //    }
        //    //Debug.Log(t);
        //    t += Time.deltaTime;
        //    _speed = Mathf.Log(t, x);
        //    //Debug.Log(_speed);
        //    if (t > boostingTime) boosted = false;
        //    yield return new WaitForSeconds(Time.deltaTime);
        //}

        //Ellipse Function
        boosted = true;
        float x = Mathf.Pow(boostingTime, 1 / maxSpeed);
        float t = Mathf.Pow(x, _speed);

        while (t <= boostingTime)
        {
            if (col.onWall)
            {
                boosted = false;
                break;
            }
            if (!attacking)
            {
                t += Time.deltaTime;
                _speed = Mathf.Log(t, x);
                if (t > boostingTime) boosted = false;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    IEnumerator AddingSpeedCoroutine(float lerpTime)
    {
        curBoostStaminaEater = boostStaminaEater;
        float startSpeed = 0;
        float endSpeed = addSpeed;
        for (float t = 0; t <= 1 * lerpTime; t += Time.deltaTime)
        {
            if (!adding)
            {
                _addSpeed = 0;
                curBoostStaminaEater = 0;
                break;
            }
            _addSpeed = Mathf.Lerp(startSpeed, endSpeed, t / lerpTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public void DragJump ()
    {
        if (col.onWall)
        {
            dragJumped = true;
            if (col.wall != null) col.wall = null;
            boosted = false;
            jumpingDir = GetJumpingDirection();
            speedMultiflier = Mathf.Abs(jumpingDir.x);
            _speed = speed * speedMultiflier;
            dir = Mathf.Sign(jumpingDir.x);
            ChangeCameraPosition();
            lastVelocity *= -1f;
            rb.velocity = new Vector2(lastVelocity, 0);
            rb.velocity += jumpForce * jumpingDir;
            stamina -= staminaDragJumpEater;
            staminaFunction = staminaMaintain;
        }
    }
    Vector2 GetJumpingDirection()
    {
        Vector3 camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 tempVector = new Vector3(camPos.x - this.transform.position.x, camPos.y - this.transform.position.y, 0);
        Vector2 _jumpingDir = dir < 0 ? new Vector2(Mathf.Abs(tempVector.normalized.x), tempVector.normalized.y) : new Vector2(-Mathf.Abs(tempVector.normalized.x), tempVector.normalized.y);
        return _jumpingDir;
    }
    public void OnGroundEnterFunction()
    {
        _staminaEater = staminaEater;
        if (adding) staminaFunction = staminaEat;
        else staminaFunction = staminaAdd;
        if (col.onWall)
        {
            Debug.LogError("Dead");
            dead = true;
            cam.SetParent(map);
            GameObject _deathParticle = Instantiate(deathParticle, this.transform.position, Quaternion.identity) as GameObject;
            Destroy(_deathParticle, 3f);
            Destroy(this.gameObject);
        }
        if (!boosted)
        {
            StartCoroutine(SpeedLerp());
        }
        //if (jumpButtonDown)
        //{
        //    jumpButtonDown = false;
        //}
        if (wallJumped) wallJumped = false;
        if (dragJumped) dragJumped = false;
        if (!fevered) fever += stamina;
        if (fever >= oriFever)
        {
            fever = oriFever;
            fevered = true;
            feverEffect.SetActive(true);
        }
    }
    public void OnGroundExitFunction()
    {
        //staminaFunction = staminaEat;
        //staminaFunction = staminaMaintain;
    }
    public void OnWallEnterFunction()
    {
        dragParticle.SetActive(true);
        dragParticle.transform.localPosition = new Vector3(dir * 0.5f, -0.5f, 0);
        playerParticle.SetActive(false);
        if (col.onGround)
        {
            Debug.LogError("Dead");
            dead = true;
            cam.SetParent(map);
            GameObject _deathParticle = Instantiate(deathParticle, this.transform.position, Quaternion.identity) as GameObject;
            Destroy(_deathParticle, 3f);
            Destroy(this.gameObject);
        }
        staminaFunction = staminaEat;
        if (col.wallTag == "WallJump") jumpable = true;
        _staminaEater = col.wall.GetComponent<Wall>().wallStaminaEater;
        wallSlideSpeed = col.wall.GetComponent<Wall>().wallSlideSpeed;
        StopCoroutine(SpeedLerp());
        //if (col.wallTag == "WallJumpable")
        //{

        //}
        //else
        if (adding)
        {
            adding = false;
            _addSpeed = 0;
            curBoostStaminaEater = 0;
        }
        if (stamina == 0)
        {
            dead = true;
            Debug.LogError("Dead");
            cam.SetParent(map);
            GameObject _deathParticle = Instantiate(deathParticle, this.transform.position, Quaternion.identity) as GameObject;
            Destroy(_deathParticle, 3f);
            Destroy(this.gameObject);
        }
        if (jumpButtonDown) jumpButtonDown = false;
        if (wallJumped) wallJumped = false;
        if (dragJumped) dragJumped = false;
    }

    public void OnWallExitFuntion()
    {
        inputController.jumpDir.gameObject.SetActive(false);
        dragParticle.SetActive(false);
        playerParticle.SetActive(true);
    }

    //public void WallJumpedFalse()
    //{
    //    wallJumped = false;
    //}

    void ChangeDirection ()
    {
        dir *= -1f;
        ChangeCameraPosition();
    }
    public void ChangeCameraPosition()
    {
        float tmp = dir == 1 ? 0 : 180;
        inputColliders.localRotation = Quaternion.Euler(inputColliders.localRotation.x, tmp, inputColliders.localRotation.z);
        StopCoroutine(ChangeCameraPositionCoroutine());
        StartCoroutine(ChangeCameraPositionCoroutine());
    }

    Vector3 startPosition;
    Vector3 destination;
    IEnumerator ChangeCameraPositionCoroutine ()
    {
        startPosition = cam.localPosition;
        destination = new Vector3(dir * 3, 2f, -10);
        float elapsedTime = 0f;
        float t;
        while(true)
        {
            if (cam.localPosition == destination)
            {
                break;
            }
            elapsedTime += Time.deltaTime;
            t = Mathf.Clamp(elapsedTime / cameraMovingTime, 0f, 1f);
            //t = Mathf.Sin(elapsedTime / cameraMovingTime * Mathf.PI * 0.5f);
            cam.localPosition = Vector3.Lerp(startPosition, destination, t);
            yield return null;
        }
    }
    IEnumerator StaminaCoroutine ()
    {
        while (!dead)
        {
            staminaFunction.Invoke();
            staminaImage.rectTransform.localScale = new Vector3(stamina / oriStamina, 1, 1);
            //if (stamina <= 0)
            //{
            //    Debug.LogError("Dead");
            //    dead = true;
            //    break;
            //}
            yield return null;
        }
    }
    IEnumerator FeverCoroutine()
    {
        while (!dead)
        {
            fever -= feverEater;
            if (fever <= 0)
            {
                fever = 0;
                fevered = false;
                feverEffect.SetActive(false);
            }
            feverImage.rectTransform.localScale = new Vector3(fever / oriFever, 1, 1);
            yield return null;
        }
    }

    public void StaminaEat ()
    {
        stamina -= (_staminaEater + curBoostStaminaEater);
        if (stamina <= 0) stamina = 0;
    }
    public void StaminaAdd()
    {
        stamina += staminaAdder;
        if (stamina >= oriStamina) stamina = oriStamina;
    }
    public void StaminaMaintain()
    {
        //stamina += 0;
    }
}
