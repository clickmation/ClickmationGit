using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [Space]

    [Header("Dead")]

    [SerializeField] bool dead;

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
    public bool isClicked;
    [SerializeField] private float jumpForce;
    private Collision col;
    [SerializeField] private float wallSlideSpeed;
    [SerializeField] private Transform jumpDir;

    [SerializeField] bool wallJumped;
    [SerializeField] private bool boosted;
    [SerializeField] private bool jumped = true;
    Vector2 jumpingDir;

    public float lastVelocity;

    [Space]

    [Header("BetterJumping")]

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Space]

    [Header("Stamina")]

    public float stamina;
    private float oriStamina;
    public float staminaEater;
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

    // Start is called before the first frame update
    void Start()
    {
        _speed = speed;
        speedMultiflier = 1f;
        col = GetComponent<Collision>();
        oriStamina = stamina;
        StartCoroutine(StaminaCoroutine());
        //Time.timeScale = 0.2f;
    }
    // Update is called once per frame
    void Update()
    {
        if (!col.onWall)
        {
            if (Input.GetButtonDown("AddSpeed") && !adding)
            {
                adding = true;
                staminaFunction = staminaEat;
                StartCoroutine(AddingSpeedCoroutine(addingTime));
                //Debug.Log("GetButtonDown");
            }
            if (Input.GetButtonUp("AddSpeed") && adding)
            {
                //StopCoroutine(AddingSpeedCoroutine(addingTime));
                adding = false;
                _addSpeed = 0;
                curBoostStaminaEater = 0;
                staminaFunction = staminaAdd;
                //Debug.Log("GetButtonUp");
            }
            rb.velocity = (new Vector2(dir * (_speed + _addSpeed), rb.velocity.y));
            //if (rb.velocity.y < 0)
            //{
            //    rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            //}
            //else
            if (!jumped && rb.velocity.y > 0 && !wallJumped && !Input.GetButton("Jump"))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
            if (!wallJumped) lastVelocity = rb.velocity.x;
        }
        else if (Input.GetButtonDown("Jump") && col.wallTag == "UnWallJumpable")
        {
            wallJumped = true;
            jumped = true;
            if (col.wall != null) col.wall = null;
            stamina -= staminaWallJumpEater;
            ChangeCameraPosition();
            Jump(-1);
        }
        else
        {
            if (!col.onGround && !wallJumped)
            {
                rb.velocity = (new Vector2(0, -wallSlideSpeed));
            }
        }

        if (!jumped)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump(1);
                stamina -= staminaJumpEater;
            }
            //else if (Input.GetButtonUp("Jump"))
            //    jumped = true;

            if (stamina == 0 && !dead)
            {
                dead = true;
                Debug.LogError("Dead");
            }
        }

        if (Input.GetButtonDown("ChangeDir"))
        {
            ChangeDirection();
        }
    }

    public void Jump(int side)
    {
        if (side == -1)
        {
            lastVelocity *= -1f;
            if (Mathf.Abs(lastVelocity) > maxSpeed) lastVelocity = Mathf.Sign(lastVelocity) * maxSpeed;
            rb.velocity = new Vector2(0.75f * lastVelocity, 0);
            dir *= -1f;
        }
        rb.velocity += jumpForce * Vector2.up;
    }

    IEnumerator SpeedLerp()
    {
        boosted = true;
        float x = Mathf.Pow(boostingTime, 1 / maxSpeed);
        float t = Mathf.Pow(x, _speed);
        //Debug.Log(t);

        while (t<= boostingTime)
        {
            if (col.onWall)
            {
                //Debug.Log("SpeedLerp Break");
                boosted = false;
                break;
            }
            //Debug.Log(t);
            t += Time.deltaTime;
            _speed = Mathf.Log(t, x);
            //Debug.Log(_speed);
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

    private void OnMouseDown()
    {
        if (col.onWall && col.wallTag == "WallJumpable")
        {
            isClicked = true;
            jumpDir.gameObject.SetActive(true);
        }
    }

    private void OnMouseDrag()
    {
        Vector2 tmp = GetJumpingDirection();
        float r = dir < 0 ? Mathf.Asin(tmp.y) * Mathf.Rad2Deg : (Mathf.PI - Mathf.Asin(tmp.y)) * Mathf.Rad2Deg;
        jumpDir.rotation = Quaternion.Euler (0, 0, r);
    }

    Vector2 GetJumpingDirection ()
    {
        Vector3 camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 tempVector = new Vector3(camPos.x - this.transform.position.x, camPos.y - this.transform.position.y, 0);
        Vector2 _jumpingDir = dir < 0 ? new Vector2(Mathf.Abs(tempVector.normalized.x), tempVector.normalized.y) : new Vector2(-Mathf.Abs(tempVector.normalized.x), tempVector.normalized.y);
        return _jumpingDir;
    }

    private void OnMouseUp()
    {
        if (isClicked)
        {
            jumpDir.gameObject.SetActive(false);
            wallJumped = true;
            if (col.wall != null) col.wall = null;
            boosted = false;
            jumpingDir = GetJumpingDirection();
            if (jumpingDir.x * dir < 0)
            {
                ChangeCameraPosition();
            }
            speedMultiflier = Mathf.Abs(jumpingDir.x);
            _speed = speed * speedMultiflier;
            dir = Mathf.Sign(jumpingDir.x);
            //Debug.Log(dir);
            lastVelocity *= -1f;
            //Debug.Log(lastVelocity);
            rb.velocity = new Vector2(lastVelocity, 0);
            rb.velocity += jumpForce * jumpingDir;
            isClicked = false;
            stamina -= staminaDragJumpEater;
            //Debug.Log(rb.velocity);
        }
    }
    public void OnGroundEnterFunction()
    {
        staminaFunction = staminaAdd;
        if (col.onWall)
        {
            Debug.LogError("Dead");
            dead = true;
        }
        if (!boosted)
        {
            //Debug.Log("SpeedLerp Start");
            StartCoroutine(SpeedLerp());
        }
        if (jumped)
        {
            jumped = false;
        }
    }
    public void OnGroundExitFunction()
    {
        staminaFunction = staminaEat;
        if (!jumped)
        {
            jumped = true;
        }
    }
    public void OnWallEnterFunction()
    {
        if (col.onGround)
        {
            Debug.LogError("Dead");
            dead = true;
        }
        StopCoroutine(SpeedLerp());
        //if (col.wallTag == "WallJumpable")
        //{

        //}
        //else
        //if (jumped)
        //{
        //    jumped = false;
        //}
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
        }
    }
    //public void OnWallExitFuntion()
    //{
    //    wallJumped = false;
    //}
    public void WallJumpedFalse()
    {
        wallJumped = false;
    }

    void ChangeDirection ()
    {
        ChangeCameraPosition();
        dir *= -1f;
    }
    public void ChangeCameraPosition()
    {
        StopCoroutine(ChangeCameraPositionCoroutine());
        StartCoroutine(ChangeCameraPositionCoroutine());
    }

    Vector3 startPosition;
    Vector3 destination;
    IEnumerator ChangeCameraPositionCoroutine ()
    {
        startPosition = cam.localPosition;
        //Debug.Log(startPosition);
        destination = new Vector3(-dir * 3, 2f, -10);
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

    public void StaminaEat ()
    {
        stamina -= (staminaEater + curBoostStaminaEater);
        if (stamina <= 0) stamina = 0;
    }
    public void StaminaAdd()
    {
        stamina += staminaAdder;
        if (stamina >= oriStamina) stamina = oriStamina;
    }
}
