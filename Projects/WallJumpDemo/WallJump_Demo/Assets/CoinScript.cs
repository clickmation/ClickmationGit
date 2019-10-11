using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public static CoinScript coinScript;
    public int coin;

    public void Awake()
    {
        coinScript = this;
        coin = PlayerPrefs.GetInt("Coin");
    }


    public void PlusCoin(int value)
    {
        coin += value;
        PlayerPrefs.SetInt("Coin", coin);
        coin = PlayerPrefs.GetInt("Coin");
    }

    public void MinusCoin(int value)
    {
        coin -= value;
        PlayerPrefs.SetInt("Coin", coin);
        coin = PlayerPrefs.GetInt("Coin");
    }
}
