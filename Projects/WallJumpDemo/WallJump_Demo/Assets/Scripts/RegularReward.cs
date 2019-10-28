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
	private DateTime currentDateTime;
	private DateTime refreshDateTime;
	private TimeSpan _remainingTime;
	private TimeSpan intervalTime = TimeSpan.FromMilliseconds(14400000);
	private string TimeFormat;
	private string dateFormat = "MM/dd/yyyy HH:mm:ss";
	private CultureInfo provider = CultureInfo.InvariantCulture;
	private bool countIsReady;
	private bool timerSet;
	
	
	void Start()
	{
		StartCoroutine("CheckTime");
	}
	
	void OnEnable()
	{
		
	}
	
	private IEnumerator CheckTime()
    {
        Debug.Log("==> Checking the time");
        timeLabel.text = "Checking the time";
        yield return StartCoroutine(
            TimeManager.sharedInstance.GetTime()
        );
        updateTime();
        Debug.Log("==> Time check complete!");
    }
	
	private void updateTime()
    {
        currentDateTime = DateTime.ParseExact(TimeManager.sharedInstance.GetCurrentDateNow()+" "+TimeManager.sharedInstance.GetCurrentTimeNow(), dateFormat, provider);
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
			} else {
				activateButton(true);
			}
		}
		
		if(countIsReady) {startCountdown();}
	}
	
	public string GetRemainingTime(double x)
	{
		TimeSpan tempB = TimeSpan.FromMilliseconds(x);
		TimeFormat = string.Format("{0:D2}:{1:D2}:{2:D2}", tempB.Hours, tempB.Minutes, tempB.Seconds);
		return TimeFormat;
	}
	
	private void startCountdown()
	{
		timerSet = false;
		tcounter -= Time.deltaTime * 1000;
		
		if (tcounter <= 0){
			countIsReady = false;
			validateTime();
		}
	}
	
	public void getReward()
	{
		//coinRanReward();
		
		updateTime();
		activateButton(false);
	}
	
	//public bool IsButtonActive()
	//{
	//	
	//}
	
	private void activateButton(bool x)
	{
		coinButton.interactable = x;
		coinAnimator.SetBool("Activated", x);
	}
	
	private void validateTime()
	{
		Debug.Log ("==> Validating time to make sure no speed hack!");
        StartCoroutine ("CheckTime");
	}
}
