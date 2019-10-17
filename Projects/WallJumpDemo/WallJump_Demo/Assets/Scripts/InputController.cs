using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    public Movement mov;
    [SerializeField] private Collision col;
    [SerializeField] private Camera camera;
    [SerializeField] GameObject touchEffect;
    private LayerMask layer;

    [Space]

    [Header("DragJump")]

    public bool isClicked;
    public Transform jumpDir;

    [Space]

    [Header("Colliders")]

    public Collider2D touchJump;
    //[SerializeField] private Collider2D boost;
    public Collider2D jump;
    public GameObject attackButton;
    private Collider2D colType;

    //[Space]

    //[Header("Functions")]

    //[SerializeField] private Button.ButtonClickedEvent dragjumpDown;
    //[SerializeField] private Button.ButtonClickedEvent dragjumpUp;
    //[SerializeField] private Button.ButtonClickedEvent boostFunction;
    //public Button.ButtonClickedEvent jumpUp;
    //public Button.ButtonClickedEvent jumpDown;

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
            RaycastHit2D hit = Physics2D.Raycast(mPos, 0.1f * Vector2.one, 0.1f, 1 <<  LayerMask.NameToLayer("TouchCollider"));
            Instantiate(touchEffect, camera.ScreenToWorldPoint(Input.mousePosition), Quaternion.Euler(0, 0, 0), camera.transform);
            if (hit)
            {
                colType = hit.collider;
                if (colType == touchJump)
                {
                    //if (col.onWall && col.wallTag == "TouchJump")
                    //{
                        //isClicked = true;
                        //jumpDir.gameObject.SetActive(true);
                    Vector2 vec = GetJumpingDirection();
                    //Debug.Log(vec);
                    mov.Jump(-1, col.wall.GetComponent<Wall>().SetVec(-mov.dir, vec.x, vec.y));
                    //}
                }
                //else if (colType == boost)
                //{
                //    movement.boost = true;
                //    boostFunction.Invoke();
                //}
                else if (colType == jump)
                {
                    mov.jump = true;
                    mov.jumpDown.Invoke();
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            //if (colType == touchJump)
            //{
            //    if (isClicked)
            //    {
            //        //lightningParticle.SetActive(false);
            //        jumpDir.gameObject.SetActive(false);
            //        isClicked = false;
            //        movement.DragJump();
            //    }
            //}
            //else
            //if (colType == boost)
            //{
            //    movement.boost = false;
            //    boostFunction.Invoke();
            //}
            //else
            if (colType == jump)
            {
                mov.jump = false;
                mov.jumpUp.Invoke();
            }
            //else if (colType == attack)
            //{
                
            //}
        }

        if (Input.GetMouseButton(0))
        {
            //if (colType == touchJump)
            //{
            //    Vector2 tmp = GetJumpingDirection();
            //    float r = movement.dir > 0 ? Mathf.Asin(tmp.y) * Mathf.Rad2Deg : (Mathf.PI - Mathf.Asin(tmp.y)) * Mathf.Rad2Deg;
            //    jumpDir.rotation = Quaternion.Euler(0, 0, r);
            //}
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
        Vector2 tempVector = new Vector2(mPos.x - mov.transform.position.x, mPos.y - mov.transform.position.y);
        return tempVector;
    }
}
