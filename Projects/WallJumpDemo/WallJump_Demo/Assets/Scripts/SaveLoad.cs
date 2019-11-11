using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad : MonoBehaviour
{
    public static SaveLoad saveload;
    public MainMenu mainMenu;
    public GameMaster gm;
    public AudioManager am;
    public DailyReward dr;
    public RegularReward rr;

    private SaveManager tmpSaver = new SaveManager();

    // Use this for initialization
    void Awake()
    {
        if (saveload == null)
        {
            saveload = this;
        }
    }

    public void MainMenuSave()
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream fstream = File.Create(Application.persistentDataPath + "/saveFile.WJM");

        SaveManager saver = new SaveManager();
        saver = tmpSaver;
        saver.highScore = mainMenu.highScore;
        saver.allScores = mainMenu.allScores;
        saver.coin = mainMenu.coin;
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

    public void MainMenuLoad()
    {
        if (File.Exists(Application.persistentDataPath + "/saveFile.WJM"))
        {
            BinaryFormatter binary = new BinaryFormatter();
            FileStream fstream = File.Open(Application.persistentDataPath + "/saveFile.WJM", FileMode.Open);
            SaveManager saver = (SaveManager)binary.Deserialize(fstream);
            fstream.Close();

            mainMenu.highScore = saver.highScore;
            mainMenu.allScores = saver.allScores;
            mainMenu.coin = saver.coin;
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
            tmpSaver = saver;
        }
    }

    public void GMLoad()
    {
        if (File.Exists(Application.persistentDataPath + "/saveFile.WJM"))
        {
            BinaryFormatter binary = new BinaryFormatter();
            FileStream fstream = File.Open(Application.persistentDataPath + "/saveFile.WJM", FileMode.Open);
            SaveManager saver = (SaveManager)binary.Deserialize(fstream);
            fstream.Close();

            gm.highScore = saver.highScore;
            gm.adToken = saver.adToken;
            tmpSaver = saver;
        }
    }

    public void GMSave()
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream fstream = File.Create(Application.persistentDataPath + "/saveFile.WJM");

        SaveManager saver = new SaveManager();

        saver = tmpSaver;
        saver.highScore = gm.highScore;
        saver.allScores = tmpSaver.allScores + gm.score;
        saver.coin = tmpSaver.coin + gm.coin;
        saver.allCoins = tmpSaver.allCoins + gm.coin;
        saver.allDeaths = tmpSaver.allDeaths + gm.deathCount;

        binary.Serialize(fstream, saver);
        fstream.Close();
    }

    public void SoundLoad()
    {
        if (File.Exists(Application.persistentDataPath + "/saveFile.WJM"))
        {
            BinaryFormatter binary = new BinaryFormatter();
            FileStream fstream = File.Open(Application.persistentDataPath + "/saveFile.WJM", FileMode.Open);
            SaveManager saver = (SaveManager)binary.Deserialize(fstream);
            fstream.Close();

            if (mainMenu != null)
            {

                mainMenu.masterVolume = saver.masterVolume;
                mainMenu.soundEffectVolume = saver.soundEffectVolume;
                mainMenu.bgmVolume = saver.bgmVolume;
            }
            else
            {
                Debug.Log(gm);
                gm.masterVolume = saver.masterVolume;
                gm.soundEffectVolume = saver.soundEffectVolume;
                gm.bgmVolume = saver.bgmVolume;
            }

            tmpSaver = saver;
        }
    }

    public void SoundSave()
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream fstream = File.Create(Application.persistentDataPath + "/saveFile.WJM");

        SaveManager saver = new SaveManager();

        saver = tmpSaver;
        saver.masterVolume = am.masterVolume;
        saver.soundEffectVolume = am.soundEffectVolume;
        saver.bgmVolume = am.bgmVolume;

        binary.Serialize(fstream, saver);
        fstream.Close();
    }

    public void TokenLoad()
    {
        if (File.Exists(Application.persistentDataPath + "/saveFile.WJM"))
        {
            BinaryFormatter binary = new BinaryFormatter();
            FileStream fstream = File.Open(Application.persistentDataPath + "/saveFile.WJM", FileMode.Open);
            SaveManager saver = (SaveManager)binary.Deserialize(fstream);
            fstream.Close();

            if (mainMenu != null) mainMenu.adToken = tmpSaver.adToken;
            else gm.adToken = tmpSaver.adToken;
            tmpSaver = saver;
        }
    }

    public void TokenSave(char s)
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream fstream = File.Create(Application.persistentDataPath + "/saveFile.WJM");

        SaveManager saver = new SaveManager();

        saver = tmpSaver;
        if (s == '+') saver.adToken = ++tmpSaver.adToken;
        else if (s == '-') saver.adToken = --tmpSaver.adToken;
        else Debug.LogError("What the fuck are you doing?");

        if (mainMenu != null)
        {
            mainMenu.adToken = tmpSaver.adToken;
            mainMenu.adTokenText.text = mainMenu.adToken.ToString();
        }
        else gm.adToken = tmpSaver.adToken;

        binary.Serialize(fstream, saver);
        fstream.Close();
    }

    public void DailyRewardLoad()
    {
        if (File.Exists(Application.persistentDataPath + "/saveFile.WJM"))
        {
            BinaryFormatter binary = new BinaryFormatter();
            FileStream fstream = File.Open(Application.persistentDataPath + "/saveFile.WJM", FileMode.Open);
            SaveManager saver = (SaveManager)binary.Deserialize(fstream);
            fstream.Close();

            dr.SetRefreshDate(saver.refreshDate);
            dr.curStack = saver.curStack;

            tmpSaver = saver;
        }
    }

    public void DailyRewardSave(char s)
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream fstream = File.Create(Application.persistentDataPath + "/saveFile.WJM");

        SaveManager saver = new SaveManager();

        saver = tmpSaver;

        saver.refreshDate = dr.GetRefreshDate();
        if (s == '-') saver.curStack = tmpSaver.curStack - 1;
        else if (s == 'r') saver.curStack = 3;
        else Debug.LogError("What the fuck are you doing?");

        binary.Serialize(fstream, saver);
        fstream.Close();
    }

    public void RegularRewardLoad()
    {
        if (File.Exists(Application.persistentDataPath + "/saveFile.WJM"))
        {
            BinaryFormatter binary = new BinaryFormatter();
            FileStream fstream = File.Open(Application.persistentDataPath + "/saveFile.WJM", FileMode.Open);
            SaveManager saver = (SaveManager)binary.Deserialize(fstream);
            fstream.Close();

            rr.SetRefreshDateTime(saver.refreshDateTime);

            tmpSaver = saver;
        }
    }

    public void RegularRewardSave()
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream fstream = File.Create(Application.persistentDataPath + "/saveFile.WJM");

        SaveManager saver = new SaveManager();

        saver = tmpSaver;

        saver.refreshDateTime = rr.GetRefreshDateTime();

        binary.Serialize(fstream, saver);
        fstream.Close();
    }

    //public void Load()
    //{
    //    if (File.Exists(Application.persistentDataPath + "/saveFile.WJM"))
    //    {
    //        BinaryFormatter binary = new BinaryFormatter();
    //        FileStream fstream = File.Open(Application.persistentDataPath + "/saveFile.WJM", FileMode.Open);
    //        SaveManager saver = (SaveManager)binary.Deserialize(fstream);
    //        fstream.Close();

    //        tmpSaver = saver;
    //    }
    //}

    public void RandomCoinSave(int c)
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream fstream = File.Create(Application.persistentDataPath + "/saveFile.WJM");

        SaveManager saver = new SaveManager();

        saver = tmpSaver;

        saver.coin = tmpSaver.coin + c;
        mainMenu.AdCoinUI(c);

        binary.Serialize(fstream, saver);
        fstream.Close();
    }
}

[System.Serializable]
class SaveManager
{

    [Space]

    [Header("MainMenuOnly")]

    public int curTrailIndex;
    public int curCharacterIndex;
    public int curBGMIndex;
    public int[] trailsArray = new int[21];
    public int[] charactersArray = new int[21];
    public int[] bgmsArray = new int[21];

    [Space]

    [Header("GameMasterOnly")]

    public int allDeaths;
    public int allJumps;
    public int allKills;

    [Space]

    [Header("Both")]

    public int adToken;
    public int adTokenStack;
    public int highScore;
    public int allScores;
    public int coin;
    public int allCoins;
    public float masterVolume;
    public float soundEffectVolume;
    public float bgmVolume;

    [Space]

    [Header("Rewards")]

    public DateTime refreshDate;
    public DateTime refreshDateTime;
    public int curStack;
}