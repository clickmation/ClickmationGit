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
    private bool timerSet;
    private bool countIsReady;
    private int tokenStack = 3;

    void Start()
    {
        eventEndTime = TimeSpan.Parse(endTime);
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
        currentDate = TimeSpan.Parse(TimeManager.sharedInstance.getCurrentDateNow());
        timerSet = true;
    }

    void Update()
    {
        if (timerSet)
        {
            CheckIfTokenNeedsRefresh();
            if (tokenStack > 0)
            {

            }
            else
            {

            }
        }
    }

    public void CheckIfTokenNeedsRefresh()
    {
        if (currentDate.Subtract(lastDate).TotalDays > 0)
        {
            tokenStack = 3;
            updateTime();
            lastDate = currentDate;
        }
    }
}
