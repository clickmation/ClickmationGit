using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour
{
    public Button tokenButton;
    public Text timeLabel;
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

	
	//lastDate도 saver에 어케 해야할거임
	
    void Start()
    {
        StartCoroutine("CheckTime");
		if (StackNeedsRefresh()){
			refreshStack();
			activateButton();
		} else {
			if (curStack == 0) {
				deactivateButton();
			} else {
				activateButton();
			}
		}
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
		timeLabel.text = GetRemainingTime(tcounter) + "Until\nMore Revive Tokens";	
		
		
		if (tcounter <= 0) {
			countIsReady = false;
			validateTime ();
		}
	}

    public bool StackNeedsRefresh()
    {
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
			activateButton();
			return true;
		} else {
			return false;
		}
	}
	
	public void refreshStack()
	{
		refreshDate = currentDate.Add(aDay);
		curStack = maxStack;
		//curStack의 값을 maxStack의 값으로 바꿈 (maxStack은 saver에 있음)
	}
	
	public void getReward()
	{
		curStack--;
		//token갯수 ++
		updateTime();
	}
	
	private void activateButton()
	{
		tokenButton.interactable = true;
		//animation 그거 parameter 중 activated를 true로 하면 될거임
	}
	
	private void deactivateButton()
	{
		tokenButton.interactable = false;
		//animation 그거 parameter 중 activated를 false로 하면 될거임
	}
	
	private void validateTime()
	{
		Debug.Log ("==> Validating time to make sure no speed hack!");
        StartCoroutine ("CheckTime");
	}
}
