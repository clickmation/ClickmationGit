using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class RegularReward : MonoBehaviour
{
	public Animator coinAnimator;
    public Button coinButton;
	public Text timeLabel;
	private double tcounter;
	private TimeSpan currentTime;
	private DateTime currentDateTime;
	private DateTime refreshDateTime;
	private TimeSpan _remainingTime;
	private TimeSpan intervalTime = TimeSpan.FromMilliseconds(120000);
	private string dateString;
	private string TimeFormat;
	private bool countIsReady;
	private bool timerSet;

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
		StartCoroutine("CheckTime");
	}
	
	void OnEnable()
	{
		ActivateButton(countIsReady);
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
                ActivateButton(countIsReady);
            } else {
				ActivateButton(countIsReady);
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
        timeLabel.text = GetRemainingTime(tcounter) + " Until\nFREE COIN";

        if (tcounter <= 0){
			countIsReady = false;
			ValidateTime();
		}
	}
	
	public void GetReward()
	{
		ValidateTime();
		if(DateTime.Compare(refreshDateTime, currentDateTime) <= 0)
		{
			//coinRanReward();
		
			refreshDateTime = currentDateTime.Add(intervalTime);
            SaveLoad.saveload.RegularRewardSave();
            SaveLoad.saveload.RegularRewardLoad();
            ActivateButton(countIsReady);
            Debug.Log(refreshDateTime);
		}
	}
	
	private void ActivateButton(bool x)
	{
		coinButton.interactable = !x;
		coinAnimator.SetBool("Activated", !x);
	}
	
	private void ValidateTime()
	{
		Debug.Log ("==> Validating time to make sure no speed hack!");
        StartCoroutine ("CheckTime");
	}
}
