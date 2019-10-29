using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class DailyReward : MonoBehaviour
{
	public Animator tokenAnimator;
    public Button tokenButton;
    public Text timeLabel;
	public Text stackLabel;
    private double tcounter;
    private TimeSpan currentTime;
    private DateTime currentDate;
    private DateTime refreshDate;
    private TimeSpan _remainingTime;
	private TimeSpan aDay = TimeSpan.FromMilliseconds(86400000);
    private string timeFormat;
    private bool countIsReady;
    private int curStack;
	private int maxStack = 3;
	
	private bool tempActivated;
	private bool tempFull;

	
	//refreshDate도 saver에 어케 해야할거임
	
	
    void Start()
    {
        StartCoroutine("CheckTime");
		if (StackNeedsRefresh()){
			RefreshStack();
			ActivateButton(IsButtonActive());
		} else {
			tokenAnimator.SetBool("Full", IsFullStack());
			ActivateButton(IsButtonActive());
		}
    }
	
	void OnEnable()
	{
		tokenAnimator.SetBool("Activated", IsButtonActive());
		tokenAnimator.SetBool("Full", IsFullStack());
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
		
		_remainingTime = aDay.Subtract(currentTime);
		tcounter = _remainingTime.TotalMilliseconds;
		countIsReady = true;
    }


    private void UpdateTime()
    {
        currentTime = TimeSpan.Parse(TimeManager.sharedInstance.GetCurrentTimeNow());
        currentDate = DateTime.ParseExact(TimeManager.sharedInstance.GetCurrentDateNow(), "MM-dd-yyyy", CultureInfo.InvariantCulture);
		Debug.Log(currentDate);
        //curStack = saver.curStack;
    }

    void Update()
    {
		if (countIsReady) {StartCountdown();}
		if (StackNeedsRefresh()) {
			RefreshStack();
		}
    }
	
	public string GetRemainingTime(double x)
	{
		TimeSpan tempB = TimeSpan.FromMilliseconds(x);
		timeFormat = string.Format("{0:D2}:{1:D2}:{2:D2}", tempB.Hours, tempB.Minutes, tempB.Seconds);
		return timeFormat;
	}
	
	private void StartCountdown()
	{
		tcounter-= Time.deltaTime * 1000;
		timeLabel.text = GetRemainingTime(tcounter) + " Until\nMore Revive Tokens";	
		
		
		if (tcounter <= 0) {
			countIsReady = false;
			ValidateTime ();
		}
	}

    private bool StackNeedsRefresh()
    {
		//안되면 DateTime.Compare(currentDate, refreshDate)>0으로 해보기
        if (currentDate.Subtract(refreshDate).TotalDays >= 0) {
            return true;
        } else {
			return false;
		}
    }
	
	private bool IsFullStack()
	{
		//saver값 써야함
		if (curStack >= maxStack) {
			return true;
		} else {
			return false;
		}
	}
	
	private bool IsButtonActive()
	{
		if(curStack == 0) {
			return false;
		} else {
			return true;
		}
	}
	
	
	public void RefreshStack()
	{
		refreshDate = currentDate.Add(aDay);
		curStack = maxStack;
		stackLabel.text = curStack + "/" + maxStack;
		//curStack의 값을 maxStack의 값으로 바꿈 (maxStack은 saver에 있음)
		tokenAnimator.SetBool("Full", IsFullStack());
	}
	
	public void GetReward()
	{
		curStack--;
		//token갯수 ++
		stackLabel.text = curStack + "/" + maxStack;
		ActivateButton(IsButtonActive());
		tokenAnimator.SetBool("Full", IsFullStack());
		UpdateTime();
	}
	
	private void ActivateButton(bool x)
	{
		tokenButton.interactable = x;
		tokenAnimator.SetBool("Activated", x);
	}
	
	
	private void ValidateTime()
	{
		Debug.Log ("==> Validating time to make sure no speed hack!");
        StartCoroutine ("CheckTime");
	}
}
