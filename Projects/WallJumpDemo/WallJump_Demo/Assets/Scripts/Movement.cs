﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public Camera2DFollow camFol;
    public CameraShake camShake;
    GameMaster gm;

    [Space]

    [Header("UI")]

    [SerializeField] GameObject[] trails;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Sprite[] sprites;
    [SerializeField] GameObject shockWaveJump;
    public GameObject shockWaveKill;
    public Button.ButtonClickedEvent jumpUp;
    public Button.ButtonClickedEvent jumpDown;
    public Button.ButtonClickedEvent attackFunction;

    //[SerializeField] GameObject deadPanel;
    //[SerializeField] GameObject pausePanel;

    //[Space]

    //[Header("Bool")]

    //[SerializeField] bool onGround;
    //[SerializeField] bool onWall;

    [Space]

    [Header("Default")]

    public Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private RotationController rotCon;
    public float speed;
    [SerializeField] private float _speed;
    //public float boostingTime;
    //public float maxSpeed;
    //public float addSpeed;
    //private float _addSpeed;
    //public float addingTime;
    //[SerializeField]private bool adding = false;
    public float dir;
    private float speedMultiflier;
    public Transform cam;
    public float cameraMovingTime;
    [SerializeField] private float jumpForce;
    private Collision col;
    public float wallSlideSpeed;
    public bool attacking;
    public bool attackable;
    public float attackTime;
    public float attackDelay;
    private float gravity;

    public bool touchJumped;
    public bool wallJumped;
    [SerializeField] private bool boosted;
    public bool jumping = true;
    private bool _jumping;
    [SerializeField] private bool jumpButtonDown;
    Vector2 jumpingDir;
    public GameObject jumpingArrow;

    //public float lastVelocity;
    public float lastSpeed;

    [Space]

    [Header("BetterJumping")]

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public bool jumpable;
    public bool panelJumped;

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
    //public Transform inputController;
    public InputController inputController;

    [Space]

    [Header("Effects")]

    [SerializeField] GameObject playerParticle;
    [SerializeField] GameObject jumpParticle;
    [SerializeField] GameObject dragParticle;
    [SerializeField] GameObject attackTrail;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameMaster.gameMaster;
        playerParticle = Instantiate(trails[PlayerPrefs.GetInt("CurTrailIndex")], transform);
        sprite.sprite = sprites[PlayerPrefs.GetInt("CurCharacterIndex")];
        _speed = speed;
        speedMultiflier = 1f;
        col = GetComponent<Collision>();
        oriFever = fever;
        fever = 0;
        gravity = rb.gravityScale;
        camFol.dir = dir;
        GameObject _deathParticle = Instantiate(gm.deathParticle, this.transform.position, Quaternion.identity) as GameObject;
        Destroy(_deathParticle, 3f);
        //StartCoroutine(StaminaCoroutine());
        //StartCoroutine(FeverCoroutine());
        //Time.timeScale = 0.2f;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        animator.SetFloat("Yvel", rb.velocity.y);

        if (Input.GetButtonDown("ChangeDir"))
        {
            dir *= -1f;
            camFol.dir = dir;
        }
        if (Input.GetButtonDown("Attack") && attackable)
        {
            Attack();
        }
        if (!col.onWall)
        {

           if (!panelJumped) rb.velocity = (new Vector2(dir * _speed, rb.velocity.y));
            if (!jumping && rb.velocity.y > 0 && !touchJumped && !panelJumped && !attacking)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y < 0)
            {
                if (jumping) jumping = false;
                if (jumping != _jumping) rotCon.SetRotation(0.2f, Vector2.zero);
                _jumping = jumping;
            }
            //if (!wallJumped && !touchJumped) lastVelocity = rb.velocity.x;
        }
        else
        {
            if (!col.onGround && !wallJumped && !touchJumped)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }

        //if (!jumping)
        //{

        //}
    }

    public void Attack()
    {
        if (attackable) StartCoroutine(AttackCoroutine());
    }

    public IEnumerator AttackCoroutine ()
    {
        attacking = true;
        attackable = false;
        if (panelJumped) panelJumped = false;
        //float tmpV = rb.velocity.y;
        rb.velocity = (new Vector2(dir * _speed, 0));
        rb.gravityScale = 0;
        _speed = 0;
        yield return new WaitForSeconds(0.1f);
        attackTrail.GetComponent<TrailRenderer>().emitting = true;
        AudioManager.PlaySound("attack");
        Instantiate(shockWaveJump, transform.position, Quaternion.Euler(0, 0, 0));
        _speed = 50;
        yield return new WaitForSeconds(attackTime);
        _speed = speed;
        if (!jumping) rb.velocity = (new Vector2(dir * _speed, 0));
        rb.gravityScale = gravity;
        attacking = false;
        attackTrail.GetComponent<TrailRenderer>().emitting = false;
        StartCoroutine(gm.AttackCoolTimeBarCoroutine(attackDelay));
    }

    //public void Boost()
    //{
    //    if (!col.onWall)
    //    {
    //        if (!adding)
    //        {
    //            adding = true;
    //            staminaFunction = staminaEat;
    //            StartCoroutine(AddingSpeedCoroutine(addingTime));
    //        }
    //        else if (adding)
    //        {
    //            adding = false;
    //            _addSpeed = 0;
    //            curBoostStaminaEater = 0;
    //            if (jumpable && col.onGround) staminaFunction = staminaAdd;
    //        }
    //    }
    //}

    public void JumpButtonDown()
    {
        jumpButtonDown = true;
        if (col.onGround && !jumping)
        {
            AudioManager.PlaySound("groundJump");
            Jump(1, Vector2.zero);
        }
        else if (col.onWall && col.wallTag == "WallJump")
        {
            gm.WallJump();
            Jump(-1, Vector2.zero);
            AudioManager.PlaySound("groundJump");
            //lightningParticle.SetActive(false);
        }
    }
    public void JumpButtonUp()
    {
        if (!panelJumped) jumping = false;
        jumpButtonDown = false;
    }

    public void Jump(int side, Vector2 vec)
    {
        if (jumpable && !jumping)
        {
            jumpable = false;
            jumping = true;
            _jumping = jumping;
            if (side == -1)
            {
                //lastVelocity *= -1f;
                dir *= -1f;
                //if (Mathf.Abs(lastVelocity) > maxSpeed && !panelJumped) lastVelocity = Mathf.Sign(lastVelocity) * maxSpeed;
                if (vec == Vector2.zero)
                {
                    wallJumped = true;
                    _speed = speed;
                    rb.velocity = new Vector2(dir * _speed, 0);
                    rb.velocity += jumpForce * Vector2.up;
                    rotCon.SetRotation(0.2f, rb.velocity);
                    //Debug.Log(dir + ", " + rb.velocity);
                    // WallJump
                }
                else
                {
                    touchJumped = true;
                    if (col.wall != null) col.wall = null;
                    panelJumped = true;
                    //adding = false;
                    //_addSpeed = 0;
                    //lastSpeed = _speed;
                    _speed = Mathf.Abs(vec.x);
                    rb.velocity = new Vector2(0, 0);
                    rb.velocity += vec;
                    rotCon.SetRotation(0.2f, rb.velocity);
                    // TouchJump
                 }
            if (col.wall != null) col.wall = null;
            }
            else
            {
                if (panelJumped)
                {
                    camFol.dir = Mathf.Sign(vec.x);
                    dir = camFol.dir;
                    rb.velocity = new Vector2(0, 0);
                    rb.velocity += vec;
                    rotCon.SetRotation(0.2f, rb.velocity);
                }
                else
                {
                    if (vec == Vector2.zero)
                    {
                        _speed = speed;
                        rb.velocity = new Vector2(dir * _speed, 0);
                        rb.velocity += jumpForce * Vector2.up;
                    }
                    else
                    {
                        panelJumped = true;
                        rb.velocity = Vector2.zero;
                        rb.velocity += vec;
                        //Debug.Log(rb.velocity);
                    }
                }
            }
            Instantiate(shockWaveJump, transform.position, Quaternion.Euler(0, 0, 0));
            camShake.Shake(5);
            GameObject _jumpParticle = Instantiate(jumpParticle, this.transform.position, Quaternion.identity) as GameObject;
            Destroy(_jumpParticle, 3f);
            //Debug.Log("Jumped");
        }
    }

    //IEnumerator SpeedLerp()
    //{
    //    boosted = true;
    //    float x = Mathf.Pow(boostingTime, 1 / maxSpeed);
    //    float t = Mathf.Pow(x, _speed);

    //    while (t <= boostingTime)
    //    {
    //        if (col.onWall)
    //        {
    //            boosted = false;
    //            break;
    //        }
    //        if (!attacking && !panelJumped)
    //        {
    //            t += Time.deltaTime;
    //            _speed = Mathf.Log(t, x);
    //            if (t > boostingTime) boosted = false;
    //        }
    //        yield return new WaitForSeconds(Time.deltaTime);
    //    }
    //}
    //IEnumerator AddingSpeedCoroutine(float lerpTime)
    //{
    //    curBoostStaminaEater = boostStaminaEater;
    //    float startSpeed = 0;
    //    float endSpeed = addSpeed;
    //    for (float t = 0; t <= 1 * lerpTime; t += Time.deltaTime)
    //    {
    //        if (!adding)
    //        {
    //            _addSpeed = 0;
    //            curBoostStaminaEater = 0;
    //            break;
    //        }
    //        _addSpeed = Mathf.Lerp(startSpeed, endSpeed, t / lerpTime);
    //        yield return new WaitForSeconds(Time.deltaTime);
    //    }
    //}

    //public void DragJump ()
    //{
    //    if (col.onWall)
    //    {
    //        touchJumped = true;
    //        if (col.wall != null) col.wall = null;
    //        boosted = false;
    //        jumpingDir = GetJumpingDirection();
    //        speedMultiflier = Mathf.Abs(jumpingDir.x);
    //        _speed = speed * speedMultiflier;
    //        dir = Mathf.Sign(jumpingDir.x);
    //        lastVelocity *= -1f;
    //        rb.velocity = new Vector2(lastVelocity, 0);
    //        rb.velocity += jumpForce * jumpingDir;
    //        stamina -= staminaTouchJumpEater;
    //        staminaFunction = staminaMaintain;
    //    }
    //}
    Vector2 GetJumpingDirection()
    {
        Vector3 camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 tempVector = new Vector3(camPos.x - this.transform.position.x, camPos.y - this.transform.position.y, 0);
        Vector2 _jumpingDir = dir > 0 ? new Vector2(Mathf.Abs(tempVector.normalized.x), tempVector.normalized.y) : new Vector2(-Mathf.Abs(tempVector.normalized.x), tempVector.normalized.y);
        return _jumpingDir;
    }
    public void OnGroundEnterFunction()
    {
        if (!jumpable) jumpable = true;
        if (panelJumped)
        {
            panelJumped = false;
        }
        else
        {
            if (jumpButtonDown)
            {
                AudioManager.PlaySound("groundJump");
                Jump(1, Vector2.zero);
            }
            AudioManager.PlaySound("landing");
            //Debug.Log("Entered");
        }
        if (col.onWall) gm.Dead();
        if (wallJumped) wallJumped = false;
        if (touchJumped) touchJumped = false;
        if (fever >= oriFever)
        {
            fever = oriFever;
            fevered = true;
            feverEffect.SetActive(true);
        }
        _speed = speed;
        gm.StaminaActiveFalse();
    }
    public void OnGroundExitFunction()
    {
        lastSpeed = _speed;
        //Debug.Log("Exited");
    }
    public void OnWallEnterFunction()
    {
        //Debug.Log("OnWallEnter");
        dragParticle.SetActive(true);
        dragParticle.transform.localPosition = new Vector3(dir * 0.5f, -0.5f, 0);
        playerParticle.SetActive(false);
        camFol.dir = -dir;
        rotCon.SetRotation(0f, new Vector2 (0, 1));
        if (col.onGround)
        {
            gm.Dead();
        }
        //camFol.updateLookAheadTarget = !camFol.updateLookAheadTarget;
        if (col.wall.GetComponent<Wall>() != null)
        {
            wallSlideSpeed = col.wall.GetComponent<Wall>().wallSlideSpeed;
        }
        //StopCoroutine(SpeedLerp());
        //if (adding)
        //{
        //    adding = false;
        //    _addSpeed = 0;
        //    curBoostStaminaEater = 0;
        //}
        if (jumping) jumping = false;
        if (wallJumped) wallJumped = false;
        if (touchJumped) touchJumped = false;
        if (col.wallTag == "WallJump")
        {
            jumpable = true;
            panelJumped = false;
            gm.StaminaActiveTrue(col.wall.GetComponent<Wall>().wallStaminaCount);
        }
        else if (col.wallTag == "TouchJump")
        {
            jumpable = true;
            panelJumped = false;
            inputController.touchJump.gameObject.SetActive(true);
            inputController.jump.gameObject.SetActive(false);
            inputController.attack.gameObject.SetActive(false);
            //inputController.attackButton.SetActive(false);
            jumpingArrow.SetActive(true);
            jumpingArrow.transform.rotation = Quaternion.Euler (0, 90 + dir * 90, 0);
            gm.StaminaActiveTrue(col.wall.GetComponent<Wall>().wallStaminaCount);
        }
        //else if (col.wallTag == "WallPanel")
        //{
        //    jumpable = true;
        //    wallJumped = true;
        //    panelJumped = true;
        //    AudioManager.PlaySound("wallPanel");
        //    Jump(-1, Vector2.zero);
        //}
    }

    public void OnWallExitFuntion()
    {
        if (!jumping)
        {
            camFol.dir *= -1;
            //dir = camFol.dir;
        }
        inputController.jump.gameObject.SetActive(true);
        inputController.attack.gameObject.SetActive(true);
        //inputController.attackButton.SetActive(true);
        //}
        //inputController.jumpDir.gameObject.SetActive(false);
        dragParticle.SetActive(false);
        //dragParticle.GetComponent<ParticleSystem>().emission.enabled = false;
        playerParticle.SetActive(true);
        jumpingArrow.SetActive(false);
    }

    //public void WallJumpedFalse()
    //{
    //    wallJumped = false;
    //}

    //public void ShockWaveSpawnFunction (float scale)
    //{
    //    GameObject sw = Instantiate(shockWave, transform.position, Quaternion.Euler(0, 0, 0));
    //    sw.transform.localScale = 10 * scale * Vector3.one;
    //}

    //IEnumerator StaminaCoroutine()
    //{
    //    while (!GameMaster.gameMaster.dead)
    //    {
    //        staminaFunction.Invoke();
    //        staminaImage.rectTransform.localScale = new Vector3(stamina / oriStamina, 1, 1);
    //        if (stamina <= 0)
    //        {
    //            Debug.LogError("Dead");
    //            GameMaster.gameMaster.Dead();
    //            break;
    //        }
    //        yield return new WaitForFixedUpdate();
    //    }
    //}
    //IEnumerator FeverCoroutine()
    //{
    //    while (!dead)
    //    {
    //        fever -= feverEater;
    //        if (fever <= 0)
    //        {
    //            fever = 0;
    //            fevered = false;
    //            feverEffect.SetActive(false);
    //        }
    //        feverImage.rectTransform.localScale = new Vector3(fever / oriFever, 1, 1);
    //        yield return new WaitForFixedUpdate();
    //    }
    //}

    //public void StaminaEat()
    //{
    //    stamina -= _staminaEater;
    //    if (stamina <= 0) stamina = 0;
    //}
    //public void StaminaAdd()
    //{
    //    stamina += staminaAdder;
    //    if (stamina >= oriStamina) stamina = oriStamina;
    //}
    //public void StaminaMaintain()
    //{
    //    //stamina += 0;
    //}

    public void OnCollisionEnter2D (Collision2D other)
    {
        if ((transform.position.y - other.transform.position.y) < 0.4f && !col.onGround)
        {
            _speed = 0;
            //Debug.Log("Hey");
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if (_speed == 0) {
            _speed = speed;
        }
    }
}
