using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Holders/Player Holder")]
public class PlayerHolder : ScriptableObject
{
    public  string[] startingCards;
    public SO.TransformVariable handGrid;
    public SO.TransformArrayVariable laneGrid;

    public CM.GameElements.GE_Logic handLogic;
    public CM.GameElements.GE_Logic laneLogic;

    [System.NonSerialized]
    public List<CardInstance> handCards = new List<CardInstance>();
    [System.NonSerialized]
    public List<List<CardInstance>> laneCards = new List<List<CardInstance>>();

    public void CardToLane (int laneNum, CardInstance c)
    {
        laneCards[laneNum].Add(c);
    }
}
