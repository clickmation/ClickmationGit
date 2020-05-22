using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Holders/Player Holder")]
public class PlayerHolder : ScriptableObject
{
    public string userName;
    public Color playerColor;
    public string[] startingCards;

    public CM.GameElements.GE_Logic handLogic;
    public CM.GameElements.GE_Logic laneLogic;

    [System.NonSerialized]
    public CardHolders currentHolder;

    [System.NonSerialized]
    public List<CardInstance> handCards = new List<CardInstance>();
    [System.NonSerialized]
    public List<List<CardInstance>> laneCards = new List<List<CardInstance>>();
    [System.NonSerialized]
    public bool canUseCard = false;

    public bool first;

    public void DropCard(CardInstance inst, int laneNum)
    {
        if (handCards.Contains(inst))
            handCards.Remove(inst);
        
        laneCards[laneNum].Add(inst);
    }

    public void AddLane ()
    {
        laneCards.Add(new List<CardInstance>());
    }
}
