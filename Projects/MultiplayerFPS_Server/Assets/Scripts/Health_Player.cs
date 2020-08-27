using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Player : MonoBehaviour, HealthManager
{
    public float health;
    public float maxHealth = 100f;
    public Player player;

    public void Init()
    {
        health = maxHealth;
    }

    public void TakeDamage(float _damage)
    {
        if (health <= 0f)
        {
            return;
        }

        health -= _damage;

        if (health <= 0f)
        {
            OnDeath();
        }

        ServerSend.PlayerHealth(player);
    }

    public float GetHealth()
    {
        return health;
    }
    
    public void OnDeath()
    {
        health = 0f;
        player.controller.enabled = false;
        player.Respawn();
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    public void InputPlayer(Player _player)
    {
        if (player == null)
        {
            player = _player;
        }
    }
}
