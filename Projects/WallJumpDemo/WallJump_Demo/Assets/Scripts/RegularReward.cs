using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularReward : MonoBehaviour
{
    public Button coinButton;
	public Text timeLabel;
	private double tcounter;
	private TimeSpan currentTime;
	private TimeSpan _remainingTime;
	private TimeSpan intervalTime = TimeSpan.FromMilliseconds(14400000);
	private string TimeFormat;
	private bool countIsReady;
	
	void Start()
	{
		StartCoroutine("CheckTime");
	}
}
