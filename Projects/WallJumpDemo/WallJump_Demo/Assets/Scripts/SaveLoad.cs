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
        saver.highScore = mainMenu.highScore;
        saver.allScores = mainMenu.allScores;
        saver.coin = mainMenu.coin;
        saver.allCoins = mainMenu.allCoins;
        saver.allDeaths = mainMenu.allDeaths;
        saver.allJumps = mainMenu.allJumps;
        saver.allKills = mainMenu.allKills;
        saver.adToken = mainMenu.adToken;
        saver.curTrailIndex = mainMenu.curTrailIndex;
        saver.curCharacterIndex = mainMenu.curCharacterIndex;
        saver.curBGMIndex = mainMenu.curBGMIndex;
        for (int i = 0; i < 20; i++)
        {
            saver.trailsArray[i] = mainMenu.trailsArray[i];
            saver.charactersArray[i] = mainMenu.charactersArray[i];
            saver.bgmsArray[i] = mainMenu.bgmsArray[i];
        }
        saver.masterVolume = mainMenu.masterVolume;
        saver.soundEffectVolume = mainMenu.soundEffectVolume;
        saver.bgmVolume = mainMenu.bgmVolume;

        binary.Serialize(fstream, saver);
        fstream.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/saveFile.WJM"))
        {
            BinaryFormatter binary = new BinaryFormatter();
            FileStream fstream = File.Open(Application.persistentDataPath + "/saveFile.WJM", FileMode.Open);
            SaveManager saver = (SaveManager)binary.Deserialize(fstream);
            fstream.Close();
            //Debug.Log("Loaded.");

            mainMenu.highScore = saver.highScore;
            mainMenu.allScores = saver.allScores;
            mainMenu.coin = saver.coin;
            mainMenu.allCoins = saver.allCoins;
            mainMenu.allDeaths = saver.allDeaths;
            mainMenu.allJumps = saver.allJumps;
            mainMenu.allKills = saver.allKills;
            mainMenu.adToken = saver.adToken;
            mainMenu.curTrailIndex = saver.curTrailIndex;
            mainMenu.curCharacterIndex = saver.curCharacterIndex;
            mainMenu.curBGMIndex = saver.curBGMIndex;
            for (int i = 0; i < 20; i++)
            {
                mainMenu.trailsArray[i] = saver.trailsArray[i];
                mainMenu.charactersArray[i] = saver.charactersArray[i];
                mainMenu.bgmsArray[i] = saver.bgmsArray[i];
            }
            mainMenu.masterVolume = saver.masterVolume;
            mainMenu.soundEffectVolume = saver.soundEffectVolume;
            mainMenu.bgmVolume = saver.bgmVolume;
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
    public int highScore;
    public int allScores;
    public int coin;
    public int allCoins;
    public int allDeaths;
    public int allJumps;
    public int allKills;
    public int adToken;
    public int adTokenStack;
    public int curTrailIndex;
    public int curCharacterIndex;
    public int curBGMIndex;
    public int[] trailsArray = new int[20];
    public int[] charactersArray = new int[20];
    public int[] bgmsArray = new int[20];
    public float masterVolume;
    public float soundEffectVolume;
    public float bgmVolume;
}