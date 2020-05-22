using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleManager : MonoBehaviour
{
    public Transform consoleGrid;
    public GameObject prefab;
    Text[] textObjects;
    int index;

    public ConsoleHook hook;

    private void Awake()
    {
        hook.consoleManager = this;

        textObjects = new Text[11];
        for (int i = 0; i < textObjects.Length; i++)
        {
            GameObject clone = Instantiate(prefab) as GameObject;
            textObjects[i] = clone.GetComponent<Text>();
            clone.transform.SetParent(consoleGrid);
        }
    }

    public void RegisterEvent(string e, Color color)
    {
        index++;
        if(index > textObjects.Length - 1)
        {
            index = 0;
        }

        textObjects[index].color = color;
        textObjects[index].text = e;
        textObjects[index].gameObject.SetActive(true);
        textObjects[index].transform.SetAsLastSibling();
    }
}
