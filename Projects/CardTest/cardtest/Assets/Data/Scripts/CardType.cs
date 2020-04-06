using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardType : ScriptableObject
{
	public string typeName;
	public Sprite cardTemplate;
	public Sprite cardType;
	
    public virtual void OnSetType(CardVis vis)
	{
		vis.typeImage.sprite = this.cardType;
		vis.cardTemplate.sprite = this.cardTemplate;
	}
}
