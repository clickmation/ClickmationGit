using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private string Timeformat;
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
			refreshStack();
			activateButton(IsButtonActive());
		} else {
			tokenAnimator.SetBool("Full", IsFullStack());
			activateButton(IsButtonActive());
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
            TimeManager.sharedInstance.getTime()
        );
        updateTime();
        Debug.Log("==> Time check complete!");
		
		_remainingTime = aDay.Subtract(currentTime);
		tcounter = _remainingTime.TotalMilliseconds;
		countIsReady = true;
    }


    private void updateTime()
    {
        currentTime = TimeSpan.Parse(TimeManager.sharedInstance.getCurrentTimeNow());
        currentDate = DateTime.Parse(TimeManager.sharedInstance.getCurrentDateNow());
        //curStack = saver.curStack;
    }

    void Update()
    {
		if (countIsReady) {startCountdown();}
		if (StackNeedsRefresh()) {
			refreshStack();
		}
    }
	
	public string GetRemainingTime(double x)
	{
		TimeSpan tempB = TimeSpan.FromMilliseconds(x);
		Timeformat = string.Format("{0:D2}:{1:D2}:{2:D2}", tempB.Hours, tempB.Minutes, tempB.Seconds);
		return Timeformat;
	}
	
	private void startCountdown()
	{
		tcounter-= Time.deltaTime * 1000;
		timeLabel.text = GetRemainingTime(tcounter) + " Until\nMore Revive Tokens";	
		
		
		if (tcounter <= 0) {
			countIsReady = false;
			validateTime ();
		}
	}

    public bool StackNeedsRefresh()
    {
		//안되면 DateTime.Compare(currentDate, refreshDate)>0으로 해보기
        if (currentDate.Subtract(refreshDate).TotalDays >= 0) {
            return true;
        } else {
			return false;
		}
    }
	
	public bool IsFullStack()
	{
		//saver값 써야함
		if (curStack >= maxStack) {
			return true;
		} else {
			return false;
		}
	}
	
	public bool IsButtonActive()
	{
		if(curStack == 0) {
			return false;
		} else {
			return true;
		}
	}
	
	
	public void refreshStack()
	{
		refreshDate = currentDate.Add(aDay);
		curStack = maxStack;
		stackLabel.text = curStack + "/" + maxStack;
		//curStack의 값을 maxStack의 값으로 바꿈 (maxStack은 saver에 있음)
		tokenAnimator.SetBool("Full", IsFullStack());
	}
	
	public void getReward()
	{
		curStack--;
		//token갯수 ++
		stackLabel.text = curStack + "/" + maxStack;
		activateButton(IsButtonActive());
		tokenAnimator.SetBool("Full", IsFullStack());
		updateTime();
	}
	
	private void activateButton(bool x)
	{
		tokenButton.interactable = x;
		tokenAnimator.SetBool("Activated", x);
	}
	
	
	private void validateTime()
	{
		Debug.Log ("==> Validating time to make sure no speed hack!");
        StartCoroutine ("CheckTime");
	}
}
