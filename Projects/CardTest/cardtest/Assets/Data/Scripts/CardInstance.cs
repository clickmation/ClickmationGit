using System.Collections;
using UnityEngine;

public class CardInstance : MonoBehaviour , IClickable
{
	public CardVis vis;
	public CM.GameElements.GE_Logic currentLogic;
	
	void Start()
	{
		vis = GetComponent<CardVis>();
	}
	
    public void OnClick()
	{
		if (currentLogic == null)
			return;
		
		currentLogic.OnClick(this);
	}
	
	public void OnHighlight()
	{
		if (currentLogic == null)
			return;
		
		currentLogic.OnHighlight(this);
	}
}
