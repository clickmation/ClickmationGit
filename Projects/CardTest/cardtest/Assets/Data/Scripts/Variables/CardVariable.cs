using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Variables/CardVariable")]
public class CardVariable : ScriptableObject
{
    public CardInstance value;
	
	public void Set (CardInstance v)
	{
		value = v;
	}
}
