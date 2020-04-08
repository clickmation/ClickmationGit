using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaManager : MonoBehaviour
{
    public GameObject laneArea;
    public GameObject laneCard;
    public Transform cardsDown;
    public SO.TransformArrayVariable laneTransformArray;

    public void SetLane(int numOfLane)
    {
        if (this.transform.Find("Lane").childCount != 0 || cardsDown.childCount != 0)
        {
            return;
        }

        GameObject[] areaClone = new GameObject[numOfLane];
        GameObject[] cardClone = new GameObject[numOfLane];

        laneTransformArray.Init(numOfLane);

        //Instantiate Lanes
        for (int i = 0; i < numOfLane; i++)
        {
            areaClone[i] = Instantiate(laneArea, this.transform.Find("Lane").position, this.transform.Find("Lane").rotation, this.transform.Find("Lane")) as GameObject;
            cardClone[i] = Instantiate(laneCard, cardsDown.position, cardsDown.rotation, cardsDown) as GameObject;
            CM.GameElements.Area a = areaClone[i].GetComponent<CM.GameElements.Area>();
            a.laneNum = i;
            laneTransformArray.ReplaceAt(cardClone[i].transform, i);
        }

        //Set Grid Size
        GridLayoutGroup areaGrid = this.transform.Find("Lane").GetComponent<GridLayoutGroup>();
        GridLayoutGroup cardGrid = cardsDown.GetComponent<GridLayoutGroup>();;
        areaGrid.cellSize = new Vector2 ((1600 - 20*(numOfLane-1))/numOfLane, 400);
        cardGrid.cellSize = new Vector2 ((1600 - 20*(numOfLane-1))/numOfLane, 400);
    }
}
