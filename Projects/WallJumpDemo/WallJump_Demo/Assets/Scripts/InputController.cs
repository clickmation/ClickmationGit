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
    private Collider2D tmpCollider;
    public Text testText;


    // Update is called once per frame
    void Update()
    {
        //Mouse
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(camera.ScreenToWorldPoint(Input.mousePosition));
            Vector2 mPos = new Vector2(camera.ScreenToWorldPoint(Input.mousePosition).x, camera.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hit = Physics2D.Raycast(mPos, 0.1f * Vector2.one, 0.1f, 1 << LayerMask.NameToLayer("TouchCollider"));

            if (hit)
            {
                tmpCollider = hit.collider;
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
                    Vector2 vec = GetJumpingDirection();
                    touchJump.gameObject.SetActive(false);
                    mov.Jump(-1, col.wall.GetComponent<Wall>().SetVec(-mov.dir, vec.x, vec.y));
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (tmpCollider == jump)
            {
                mov.jump = false;
                mov.jumpUp.Invoke();
            }
        }

            //MultiTouch (New Version)
        //    for (int i = 0; i < Input.touchCount; i++)
        //{
        //    if (Input.GetTouch(i).phase == TouchPhase.Began)
        //    {
        //        Ray ray = camera.ScreenPointToRay(camera.ScreenToWorldPoint(Input.GetTouch(i).position));
        //        Vector2 mPos = new Vector2(camera.ScreenToWorldPoint(Input.GetTouch(i).position).x, camera.ScreenToWorldPoint(Input.GetTouch(i).position).y);
        //        RaycastHit2D hit = Physics2D.Raycast(mPos, 0.1f * Vector2.one, 0.1f, 1 << LayerMask.NameToLayer("TouchCollider"));
                
        //        if (hit)
        //        {
        //            tmpCollider = hit.collider;
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
        //                Vector2 vec = GetJumpingDirection();
        //                touchJump.gameObject.SetActive(false);
        //                mov.Jump(-1, col.wall.GetComponent<Wall>().SetVec(-mov.dir, vec.x, vec.y));
        //            }
        //        }
        //    }
        //    if (Input.GetTouch(i).phase == TouchPhase.Ended)
        //    {
        //        Ray ray = camera.ScreenPointToRay(camera.ScreenToWorldPoint(Input.GetTouch(i).position));
        //        Vector2 mPos = new Vector2(camera.ScreenToWorldPoint(Input.GetTouch(i).position).x, camera.ScreenToWorldPoint(Input.GetTouch(i).position).y);
        //        RaycastHit2D hit = Physics2D.Raycast(mPos, 0.1f * Vector2.one, 0.1f, 1 << LayerMask.NameToLayer("TouchCollider"));
                
        //        if (hit)
        //        {
        //            tmpCollider = hit.collider;
        //            if (tmpCollider == jump)
        //            {
        //                mov.jump = false;
        //                mov.jumpUp.Invoke();
        //            }
        //        }
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
