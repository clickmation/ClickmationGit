using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Camera2DFollow : MonoBehaviour
{
    public Transform target;
    public float xDamping = 1;
    public float yDamping = 1;
    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;
    //public float yPosRestriction = -1;
    private bool updateLookAheadTarget;
    public float dir;

    private float offsetZ;
    private Vector3 lastTargetPosition;
    private Vector3 currentVelocity;
    private Vector3 lookAheadPos;

    float nextTimeToSearch = 0;

    // Use this for initialization
    private void Start()
    {
        lastTargetPosition = target.position;
        offsetZ = (transform.position - target.position).z;
        transform.parent = null;
    }

    // Update is called once per frame
    private void Update()
    {
        //if (target == null)
        //{
        //    FindPlayer();
        //    return;
        //}

        // only update lookahead pos if accelerating or changed direction
        float xMoveDelta = dir * Mathf.Abs((target.position - lastTargetPosition).x);

        updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

        if (updateLookAheadTarget)
        {
            lookAheadPos = lookAheadFactor * Vector3.right * dir;
        }
        else
        {
            lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
        }

        Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.forward * offsetZ;
        float newPosX = Mathf.SmoothDamp(transform.position.x, aheadTargetPos.x, ref currentVelocity.x, xDamping);
        float newPosY = Mathf.SmoothDamp(transform.position.y, aheadTargetPos.y, ref currentVelocity.y, yDamping);

        //newPos = new Vector3(newPos.x, Mathf.Clamp(newPos.y, yPosRestriction, Mathf.Infinity), newPos.z);
        Vector3 newPos = new Vector3(newPosX, newPosY, -10);

        transform.position = newPos;

        lastTargetPosition = target.position;
    }

    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null)
            {
                target = searchResult.transform;
            }
            nextTimeToSearch = Time.time + 0.5f;
        }
    }
}
