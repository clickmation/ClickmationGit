using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public Movement movement;
    [SerializeField] private Collision col;
    [SerializeField] private Camera camera;

    [Space]

    [Header("DragJump")]

    public bool isClicked;
    [SerializeField] private Transform jumpDir;

    [Space]

    [Header("Dead")]

    [SerializeField] private Collider2D dragJump;
    [SerializeField] private Collider2D boost;
    [SerializeField] private Collider2D jump;
    private Collider2D tmp;

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
            if (hit)
            {
                tmp = hit.collider;
                if (tmp == dragJump)
                {
                    if (col.onWall && col.wallTag == "DragJump")
                    {
                        isClicked = true;
                        jumpDir.gameObject.SetActive(true);
                    }
                }
                else if (tmp == boost)
                {
                    movement.boost = true;
                }
                else if (tmp == jump)
                {
                    movement.jump = true;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (tmp == dragJump)
            {
                //lightningParticle.SetActive(false);
                jumpDir.gameObject.SetActive(false);
                isClicked = false;
                movement.DragJump();
            }
            else if (tmp == boost)
            {
                movement.boost = false;
            }
            else if (tmp == jump)
            {
                movement.jump = false;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (tmp == dragJump)
            {
                Vector2 tmp = GetJumpingDirection();
                float r = movement.dir < 0 ? Mathf.Asin(tmp.y) * Mathf.Rad2Deg : (Mathf.PI - Mathf.Asin(tmp.y)) * Mathf.Rad2Deg;
                jumpDir.rotation = Quaternion.Euler(0, 0, r);
            }
            //else if (tmp == boost)
            //{

            //}
            //else if (tmp == jump)
            //{

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
