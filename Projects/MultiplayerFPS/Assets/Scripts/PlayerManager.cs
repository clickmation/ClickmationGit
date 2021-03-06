﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public float health;
    public float maxHealth = 100f;
    public int itemCount = 0;
    public Animator animator;
    public GameObject model;
    public PlayerUI playerUI;

    private void LateUpdate()
    {
        animator.transform.localPosition = Vector3.zero;
        animator.transform.localRotation = Quaternion.identity;
    }
    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;
        health = maxHealth;

        playerUI = GetComponentInChildren<PlayerUI>();

        if (playerUI != null)
        {
            playerUI.SetName(username);
            playerUI.UpdateHealth(health / maxHealth);
        }

        animator = GetComponentInChildren<Animator>();
    }

    public void SetHealth(float _health)
    {
        health = _health;

        if (playerUI != null)
        {
            playerUI.UpdateHealth(health / maxHealth);
        }

        if (health <= 0f)
        {
            Die();
        }
    }

    public void EquipGun()
    {

    }

    public void UnEquipGun()
    {
        
    }

    public void Die()
    {
        model.SetActive(false);
    }

    public void Respawn()
    {
        model.SetActive(true);
        SetHealth(maxHealth);
    }
}