using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Areas/LaneWhileHoldingCard")]
public class Area_LogicLane : Area_Logic
{
    public CardVariable card;
    public CardType cardTypeMonster;
    public SO.TransformArrayVariable areaGridTransform;
    public CM.GameElements.GE_Logic cardDownLogic;

    public override void Execute(int laneNum)
    {
        if (card.value == null)
            return;

        if (card.value.vis.card.cardType == cardTypeMonster)
        {
            if (Settings.gameManager.currentPlayer.canUseCard == true)
            {
                Settings.SetParentForCard(card.value.transform, areaGridTransform.value[laneNum].transform);
                card.value.currentLogic = cardDownLogic;
                card.value.gameObject.SetActive(true);
                Settings.gameManager.currentPlayer.canUseCard = false;
            }
            else
            {
                Debug.Log("You Already Played a Card This Turn");
            }
        }
    }
}
