using System.Collections;
using UnityEngine;


namespace CM.GameElements
{
	[CreateAssetMenu(menuName = "GameElements/My Hand Card")]
	public class GE_LogicHandCard : GE_Logic
	{
		public SO.GameEvent onCurrentCardSelected;
		public CardVariable currentCard;
		public CM.GameStates.GameState holdingCard;
		
		public override void OnClick(CardInstance inst)
		{
			currentCard.Set(inst);
			Settings.gameManager.SetState(holdingCard);
			//여기를 inst에 있는 타겟 에리어로 정해줘야함
			onCurrentCardSelected.Raise();
		}
		
		public override void OnHighlight(CardInstance inst)
		{
			
		}
	}
}