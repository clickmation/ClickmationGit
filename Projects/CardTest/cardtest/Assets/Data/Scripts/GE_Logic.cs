using System.Collections;
using UnityEngine;


namespace CM.GameElements
{
	public abstract class GE_Logic : ScriptableObject
	{
		public abstract void OnClick(CardInstance inst);
		
		public abstract void OnHighlight(CardInstance inst);
	}
}