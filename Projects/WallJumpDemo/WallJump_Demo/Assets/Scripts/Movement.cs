using System.Collections;
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
    [SerializeField] private Transform attackCoolTimeBar;

    [Space]

    [Header("Default")]

    public Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private RotationController rotCon;
    public float speed;
    [SerializeField] private float _speed;
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
    public float attackCoolTime;
    [SerializeField] float deathYPos;
    [SerializeField] float deathYOffset;
    private float gravity;

    public bool touchJumped;
    public bool wallJumped;
    [SerializeField] private bool boosted;
    public bool jumping = true;
    [SerializeField] private bool jumpButtonDown;
    Vector2 jumpingDir;
    public GameObject jumpingArrow;
    private bool rezero;
    private float rotZ;
    private float yVel;
    [SerializeField] private Transform rotTransform;

    public float lastSpeed;
    bool particleTrail;

    [Space]

    [Header("BetterJumping")]

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public bool jumpable;
    public bool panelJumped;

    [Space]

    [Header("Input")]

    public bool jump;
    public bool boost;
    public InputController inputController;

    [Space]

    [Header("Effects")]

    [SerializeField] GameObject playerTrail;
    [SerializeField] GameObject jumpParticle;
    [SerializeField] GameObject dragParticle;
    [SerializeField] GameObject attackTrail;

    // Start is called before the first frame update
    void Start()
    {
        if (GameMaster.gameMaster != null) gm = GameMaster.gameMaster;
        else Debug.LogError("There's no GameMaster.");
        playerTrail = Instantiate(trails[PlayerPrefs.GetInt("CurTrailIndex")], transform);
        if (PlayerPrefs.GetInt("CurTrailIndex") < 7) particleTrail = true;
        sprite.sprite = sprites[PlayerPrefs.GetInt("CurCharacterIndex")];
        _speed = speed;
        speedMultiflier = 1f;
        if (GetComponent<Collision>() != null) col = GetComponent<Collision>();
        else Debug.LogError("There's no Collision.");
        gravity = rb.gravityScale;
        camFol.dir = dir;
        GameObject _deathParticle = Instantiate(gm.deathParticle, this.transform.position, Quaternion.identity) as GameObject;
        Destroy(_deathParticle, 3f);
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
            if (rezero) rotTransform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(0, rotZ, rb.velocity.y / yVel));
            if (!jumping && rb.velocity.y > 0 && !touchJumped && !attacking)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y < 0)
            {
                if (jumping) jumping = false;
                rezero = false;
            }
        }
        else
        {
            if (!col.onGround && !wallJumped && !touchJumped)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }

        if (transform.position.y < deathYPos) gm.Dead();
    }

    public void Rezero()
    {
        rotZ = Mathf.Rad2Deg * rotTransform.rotation.z;
        yVel = Mathf.Abs(rb.velocity.y);
        rezero = true;
    }

    public void RezeroStop()
    {
        rezero = false;
        rotTransform.rotation = Quaternion.Euler(0, 0, 0);
    }

    IEnumerator attackCoroutine;
    public void Attack()
    {
        if (attackable)
        {
            attackCoroutine = AttackCoroutine();
            animator.SetTrigger("Attack");
            rotCon.SetRotation(0, Vector2.zero, false);
            StartCoroutine(attackCoroutine);
        }
    }

    public void StopAttack ()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            _speed = speed;
            rb.velocity = (new Vector2(dir * _speed, 0));
            rb.gravityScale = gravity;
            attacking = false;
            attackTrail.GetComponent<TrailRenderer>().emitting = false;
            StartCoroutine(AttackCoolTimeBarCoroutine(attackCoolTime));
        }
    }

    public IEnumerator AttackCoroutine ()
    {
        attacking = true;
        attackable = false;
        if (panelJumped) panelJumped = false;
        rb.velocity = (new Vector2(dir * _speed, 0));
        rb.gravityScale = 0;
        _speed = 0;
        yield return new WaitForSeconds(0.1f);
        attackTrail.GetComponent<TrailRenderer>().emitting = true;
        AudioManager.PlaySound("attack");
        gm.SpawnShockWave(shockWaveJump, 0.25f);
        _speed = 50;
        yield return new WaitForSeconds(attackTime);
        _speed = speed;
        rb.velocity = (new Vector2(dir * _speed, 0));
        rb.gravityScale = gravity;
        attacking = false;
        attackTrail.GetComponent<TrailRenderer>().emitting = false;
        StartCoroutine(AttackCoolTimeBarCoroutine(attackCoolTime));
    }

    public IEnumerator AttackCoolTimeBarCoroutine(float coolTime)
    {
        attackCoolTimeBar.gameObject.SetActive(true);
        for (float t = 0; t < coolTime; t += Time.deltaTime)
        {
            attackCoolTimeBar.localScale = new Vector3(Mathf.Lerp(0, 1, t / coolTime), 0.15f, 1);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        attackCoolTimeBar.gameObject.SetActive(false);
        attackable = true;
    }

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
            if (gm.staminaCount != 0)
            {
                gm.WallJump();
                Jump(-1, Vector2.zero);
                AudioManager.PlaySound("groundJump");
            }
            else
            {
                gm.SpawnNoMoreStamina();
            }
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
            rb.gravityScale = gravity;
            gm.Jumpcount(1);
            if (side == -1)
            {
                dir *= -1f;
                if (vec == Vector2.zero)
                {
                    wallJumped = true;
                    _speed = speed;
                    rb.velocity = new Vector2(dir * _speed, 0);
                    rb.velocity += jumpForce * Vector2.up;
                    rotCon.SetRotation(0.1f, rb.velocity,true);
                }
                else
                {
                    touchJumped = true;
                    if (col.wall != null) col.wall = null;
                    panelJumped = true;
                    _speed = Mathf.Abs(vec.x);
                    rb.velocity = new Vector2(0, 0);
                    rb.velocity += vec;
                    rotCon.SetRotation(0.1f, rb.velocity, true);
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
                    rotCon.SetRotation(0.1f, rb.velocity, true);
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
                    }
                }
            }
            gm.SpawnShockWave(shockWaveJump, 0.25f);
            camShake.Shake(5);
            GameObject _jumpParticle = Instantiate(jumpParticle, this.transform.position, Quaternion.identity) as GameObject;
            Destroy(_jumpParticle, 3f);
        }
    }

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
        }
        if (col.onWall) gm.Dead();
        if (wallJumped) wallJumped = false;
        if (touchJumped) touchJumped = false;
        _speed = speed;
        gm.StaminaActiveFalse();
    }
    public void OnGroundExitFunction()
    {
        lastSpeed = _speed;
    }
    public void OnWallEnterFunction()
    {
        dragParticle.SetActive(true);
        dragParticle.transform.localPosition = new Vector3(dir * 0.5f, -0.5f, 0);
        if (particleTrail) playerTrail.SetActive(false);
        else playerTrail.GetComponent<TrailRenderer>().emitting = false;
        camFol.dir = -dir;
        RezeroStop();
        rotCon.SetRotation(0f, new Vector2 (0, -1), false);
        if (col.onGround)
        {
            gm.Dead();
        }
        if (col.wall.GetComponent<Wall>() != null)
        {
            wallSlideSpeed = col.wall.GetComponent<Wall>().wallSlideSpeed;
        }
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
            jumpingArrow.SetActive(true);
            jumpingArrow.transform.rotation = Quaternion.Euler (0, 90 + dir * 90, 0);
            gm.StaminaActiveTrue(col.wall.GetComponent<Wall>().wallStaminaCount);
        }
    }

    public void OnWallExitFuntion()
    {
        if (!jumping) camFol.dir *= -dir;
        if (!jumping) rotCon.SetRotation(0.2f, Vector2.zero, false);
        inputController.jump.gameObject.SetActive(true);
        inputController.attack.gameObject.SetActive(true);
        dragParticle.SetActive(false);
        if (particleTrail) playerTrail.SetActive(true);
        else playerTrail.GetComponent<TrailRenderer>().emitting = true;
        jumpingArrow.SetActive(false);
    }

    public void SetDeathYPosition(float startY, float endY)
    {
        float tmp = startY < endY ? startY : endY;
        deathYPos = tmp + deathYOffset;
    }

    public void OnCollisionEnter2D (Collision2D other)
    {
        if ((transform.position.y - other.transform.position.y) < 0.4f && !col.onGround)
        {
            _speed = 0;
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if (_speed == 0) {
            _speed = speed;
        }
    }
}
