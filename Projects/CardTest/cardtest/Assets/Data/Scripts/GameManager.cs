using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CM.GameStates;

public class GameManager : MonoBehaviour
{
	public PlayerHolder currentPlayer;
    public GameState currentState;
	public GameObject cardPrefab;
	public AreaManager areaManager;

	public int turnIndex;
	public Turn[] turns;

	public int numOfLane;
	
	private void Start()
	{
		Settings.gameManager = this;
		CreateStartingCards();
		areaManager.SetLane(numOfLane);
	}

	void CreateStartingCards()
	{
		ResourcesManager rm = Settings.GetResourcesManager();
		
		for (int i = 0; i < currentPlayer.startingCards.Length; i++)
		{
			GameObject go = Instantiate(cardPrefab) as GameObject;
			CardVis v = go.GetComponent<CardVis>();
			v.LoadCard(rm.GetCardInstance(currentPlayer.startingCards[i]));
			CardInstance inst = go.GetComponent<CardInstance>();
			inst.currentLogic = currentPlayer.handLogic;
			Settings.SetParentForCard(go.transform, currentPlayer.handGrid.value);
		}
	}
	
	private void Update()
	{
		bool isComplete = turns[turnIndex].Execute();

		if (isComplete)
		{
			turnIndex++;
			if (turnIndex > turns.Length - 1)
			{
				turnIndex = 0;
			}
		}

		if (currentState != null)
			currentState.Tick(Time.deltaTime);
	}
	
	public void SetState(GameState state)
	{
		currentState = state;
	}
}
