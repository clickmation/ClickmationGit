using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HealthManager
{
    void Init();
    void TakeDamage(float _damage);
    float GetHealth();
    void OnDeath();
    bool IsDead();
}
