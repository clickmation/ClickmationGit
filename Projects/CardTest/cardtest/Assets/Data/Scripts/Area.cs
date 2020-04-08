using System.Collections;
using UnityEngine;


namespace CM.GameElements
{
    public class Area : MonoBehaviour
    {
        public Area_Logic logic;
        public int laneNum;

        public void OnDrop()
        {
            logic.Execute(laneNum);
        }
    }
}

