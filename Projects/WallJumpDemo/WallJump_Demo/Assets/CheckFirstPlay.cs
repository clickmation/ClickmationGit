using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFirstPlay : MonoBehaviour
{
    [SerializeField] GameObject get500CoinText;

    void Start()
    {
        if (PlayerPrefs.GetInt("FirstPlayed") == 1) Destroy(this.gameObject);
    }

    public void Get500Coins()
    {
        SaveLoad.saveload.RandomCoinSave(500);
        SaveLoad.saveload.MainMenuLoad();
        SaveLoad.saveload.mainMenu.coinText.text = SaveLoad.saveload.mainMenu.coin.ToString();
        PlayerPrefs.SetInt("FirstPlayed", 1); ;
        SaveLoad.saveload.mainMenu.CoinPopUpUIActivate();
        Destroy(get500CoinText);
        Destroy(this.gameObject);
    }
}
