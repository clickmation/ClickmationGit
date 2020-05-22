﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Settings
{
    public static GameManager gameManager;
	private static ResourcesManager _resourcesManager;
	private static ConsoleHook _consoleManager;

	public static void RegisterEvent(string e, Color color = default(Color))
	{
		if (_consoleManager == null)
		{
			_consoleManager = Resources.Load("ConsoleHook") as ConsoleHook;
		}

		_consoleManager.RegisterEvent(e, color);
	}

	public static ResourcesManager GetResourcesManager()
	{
		if (_resourcesManager == null)
		{
			_resourcesManager = Resources.Load("ResourcesManager") as ResourcesManager;
			_resourcesManager.Init();
		}

		return _resourcesManager;
	}
	
	public static List<RaycastResult> GetUIObjs()
	{
		PointerEventData pointerData = new PointerEventData(EventSystem.current)
		{
			position = Input.mousePosition
		};
	
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(pointerData, results);
		return results;
	}

	public static void DropCreatureCard(Transform c, Transform p, CardInstance cardInst, int laneNum)
	{
		SetParentForCard(c, p);

		gameManager.currentPlayer.DropCard(cardInst, laneNum);

		int convLaneNum = laneNum + 1;
		RegisterEvent(gameManager.currentPlayer.userName + " played " + cardInst.vis.card.name + " on lane " + convLaneNum.ToString(), Color.white);
	}

	public static void SetParentForCard(Transform c, Transform p)
	{
		c.SetParent(p);
		c.localPosition = Vector3.zero;
		c.localEulerAngles = Vector3.zero;
		c.localScale = Vector3.one;
	}
}
