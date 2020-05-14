using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaManager : MonoBehaviour
{
    public GameObject laneP1Area;
    public GameObject laneP1Card;
    public GameObject laneP2Area;
    public GameObject laneP2Card;
    public Transform cardsP1Down;
    public Transform cardsP2Down;
    public SO.TransformArrayVariable laneP1TransformArray;
    public SO.TransformArrayVariable laneP2TransformArray;

    public void SetLane(int numOfLane)
    {
        //Setting P1 Lane
        if (this.transform.Find("P1 Lane").childCount != 0 || cardsP1Down.childCount != 0)
        {
            return;
        }

        GameObject[] areaP1Clone = new GameObject[numOfLane];
        GameObject[] cardP1Clone = new GameObject[numOfLane];

        laneP1TransformArray.Init(numOfLane);

        //Instantiate Lanes
        for (int i = 0; i < numOfLane; i++)
        {
            areaP1Clone[i] = Instantiate(laneP1Area, this.transform.Find("P1 Lane").position, this.transform.Find("P1 Lane").rotation, this.transform.Find("P1 Lane")) as GameObject;
            cardP1Clone[i] = Instantiate(laneP1Card, cardsP1Down.position, cardsP1Down.rotation, cardsP1Down) as GameObject;
            CM.GameElements.Area a = areaP1Clone[i].GetComponent<CM.GameElements.Area>();
            a.laneNum = i;
            laneP1TransformArray.ReplaceAt(cardP1Clone[i].transform, i);
        }

        //Setting P2 Lane
        if (this.transform.Find("P2 Lane").childCount != 0 || cardsP2Down.childCount != 0)
        {
            return;
        }

        GameObject[] areaP2Clone = new GameObject[numOfLane];
        GameObject[] cardP2Clone = new GameObject[numOfLane];

        laneP2TransformArray.Init(numOfLane);

        //Instantiate Lanes
        for (int i = 0; i < numOfLane; i++)
        {
            areaP2Clone[i] = Instantiate(laneP2Area, this.transform.Find("P2 Lane").position, this.transform.Find("P2 Lane").rotation, this.transform.Find("P2 Lane")) as GameObject;
            cardP2Clone[i] = Instantiate(laneP2Card, cardsP2Down.position, cardsP2Down.rotation, cardsP2Down) as GameObject;
            CM.GameElements.Area a = areaP2Clone[i].GetComponent<CM.GameElements.Area>();
            a.laneNum = i;
            laneP2TransformArray.ReplaceAt(cardP2Clone[i].transform, i);
        }

        //Set Grid Size
        GridLayoutGroup areaP1Grid = this.transform.Find("P1 Lane").GetComponent<GridLayoutGroup>();
        GridLayoutGroup cardP1Grid = cardsP1Down.GetComponent<GridLayoutGroup>();;
        areaP1Grid.cellSize = new Vector2 ((1600 - 20*(numOfLane-1))/numOfLane, 400);
        cardP1Grid.cellSize = new Vector2 ((1600 - 20*(numOfLane-1))/numOfLane, 400);
        GridLayoutGroup areaP2Grid = this.transform.Find("P2 Lane").GetComponent<GridLayoutGroup>();
        GridLayoutGroup cardP2Grid = cardsP2Down.GetComponent<GridLayoutGroup>();;
        areaP2Grid.cellSize = new Vector2 ((1600 - 20*(numOfLane-1))/numOfLane, 400);
        cardP2Grid.cellSize = new Vector2 ((1600 - 20*(numOfLane-1))/numOfLane, 400);
    }
}
