using System.Collections;
using UnityEngine;


namespace CM.GameElements
{
	[CreateAssetMenu(menuName = "GameElements/My Down Card")]
	public class GE_LogicMyDownCard : GE_Logic
	{
		public override void OnClick(CardInstance inst)
		{
			Debug.Log("This card is on my ground");
		}
		
		public override void OnHighlight(CardInstance inst)
		{
			
		}
	}
}
