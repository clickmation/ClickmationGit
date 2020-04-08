using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentSelected : MonoBehaviour
{
    public CardVariable currentCard;
	public CardVis cardVis;
	
	Transform mTransform;
	
	public void LoadCard()
	{
		if (currentCard.value == null)
			return;
		
		currentCard.value.gameObject.SetActive(false);
		cardVis.LoadCard(currentCard.value.vis.card);
		cardVis.gameObject.SetActive(true);
	}
	
	public void CloseCard()
	{
		cardVis.gameObject.SetActive(false);
	}
	
	private void Start()
	{
		mTransform = this.transform;
		CloseCard();
	}
	
	void Update()
	{
		mTransform.position = Input.mousePosition;
	}
}
