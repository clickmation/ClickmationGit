﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collision : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    [Space]
	
	public Animator animator;

    public bool onGround;
    private bool _onGround;
    public bool onWall;
    private bool _onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public int wallSide;

    [Space]

    [Header("Collision")]

    public float collisionRadius = 0.25f;
    public float capsuleSize;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    private Color debugCollisionColor = Color.red;

    [Space]

    public Transform wall;
    public string wallTag;

    [Header("EnterFunction")]

    [Space]

    public Button.ButtonClickedEvent[] onGroundEnter;
    public Button.ButtonClickedEvent[] onGroundExit;
    public Button.ButtonClickedEvent[] onWallEnter;
    public Button.ButtonClickedEvent[] onWallExit;

    // Update is called once per frame
    void FixedUpdate()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        animator.SetBool("Ground", onGround);
		onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, wallLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, wallLayer);
        animator.SetBool("Wall", onWall);

        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, wallLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, wallLayer);

        //onGround = Physics2D.OverlapCapsule((Vector2)transform.position + bottomOffset, new Vector2(capsuleSize, collisionRadius), CapsuleDirection2D.Horizontal, groundLayer);
        //onWall = Physics2D.OverlapCapsule((Vector2)transform.position + rightOffset, new Vector2(capsuleSize, collisionRadius), CapsuleDirection2D.Vertical, groundLayer)
        //    || Physics2D.OverlapCapsule((Vector2)transform.position + leftOffset, new Vector2(capsuleSize, collisionRadius), CapsuleDirection2D.Vertical, groundLayer);

        //onRightWall = Physics2D.OverlapCapsule((Vector2)transform.position + rightOffset, new Vector2(capsuleSize, collisionRadius), CapsuleDirection2D.Vertical, groundLayer);
        //onLeftWall = Physics2D.OverlapCapsule((Vector2)transform.position + leftOffset, new Vector2(capsuleSize, collisionRadius), CapsuleDirection2D.Vertical, groundLayer);

        wallSide = onRightWall ? -1 : 1;

        if (onGround == true && _onGround == false)
        {
            for (int i = 0; i < onGroundEnter.Length; i++)
            {
                onGroundEnter[i].Invoke();
            }
        }
        else
        if (onGround == false && _onGround == true)
        {
            for (int i = 0; i < onGroundExit.Length; i++)
            {
                onGroundExit[i].Invoke();
            }
        }
        if (onWall == true && _onWall == false)
        {
            for (int i = 0; i < onWallEnter.Length; i++)
            {
                onWallEnter[i].Invoke();
            }
        }
        else
        if (onWall == false && _onWall == true)
        {
            for (int i = 0; i < onWallExit.Length; i++)
            {
                onWallExit[i].Invoke();
            }
        }

        _onGround = onGround;
        _onWall = onWall;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }

    public void OnWallEnter()
    {
        if (onRightWall) wall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, wallLayer).transform;
        else if (onLeftWall) wall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, wallLayer).transform;

        //if (onRightWall) wall = Physics2D.OverlapCapsule((Vector2)transform.position + rightOffset, new Vector2(capsuleSize, collisionRadius), CapsuleDirection2D.Vertical, groundLayer).transform;
        //else if (onLeftWall) wall = Physics2D.OverlapCapsule((Vector2)transform.position + leftOffset, new Vector2(capsuleSize, collisionRadius), CapsuleDirection2D.Vertical, groundLayer).transform;
        wallTag = wall.tag;
    }
}
