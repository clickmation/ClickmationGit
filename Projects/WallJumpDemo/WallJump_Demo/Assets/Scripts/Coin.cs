﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    public int coinAddAmount;

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.transform.GetComponent<Movement>().AddCoin(coinAddAmount);
            AudioManager.PlaySound("coin");
            Destroy(this.gameObject);
        }
    }
}