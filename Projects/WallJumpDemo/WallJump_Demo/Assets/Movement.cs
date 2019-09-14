using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float speedBoostTime;
    private float _speed;
    public float dir;
    public Transform cam;
    public float cameraMovingTime;
    public bool isClicked;
    [SerializeField] private float jumpForce;
    private Collision col;
    [SerializeField] private float wallSlideSpeed;
    [SerializeField] private Transform jumpDir;

    [SerializeField] bool wallJumped;
    public bool boosted;
    bool jumped;
    Vector2 jumpingDir;

    public float lastVelocity;

    [Space]

    [Header("BetterJumping")]

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    // Start is called before the first frame update
    void Start()
    {
        _speed = speed;
        col = GetComponent<Collision>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!col.onWall)
        {
            rb.velocity = (new Vector2(dir * _speed, rb.velocity.y));
            //if (rb.velocity.y < 0)
            //{
            //    rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            //}
            //else
            if (!jumped && rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
            lastVelocity = rb.velocity.x;
        }
        else if (Input.GetButtonDown("Jump") && col.wallTag == "UnWallJumpable")
        {
            wallJumped = true;
            if (col.wall != null) col.wall = null;
            ChangeCameraPosition();
            Jump(-1);
        }
        else
        {
            if (!col.onGround && !wallJumped)
            {
                rb.velocity = (new Vector2(rb.velocity.x, -wallSlideSpeed));
            }
        }
        if (!jumped)
        {
            if (Input.GetButtonDown("Jump"))
                Jump(1);
            else if (Input.GetButtonUp("Jump"))
                jumped = true;
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
            rb.velocity = new Vector2(-0.75f * lastVelocity, 0);
            dir *= -1f;
        }
        rb.velocity += jumpForce * Vector2.up;
    }

    IEnumerator SpeedLerp (float lerpTime)
    {
        boosted = true;
        float startSpeed = speed;
        float endSpeed = speed * 1.5f;
        float t = 0;
        while (rb.velocity.x < endSpeed)
        {
            t += Time.deltaTime;
            _speed = Mathf.Lerp(startSpeed, endSpeed, t/lerpTime);
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
        float r = tmp.x > 0 ? Mathf.Asin(tmp.y) * Mathf.Rad2Deg : (Mathf.PI - Mathf.Asin(tmp.y)) * Mathf.Rad2Deg;
        jumpDir.rotation = Quaternion.Euler (0, 0, r);
    }

    Vector2 GetJumpingDirection ()
    {
        Vector3 camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 tempVector = new Vector3(camPos.x - this.transform.position.x, camPos.y - this.transform.position.y, 0);
        Vector2 _jumpingDir = new Vector2(tempVector.normalized.x, tempVector.normalized.y);
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
            _speed = speed;
            jumpingDir = GetJumpingDirection();
            if (jumpingDir.x * dir < 0)
            {
                ChangeCameraPosition();
            }
            dir = jumpingDir.x;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity += jumpForce * jumpingDir;
            isClicked = false;
            //Debug.Log(rb.velocity);
        }
    }
    public void OnGroundFunction()
    {
        if (!boosted)
        {
            StartCoroutine(SpeedLerp(speedBoostTime));
        }
        if (jumped)
        {
            jumped = false;
        }
    }
    public void OnWallEnter()
    {
        //lastVelocity = rb.velocity.x;
        StopCoroutine(SpeedLerp(speedBoostTime));
        //if (col.wallTag == "WallJumpable")
        //{

        //}
        //else
        if (jumped)
        {
            jumped = false;
        }
    }
    //public void OnWallExit()
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
}
