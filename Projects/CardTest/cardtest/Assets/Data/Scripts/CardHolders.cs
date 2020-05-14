using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Holders/Card Holder")]
public class CardHolders : ScriptableObject
{
    public SO.TransformVariable handGrid;
    public SO.TransformArrayVariable laneGrid;

    public void LoadPlayer(PlayerHolder p)
    {
        foreach (CardInstance c in p.handCards)
        {
            Settings.SetParentForCard(c.vis.gameObject.transform, handGrid.value.transform);
        }
        for (int i = 0; i < Settings.gameManager.numOfLane; i++)
        {
            foreach (CardInstance c in p.laneCards[i])
            {
                Settings.SetParentForCard(c.vis.gameObject.transform, laneGrid.value[i].transform);
            }
        }
    }
}
