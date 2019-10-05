using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    public Movement movement;
    [SerializeField] private Collision col;
    [SerializeField] private Camera camera;
    [SerializeField] GameObject touchEffect;

    [Space]

    [Header("DragJump")]

    public bool isClicked;
    public Transform jumpDir;

    [Space]

    [Header("Colliders")]

    [SerializeField] private Collider2D dragJump;
    [SerializeField] private Collider2D boost;
    [SerializeField] private Collider2D jump;
    [SerializeField] private Collider2D attack;
    private Collider2D colType;

    [Space]

    [Header("Functions")]

    [SerializeField] private Button.ButtonClickedEvent dragjumpDown;
    [SerializeField] private Button.ButtonClickedEvent dragjumpUp;
    [SerializeField] private Button.ButtonClickedEvent boostFunction;
    [SerializeField] private Button.ButtonClickedEvent jumpUp;
    [SerializeField] private Button.ButtonClickedEvent jumpDown;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(camera.ScreenToWorldPoint(Input.mousePosition));
            Vector2 mPos = new Vector2(camera.ScreenToWorldPoint(Input.mousePosition).x, camera.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hit = Physics2D.Raycast(mPos, 0.1f * Vector2.one);
            Instantiate(touchEffect, camera.ScreenToWorldPoint(Input.mousePosition), Quaternion.Euler(0, 0, 0), camera.transform);
            if (hit)
            {
                colType = hit.collider;
                if (colType == dragJump)
                {
                    if (col.onWall && col.wallTag == "DragJump")
                    {
                        isClicked = true;
                        jumpDir.gameObject.SetActive(true);
                    }
                }
                else if (colType == boost)
                {
                    movement.boost = true;
                    boostFunction.Invoke();
                }
                else if (colType == jump)
                {
                    movement.jump = true;
                    jumpDown.Invoke();
                }
                else if (colType == attack)
                {
                    movement.Attack();
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (colType == dragJump)
            {
                if (isClicked)
                {
                    //lightningParticle.SetActive(false);
                    jumpDir.gameObject.SetActive(false);
                    isClicked = false;
                    movement.DragJump();
                }
            }
            else if (colType == boost)
            {
                movement.boost = false;
                boostFunction.Invoke();
            }
            else if (colType == jump)
            {
                movement.jump = false;
                jumpUp.Invoke();
            }
            //else if (colType == attack)
            //{
                
            //}
        }
        else if (Input.GetMouseButton(0))
        {
            if (colType == dragJump)
            {
                Vector2 tmp = GetJumpingDirection();
                float r = movement.dir > 0 ? Mathf.Asin(tmp.y) * Mathf.Rad2Deg : (Mathf.PI - Mathf.Asin(tmp.y)) * Mathf.Rad2Deg;
                jumpDir.rotation = Quaternion.Euler(0, 0, r);
            }
            //else if (tmp == boost)
            //{

            //}
            //else if (tmp == jump)
            //{

            //}
            //else if (colType == attack)
            //{
            //
            //}
        }
     }

    Vector2 GetJumpingDirection()
    {
        Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 tempVector = new Vector3(mPos.x - this.transform.position.x, mPos.y - this.transform.position.y, 0);
        Vector2 _jumpingDir = movement.dir < 0 ? new Vector2(Mathf.Abs(tempVector.normalized.x), tempVector.normalized.y) : new Vector2(-Mathf.Abs(tempVector.normalized.x), tempVector.normalized.y);
        return _jumpingDir;
    }
}
