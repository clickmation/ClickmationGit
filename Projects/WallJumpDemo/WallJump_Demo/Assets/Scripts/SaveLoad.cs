using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad : MonoBehaviour
{
    public static SaveLoad saveload;
    public MainMenu mainMenu;

    // Use this for initialization
    void Awake()
    {
        if (saveload == null)
        {
            saveload = this;
        }
        //DontDestroyOnLoad(this.gameObject);
        //Debug.LogError ("saveload Awake");
    }

    public void Save()
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream fstream = File.Create(Application.persistentDataPath + "/saveFile.WJM");
        //Debug.Log(Application.persistentDataPath + "/saveFile.WJM");

        SaveManager saver = new SaveManager();
        saver.coin = mainMenu.coin;
        saver.curTrailIndex = mainMenu.curTrailIndex;
        saver.curCharacterIndex = mainMenu.curCharacterIndex;
        for (int i = 0; i < 20; i++)
        {
            saver.trailsArray[i] = mainMenu.trailsArray[i];
            saver.charactersArray[i] = mainMenu.charactersArray[i];
            saver.bgmsArray[i] = mainMenu.bgmsArray[i];
        }

        binary.Serialize(fstream, saver);
        fstream.Close();
        Debug.Log("Saved.");
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/saveFile.WJM"))
        {
            BinaryFormatter binary = new BinaryFormatter();
            FileStream fstream = File.Open(Application.persistentDataPath + "/saveFile.WJM", FileMode.Open);
            SaveManager saver = (SaveManager)binary.Deserialize(fstream);
            fstream.Close();
            Debug.Log("Loaded.");

            mainMenu.coin = saver.coin;
            mainMenu.curTrailIndex = saver.curTrailIndex;
            mainMenu.curCharacterIndex = saver.curCharacterIndex;
            for (int i = 0; i < 20; i++)
            {
                mainMenu.trailsArray[i] = saver.trailsArray[i];
                mainMenu.charactersArray[i] = saver.charactersArray[i];
                mainMenu.bgmsArray[i] = saver.bgmsArray[i];
            }
        }
    }

    public void FirstPlay()
    {
        Debug.Log("First");
        
    }

    IEnumerator WaitToChange()
    {
        yield return new WaitForSeconds(0.1f);


    }
}

[System.Serializable]
class SaveManager
{
    public int coin;
    public int curTrailIndex;
    public int curCharacterIndex;
    public int[] trailsArray = new int[20];
    public int[] charactersArray = new int[20];
    public int[] bgmsArray = new int[20];
}