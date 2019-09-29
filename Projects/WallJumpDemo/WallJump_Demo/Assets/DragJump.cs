using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragJump : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField] private Collision col;
    public bool isClicked;
    [SerializeField] private Transform jumpDir;

    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        if (col.onWall && col.wallTag == "DragJump")
        {
            isClicked = true;
            jumpDir.gameObject.SetActive(true);
            Debug.Log("Clicked");
        }
    }

    private void OnMouseDrag()
    {
        Vector2 tmp = GetJumpingDirection();
        float r = movement.dir < 0 ? Mathf.Asin(tmp.y) * Mathf.Rad2Deg : (Mathf.PI - Mathf.Asin(tmp.y)) * Mathf.Rad2Deg;
        jumpDir.rotation = Quaternion.Euler(0, 0, r);
    }

    Vector2 GetJumpingDirection()
    {
        Vector3 camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 tempVector = new Vector3(camPos.x - this.transform.position.x, camPos.y - this.transform.position.y, 0);
        Vector2 _jumpingDir = movement.dir < 0 ? new Vector2(Mathf.Abs(tempVector.normalized.x), tempVector.normalized.y) : new Vector2(-Mathf.Abs(tempVector.normalized.x), tempVector.normalized.y);
        return _jumpingDir;
    }

    private void OnMouseUp()
    {
        if (isClicked)
        {
            //lightningParticle.SetActive(false);
            jumpDir.gameObject.SetActive(false);
            isClicked = false;
            movement.DragJump();
            //dragJumped = true;
            //if (col.wall != null) col.wall = null;
            //boosted = false;
            //jumpingDir = GetJumpingDirection();
            //if (jumpingDir.x * dir < 0)
            //{
            //    ChangeCameraPosition();
            //}
            //speedMultiflier = Mathf.Abs(jumpingDir.x);
            //_speed = speed * speedMultiflier;
            //dir = Mathf.Sign(jumpingDir.x);
            //lastVelocity *= -1f;
            //rb.velocity = new Vector2(lastVelocity, 0);
            //rb.velocity += jumpForce * jumpingDir;
            //stamina -= staminaDragJumpEater;
            //staminaFunction = staminaMaintain;
        }
    }
}
