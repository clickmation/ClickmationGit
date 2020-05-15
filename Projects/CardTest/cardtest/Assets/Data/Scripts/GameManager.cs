using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CM.GameStates;

public class GameManager : MonoBehaviour
{
	public PlayerHolder[] allPlayers;
	public PlayerHolder currentPlayer;
	public CardHolders playerOneHolder;
	public CardHolders playerTwoHolder;

    public GameState currentState;
	public GameObject cardPrefab;
	public AreaManager areaManager;

	public int turnIndex;
	public Turn[] turns;
	public SO.GameEvent onTurnChanged;
	public SO.GameEvent onPhaseChanged;
	public SO.StringVariable turnText;

	public int numOfLane;
	
	private void Start()
	{
		Settings.gameManager = this;

		SetupPlayers();

		CreateStartingCards();

		areaManager.SetLane(numOfLane);
		
		currentPlayer = turns[turnIndex].player;

		turnText.value = currentPlayer.userName + " Turn";
		onTurnChanged.Raise();
	}

	void SetupPlayers()
	{
		foreach (PlayerHolder p in allPlayers)
		{
			if (p.first)
			{
				p.currentHolder = playerOneHolder;
			}
			else
			{
				p.currentHolder = playerTwoHolder;
			}

			for (int i = 0; i < numOfLane; i++)
			{
				p.AddLane();
			}
		}
	}

	void CreateStartingCards()
	{
		ResourcesManager rm = Settings.GetResourcesManager();
		
		for (int p = 0; p < allPlayers.Length; p++)
		{
			for (int i = 0; i < allPlayers[p].startingCards.Length; i++)
			{
				GameObject go = Instantiate(cardPrefab) as GameObject;
				CardVis v = go.GetComponent<CardVis>();
				v.LoadCard(rm.GetCardInstance(allPlayers[p].startingCards[i]));
				CardInstance inst = go.GetComponent<CardInstance>();
				inst.currentLogic = allPlayers[p].handLogic;
				Settings.SetParentForCard(go.transform, allPlayers[p].currentHolder.handGrid.value);
				allPlayers[p].handCards.Add(inst);
			}
		}
		
	}

	public bool switchPlayer;
	
	private void Update()
	{
		if (switchPlayer)
		{
			switchPlayer = false;

			playerOneHolder.LoadPlayer(allPlayers[0]);
			playerTwoHolder.LoadPlayer(allPlayers[1]);
		}

		bool isComplete = turns[turnIndex].Execute();

		if (isComplete)
		{
			turnIndex++;
			if (turnIndex > turns.Length - 1)
			{
				turnIndex = 0;
			}

			currentPlayer = turns[turnIndex].player;

			turnText.value = currentPlayer.userName + " Turn";
			onTurnChanged.Raise();
		}

		if (currentState != null)
			currentState.Tick(Time.deltaTime);
	}
	
	public void SetState(GameState state)
	{
		currentState = state;
	}

	public void EndCurrentPhase()
	{
		turns[turnIndex].EndCurrentPhase();
	}
}
