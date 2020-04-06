using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace CM.GameStates
{
	[CreateAssetMenu(menuName = "Actions/MouseHold")]
	public class MouseHold : Action
	{
		public GameState playerControlState;
		public SO.GameEvent onPlayerControlState;
		
		public override void Execute(float d)
		{
			bool mouseIsDown = Input.GetMouseButton(0);
			
			if (!mouseIsDown)
			{
				List<RaycastResult> results = Settings.GetUIObjs();
				
				foreach (RaycastResult r in results)
				{
					
				}
				
				Settings.gameManager.SetState(playerControlState);
				onPlayerControlState.Raise();
				return;
			}
		}
	}
}