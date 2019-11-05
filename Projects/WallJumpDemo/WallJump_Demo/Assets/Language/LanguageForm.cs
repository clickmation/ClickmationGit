﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LanguageDefault", menuName = "LanguageForm")]
public class LanguageForm : ScriptableObject
{
    public string language;

    [Space]

    [Header("MainMenu")]

    public string start;
    public string howTo;
    public string shop;
    public string customization;
    public string quitCheck;
    public string getToken;
    public string untilToken;
    public string getCoin;
    public string untilCoin;
    public string until;

    [Space]

    [Header("Shop")]

    public string whatsPrize;
    public string coin;

    [Space]

    [Header("Tutorial")]

    public string[] tut;

    [Space]

    [Header("Customization")]

    public string trail;
    public string skin;

    [Space]

    [Header("Sound")]

    public string sound;
    public string master;
    public string soundEffect;
    public string bgm;

    [Space]

    [Header("MainMenu Pop Up")]

    public string congratulation;

    [Space]

    [Header("Choice")]

    public string yes;
    public string no;

    [Space]

    [Header("InGame")]

    public string pause;
    public string retry;
    public string resume;
    public string mainmenu;
    public string gameOver;
    public string revive;

    [Space]

    [Header("Check")]

    public string deadRetry;
    public string quitToMain;
    public string scoreLost;
}