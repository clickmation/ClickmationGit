using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    public Movement mov;
    public Collision col;
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
    public Collider2D jump;
    public Collider2D attack;
    //private List<Collider2D> colTypes = new List<Collider2D>();
    private Collider2D tmpCollider;
    public Text testText;

    //[Space]

    //[Header("Functions")]

    //[SerializeField] private Button.ButtonClickedEvent dragjumpDown;
    //[SerializeField] private Button.ButtonClickedEvent dragjumpUp;
    //[SerializeField] private Button.ButtonClickedEvent boostFunction;
    //public Button.ButtonClickedEvent jumpUp;
    //public Button.ButtonClickedEvent jumpDown;

    // Update is called once per frame
    void Update()
    {
        //Mouse
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(camera.ScreenToWorldPoint(Input.mousePosition));
            Vector2 mPos = new Vector2(camera.ScreenToWorldPoint(Input.mousePosition).x, camera.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hit = Physics2D.Raycast(mPos, 0.1f * Vector2.one, 0.1f, 1 << LayerMask.NameToLayer("TouchCollider"));
            Instantiate(touchEffect, camera.ScreenToWorldPoint(Input.mousePosition), Quaternion.Euler(0, 0, 0), camera.transform);

            if (hit)
            {
                tmpCollider = hit.collider;
                //else if (colType == boost)
                //{
                //    movement.boost = true;
                //    boostFunction.Invoke();
                //}
                if (tmpCollider == jump)
                {
                    mov.jump = true;
                    mov.jumpDown.Invoke();
                }
                else if (tmpCollider == attack)
                {
                    mov.Attack();
                }
                else if (tmpCollider == touchJump)
                {
                    //if (col.onWall && col.wallTag == "TouchJump")
                    //{
                    //  isClicked = true;
                    //  jumpDir.gameObject.SetActive(true);
                    Vector2 vec = GetJumpingDirection();
                    //  Debug.Log(vec);
                    touchJump.gameObject.SetActive(false);
                    mov.Jump(-1, col.wall.GetComponent<Wall>().SetVec(-mov.dir, vec.x, vec.y));
                    //  }
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
            if (tmpCollider == jump)
            {
                mov.jump = false;
                mov.jumpUp.Invoke();
            }
            //else if (colType == attack)
            //{

            //}
        }

        //MultiTouch(OldVersion)
        //for (int i = 0; i < Input.touchCount; i++)
        //{
        //    if (Input.GetTouch(i).phase == TouchPhase.Began)
        //    {
        //        Ray ray = camera.ScreenPointToRay(camera.ScreenToWorldPoint(Input.GetTouch(i).position));
        //        Vector2 mPos = new Vector2(camera.ScreenToWorldPoint(Input.GetTouch(i).position).x, camera.ScreenToWorldPoint(Input.GetTouch(i).position).y);
        //        RaycastHit2D hit = Physics2D.Raycast(mPos, 0.1f * Vector2.one, 0.1f, 1 << LayerMask.NameToLayer("TouchCollider"));
        //        Instantiate(touchEffect, camera.ScreenToWorldPoint(Input.GetTouch(i).position), Quaternion.Euler(0, 0, 0), camera.transform);



        //        if (hit)
        //        {
        //            colTypes.Add(hit.collider);
        //            //else if (colType == boost)
        //            //{
        //            //    movement.boost = true;
        //            //    boostFunction.Invoke();
        //            //}
        //            if (colTypes[i] == jump)
        //            {
        //                mov.jump = true;
        //                mov.jumpDown.Invoke();
        //            }
        //            else if (colTypes[i] == attack)
        //            {
        //                mov.Attack();
        //            }
        //            else if (colTypes[i] == touchJump)
        //            {
        //                //if (col.onWall && col.wallTag == "TouchJump")
        //                //{
        //                //  isClicked = true;
        //                //  jumpDir.gameObject.SetActive(true);
        //                Vector2 vec = GetJumpingDirection();
        //                //  Debug.Log(vec);
        //                touchJump.gameObject.SetActive(false);
        //                mov.Jump(-1, col.wall.GetComponent<Wall>().SetVec(-mov.dir, vec.x, vec.y));
        //                //  }
        //            }
        //        }
        //    }
        //    if (Input.GetTouch(i).phase == TouchPhase.Ended)
        //    {

        //        //if (colType == touchJump)
        //        //{
        //        //    if (isClicked)
        //        //    {
        //        //        //lightningParticle.SetActive(false);
        //        //        jumpDir.gameObject.SetActive(false);
        //        //        isClicked = false;
        //        //        movement.DragJump();
        //        //    }
        //        //}
        //        //else
        //        //if (colType == boost)
        //        //{
        //        //    movement.boost = false;
        //        //    boostFunction.Invoke();
        //        //}
        //        //else
        //        if (colTypes[i] == jump)
        //        {
        //            mov.jump = false;
        //            mov.jumpUp.Invoke();
        //        }
        //        //else if (colType == attack)
        //        //{

        //        //}
        //        colTypes.RemoveAt(i);
        //    }
        //}

        //MultiTouch (New Version)
        //for (int i = 0; i < Input.touchCount; i++)
        //{
        //    if (Input.GetTouch(i).phase == TouchPhase.Began)
        //    {
        //        Ray ray = camera.ScreenPointToRay(camera.ScreenToWorldPoint(Input.GetTouch(i).position));
        //        Vector2 mPos = new Vector2(camera.ScreenToWorldPoint(Input.GetTouch(i).position).x, camera.ScreenToWorldPoint(Input.GetTouch(i).position).y);
        //        RaycastHit2D hit = Physics2D.Raycast(mPos, 0.1f * Vector2.one, 0.1f, 1 << LayerMask.NameToLayer("TouchCollider"));
        //        Instantiate(touchEffect, camera.ScreenToWorldPoint(Input.GetTouch(i).position), Quaternion.Euler(0, 0, 0), camera.transform);



        //        if (hit)
        //        {
        //            tmpCollider = hit.collider;
        //            //else if (colType == boost)
        //            //{
        //            //    movement.boost = true;
        //            //    boostFunction.Invoke();
        //            //}
        //            if (tmpCollider == jump)
        //            {
        //                mov.jump = true;
        //                mov.jumpDown.Invoke();
        //            }
        //            else if (tmpCollider == attack)
        //            {
        //                mov.Attack();
        //            }
        //            else if (tmpCollider == touchJump)
        //            {
        //                //if (col.onWall && col.wallTag == "TouchJump")
        //                //{
        //                //  isClicked = true;
        //                //  jumpDir.gameObject.SetActive(true);
        //                Vector2 vec = GetJumpingDirection();
        //                //  Debug.Log(vec);
        //                touchJump.gameObject.SetActive(false);
        //                mov.Jump(-1, col.wall.GetComponent<Wall>().SetVec(-mov.dir, vec.x, vec.y));
        //                //  }
        //            }
        //        }
        //    }
        //    if (Input.GetTouch(i).phase == TouchPhase.Ended)
        //    {
        //        Ray ray = camera.ScreenPointToRay(camera.ScreenToWorldPoint(Input.GetTouch(i).position));
        //        Vector2 mPos = new Vector2(camera.ScreenToWorldPoint(Input.GetTouch(i).position).x, camera.ScreenToWorldPoint(Input.GetTouch(i).position).y);
        //        RaycastHit2D hit = Physics2D.Raycast(mPos, 0.1f * Vector2.one, 0.1f, 1 << LayerMask.NameToLayer("TouchCollider"));
        //        Instantiate(touchEffect, camera.ScreenToWorldPoint(Input.GetTouch(i).position), Quaternion.Euler(0, 0, 0), camera.transform);

        //        if (hit)
        //        {
        //            tmpCollider = hit.collider;
        //            //if (colType == touchJump)
        //            //{
        //            //    if (isClicked)
        //            //    {
        //            //        //lightningParticle.SetActive(false);
        //            //        jumpDir.gameObject.SetActive(false);
        //            //        isClicked = false;
        //            //        movement.DragJump();
        //            //    }
        //            //}
        //            //else
        //            //if (colType == boost)
        //            //{
        //            //    movement.boost = false;
        //            //    boostFunction.Invoke();
        //            //}
        //            //else
        //            if (tmpCollider == jump)
        //            {
        //                mov.jump = false;
        //                mov.jumpUp.Invoke();
        //            }
        //        }
        //        //else if (colType == attack)
        //        //{

        //        //}
        //    }
        //}
    }

    Vector2 GetJumpingDirection()
    {
        Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 tempVector = new Vector2(mPos.x - mov.transform.position.x, mPos.y - mov.transform.position.y);
        return tempVector;
    }

    //public void ColTypesReSet()
    //{
    //    colTypes.Clear();
    //}
}
