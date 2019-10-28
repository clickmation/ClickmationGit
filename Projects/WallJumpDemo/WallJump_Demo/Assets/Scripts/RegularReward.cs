using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegularReward : MonoBehaviour
{
    public Button coinButton;
	public Text timeLabel;
	private double tcounter;
	private TimeSpan currentTime;
	private DateTime currentDate;
	private TimeSpan _remainingTime;
	private TimeSpan intervalTime = TimeSpan.FromMilliseconds(14400000);
	private string TimeFormat;
	private bool countIsReady;
	
	void Start()
	{
		StartCoroutine("CheckTime");
	}
	
	private IEnumerator CheckTime()
    {
        Debug.Log("==> Checking the time");
        timeLabel.text = "Checking the time";
        yield return StartCoroutine(
            TimeManager.sharedInstance.getTime()
        );
        updateTime();
        Debug.Log("==> Time check complete!");
    }
	
	private void updateTime()
    {
        currentTime = TimeSpan.Parse(TimeManager.sharedInstance.getCurrentTimeNow());
        currentDate = DateTime.Parse(TimeManager.sharedInstance.getCurrentDateNow());
    }
	
	void Update()
	{
		
	}
	
	public string GetRemainingTime(double x)
	{
		TimeSpan tempB = TimeSpan.FromMilliseconds(x);
		TimeFormat = string.Format("{0:D2}:{1:D2}:{2:D2}", tempB.Hours, tempB.Minutes, tempB.Seconds);
		return TimeFormat;
	}
	
	private void startCountdown()
	{
		
	}
	
	public void getReward()
	{
		
	}
	
	private void activateButton()
	{
		coinButton.interactable = true;
	}
	
	private void deactivateButton()
	{
		coinButton.interactable = false;
	}
	
	private void validateTime()
	{
		Debug.Log ("==> Validating time to make sure no speed hack!");
        StartCoroutine ("CheckTime");
	}
}
