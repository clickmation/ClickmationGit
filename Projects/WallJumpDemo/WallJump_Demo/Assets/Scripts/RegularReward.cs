using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class RegularReward : MonoBehaviour
{
    LanguageSet ls;

	public Animator coinAnimator;
    public Button coinButton;
	public Text timeLabel;
	private double tcounter;
	private TimeSpan currentTime;
	private DateTime currentDateTime;
	private DateTime refreshDateTime;
	private TimeSpan _remainingTime;
	private TimeSpan intervalTime = TimeSpan.FromMilliseconds(14400000);//14400000
	private string dateString;
	private string TimeFormat;
	private bool countIsReady;
	private bool timerSet;

    [SerializeField] int[] ranCoin;

    public DateTime GetRefreshDateTime()
    {
        return refreshDateTime;
    }

    public void SetRefreshDateTime(DateTime dt)
    {
        refreshDateTime = dt;
    }

    void Start()
	{
        SaveLoad.saveload.rr = this;
        SaveLoad.saveload.RegularRewardLoad();
        ls = LanguageSet.ls;
        StartCoroutine("CheckTime");
	}
	
	void OnEnable()
	{
		StartCoroutine("CheckTime");
		ActivateButton(IsButtonActive());
	}
	
	private IEnumerator CheckTime()
    {
        Debug.Log("==> Checking the time");
        timeLabel.text = "Checking the time";
        yield return StartCoroutine(
            TimeManager.sharedInstance.GetTime()
        );
        UpdateTime();
        Debug.Log("==> Time check complete!");
    }
	
	private void UpdateTime()
    {
		Debug.Log("updatingTime");
        currentDateTime = DateTime.ParseExact(TimeManager.sharedInstance.GetCurrentDateNow(), "MM-dd-yyyy", CultureInfo.InvariantCulture);
		currentTime = TimeSpan.Parse(TimeManager.sharedInstance.GetCurrentTimeNow());
		currentDateTime = currentDateTime.Add(currentTime);
		Debug.Log("currentDateTime is : " + currentDateTime);
		
		timerSet = true;
    }
	
	void Update()
	{
		if(timerSet)
		{
			if(DateTime.Compare(refreshDateTime, currentDateTime) > 0) {
				_remainingTime = refreshDateTime.Subtract(currentDateTime);
				tcounter = _remainingTime.TotalMilliseconds;
				countIsReady = true;
                ActivateButton(IsButtonActive());
            } else {
				ActivateButton(IsButtonActive());
			}
		}
		
		if(countIsReady) {StartCountdown();}
	}
	
	public string GetRemainingTime(double x)
	{
		TimeSpan tempB = TimeSpan.FromMilliseconds(x);
		TimeFormat = string.Format("{0:D2}:{1:D2}:{2:D2}", tempB.Hours, tempB.Minutes, tempB.Seconds);
		return TimeFormat;
	}
	
	private void StartCountdown()
	{
		timerSet = false;
		tcounter -= Time.deltaTime * 1000;
        timeLabel.text = GetRemainingTime(tcounter) + ls.language.until + "\n" + ls.language.untilCoin;

        if (tcounter <= 0){
			countIsReady = false;
			ValidateTime();
		}
	}

    public void ShowAd()
    {
        ADManager.adManager.ShowCoinRewardedAd();
    }

    public void GetReward()
	{
		ValidateTime();
		if(DateTime.Compare(refreshDateTime, currentDateTime) <= 0)
		{
            SaveLoad.saveload.RandomCoinSave(CoinRanReward());
            SaveLoad.saveload.MainMenuLoad();
            SaveLoad.saveload.mainMenu.coinText.text = SaveLoad.saveload.mainMenu.coin.ToString();
            refreshDateTime = currentDateTime.Add(intervalTime);
            SaveLoad.saveload.RegularRewardSave();
            SaveLoad.saveload.RegularRewardLoad();
            ActivateButton(IsButtonActive());
        }
    }

    private int CoinRanReward()
    {
        int i = 0;
        int r = UnityEngine.Random.Range(0, 100);
        if (r < 30) i = 0;
        else if (r >= 30 && r < 70) i = 1;
        else if (r >= 70 && r < 90) i = 2;
        else if (r >= 90 && r < 98) i = 3;
        else if (r >= 98 && r < 100) i = 4;
        return ranCoin[i];
    }
	
	private bool IsButtonActive()
	{
		if(DateTime.Compare(refreshDateTime, currentDateTime)>0) {
			return false;
		} else {
			return true;
		}
	}
	
	private void ActivateButton(bool x)
	{
		coinButton.interactable = x;
		coinAnimator.SetBool("Activated", x);
	}
	
	private void ValidateTime()
	{
		Debug.Log ("==> Validating time to make sure no speed hack!");
        StartCoroutine ("CheckTime");
	}
}
