using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour
{
    public Button tokenButton;
    public Text timeLabel;
    public string endTime;
    private double tcounter;
    private TimeSpan eventEndTime;
    private TimeSpan currentTime;
    private TimeSpan currentDate;
    private TimeSpan lastDate;
    private TimeSpan _remainingTime;
    private string timeformat;
    private bool countIsReady;
    private int curStack;

	
	
	//lastDate도 saver에 어케 해야할거임
	
    void Start()
    {
        eventEndTime = TimeSpan.Parse(endTime);
        StartCoroutine("CheckTime");
		if (StackNeedsRefresh()){
			RefreshStack();
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

    }


    private void updateTime()
    {
        currentTime = TimeSpan.Parse(TimeManager.sharedInstance.getCurrentTimeNow());
        currentDate = TimeSpan.Parse(TimeManager.sharedInstance.getCurrentDateNow());
        curStack = saver.curStack;
    }

    void Update()
    {
		if (!isFullStack() && justRefreshed) {
			_remainingTime = eventEndTime.Subtract(currentTime);
			tcounter = _remainingTime.TotalMilliseconds;
			countIsReady = true;
			justRefreshed = false;
		}
		
		if (countIsReady) {startCountdown();}
    }
	
	public string GetRemainingTime(double x)
	{
		TimeSpan tempB = TimeSpan.FromMilliseconds(x);
		Timeformat = string.Format("{0:D2}:{1:D2}:{2:D2}", tempB.Hours, tempB.Minutes, tempB.Seconds);
		return Timeformat
	}
	
	private void startCountdown()
	{
		tcounter-= Time.deltaTime * 1000;
		if (saver.curStack == 0) {
			deactivateButton(GetRemainingTime(tcounter) + "Until\nMore Revive Tokens");	
		} else {
			activateButton(GetRemainingTime(tcounter) + "Until\nMore Revive Tokens");
		}
		
		if (tcounter <= 0) {
			countIsReady = false;
			validateTime ();
		}
	}

    public bool StackNeedsRefresh()
    {
        if (currentDate.Subtract(lastDate).TotalDays > 0) {
            return true;
        } else {
			return false;
		}
    }
	
	public bool IsFullStack()
	{
		if (saver.curStack >= saver.maxStack) {
			activateButton("")
			return true;
		} else {
			return false;
		}
	}
	
	public void refreshStack()
	{
		lastDate = currentDate;
		//curStack의 값을 maxStack의 값으로 바꿈 (maxStack은 saver에 있음)
		justRefreshed = true;
	}
	
	public void getReward()
	{
		saver.curStack--;
		//token갯수 ++
		updateTime();
	}
	
	private void activateButton(string x)
	{
		tokenButton.interactable = true;
		//animation 그거 parameter 중 activated를 true로 하면 될거임
		timeLabel.text = x;
	}
	
	private void deactivateButton(string x)
	{
		tokenButton.interactable = false;
		//animation 그거 parameter 중 activated를 false로 하면 될거임
		timeLabel.text = x;
	}
	
	private void validateTime()
	{
		Debug.Log ("==> Validating time to make sure no speed hack!");
        StartCoroutine ("CheckTime");
	}
}
