using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Settings
{
    public static GameManager gameManager;
	
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
}
