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
    public float wallSlideSpeed;
    [SerializeField] private Transform jumpDir;

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
    [SerializeField] private bool jumpable;

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

    // Start is called before the first frame update
    void Start()
    {
        _speed = speed;
        _staminaEater = staminaEater;
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
            }
            if (Input.GetButtonUp("AddSpeed") && adding)
            {
                adding = false;
                _addSpeed = 0;
                curBoostStaminaEater = 0;
                if (jumpable) staminaFunction = staminaAdd;
            }
            rb.velocity = (new Vector2(dir * (_speed + _addSpeed), rb.velocity.y));
            //if (rb.velocity.y < 0)
            //{
            //    rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            //}
            //else
            if (!jumpButtonDown && rb.velocity.y > 0 && !dragJumped && !Input.GetButton("Jump"))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y < 0)
            {
                if (jumpButtonDown) jumpButtonDown = false;
                if (!jumpable) jumpable = true;
            }
            else if (Input.GetButtonUp("Jump"))
                jumpButtonDown = false;
            if (!wallJumped && !dragJumped) lastVelocity = rb.velocity.x;
        }
        else if (Input.GetButtonDown("Jump") && col.wallTag == "WallJump")
        {
            wallJumped = true;
            if (col.wall != null) col.wall = null;
            stamina -= staminaWallJumpEater;
            ChangeCameraPosition();
            Jump(-1);
            //lightningParticle.SetActive(false);
        }
        else
        {
            if (!col.onGround && !wallJumped && !dragJumped)
            {
                if (col.wallTag == "DragJump")
                    rb.velocity = Input.GetButton("AddSpeed") ? (new Vector2(lastVelocity, 0)) : (new Vector2(0, -wallSlideSpeed));
                else if (col.wallTag == "WallJump")
                    rb.velocity = new Vector2(0, -wallSlideSpeed);
                //if (Input.GetButtonDown("AddSpeed")) lightningParticle.SetActive(true);
                //else if (Input.GetButtonUp("AddSpeed")) lightningParticle.SetActive(false);
            }
        }

        if (!jumpButtonDown)
        {
            if (Input.GetButtonDown("Jump") && col.onGround)
            {
                Jump(1);
                stamina -= staminaJumpEater;
            }

            if (stamina <= 0 && !dead)
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
            }
            rb.velocity += jumpForce * Vector2.up;
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
            t += Time.deltaTime;
            _speed = Mathf.Log(t, x);
            if (t > boostingTime) boosted = false;
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
        if (col.onWall && col.wallTag == "DragJump")
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
            //lightningParticle.SetActive(false);
            jumpDir.gameObject.SetActive(false);
            dragJumped = true;
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
            lastVelocity *= -1f;
            rb.velocity = new Vector2(lastVelocity, 0);
            rb.velocity += jumpForce * jumpingDir;
            isClicked = false;
            stamina -= staminaDragJumpEater;
            staminaFunction = staminaMaintain;
        }
    }
    public void OnGroundEnterFunction()
    {
        _staminaEater = staminaEater;
        staminaFunction = staminaAdd;
        if (col.onWall)
        {
            Debug.LogError("Dead");
            dead = true;
        }
        if (!boosted)
        {
            StartCoroutine(SpeedLerp());
        }
        if (jumpButtonDown)
        {
            jumpButtonDown = false;
        }
        if (wallJumped) wallJumped = false;
        if (dragJumped) dragJumped = false;
    }
    public void OnGroundExitFunction()
    {
        //staminaFunction = staminaEat;
        //staminaFunction = staminaMaintain;
    }
    public void OnWallEnterFunction()
    {
        if (col.onGround)
        {
            Debug.LogError("Dead");
            dead = true;
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
        if (jumpButtonDown)
        {
            jumpButtonDown = false;
        }
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
        if (wallJumped) wallJumped = false;
        if (dragJumped) dragJumped = false;
    }

    //public void OnWallExitFuntion()
    //{
    //    wallJumped = false;
    //}
    //public void WallJumpedFalse()
    //{
    //    wallJumped = false;
    //}

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
