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

    [SerializeField] int highScore;
    [SerializeField] int allScores;
    [SerializeField] int coin;
    [SerializeField] int allCoins;
    [SerializeField] int allDeaths;
    [SerializeField] int allJumps;
    [SerializeField] int allKills;
    [SerializeField] int adToken;
    [SerializeField] int curTrailIndex;
    [SerializeField] int curCharacterIndex;
    [SerializeField] int curBGMIndex;
    [SerializeField] int[] trailsArray;
    [SerializeField] int[] charactersArray;
    [SerializeField] int[] bgmsArray;
    [SerializeField] float masterVolume;
    [SerializeField] float soundEffectVolume;
    [SerializeField] float bgmVolume;

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

    public void MainMenuSave()
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream fstream = File.Create(Application.persistentDataPath + "/saveFile.WJM");

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

    public void MainMenuLoad()
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

    public void GMLoad()
    {
        if (File.Exists(Application.persistentDataPath + "/saveFile.WJM"))
        {
            trailsArray = new int[20];
            charactersArray = new int[20];
            bgmsArray = new int[20]; 

            BinaryFormatter binary = new BinaryFormatter();
            FileStream fstream = File.Open(Application.persistentDataPath + "/saveFile.WJM", FileMode.Open);
            SaveManager saver = (SaveManager)binary.Deserialize(fstream);
            fstream.Close();

            gm.highScore = saver.highScore;
            allScores = saver.allScores;
            coin = saver.coin;
            allCoins = saver.allCoins;
            allDeaths = saver.allDeaths;
            allJumps = saver.allJumps;
            allKills = saver.allKills;
            adToken = saver.adToken;
            curTrailIndex = saver.curTrailIndex;
            curCharacterIndex = saver.curCharacterIndex;
            curBGMIndex = saver.curBGMIndex;
            for (int i = 0; i < 20; i++)
            {
                trailsArray[i] = saver.trailsArray[i];
                charactersArray[i] = saver.charactersArray[i];
                bgmsArray[i] = saver.bgmsArray[i];
            }
            masterVolume = saver.masterVolume;
            soundEffectVolume = saver.soundEffectVolume;
            bgmVolume = saver.bgmVolume;
        }
    }

    public void GMSave()
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream fstream = File.Create(Application.persistentDataPath + "/saveFile.WJM");
        //Debug.Log(Application.persistentDataPath + "/saveFile.WJM");

        SaveManager saver = new SaveManager();

        saver.highScore = gm.highScore;
        saver.allScores = allScores + gm.score;
        saver.coin = coin + gm.coin;
        saver.allCoins = allCoins + gm.coin;
        saver.allDeaths = allDeaths + gm.deathCount;
        saver.allJumps = allJumps; ;
        saver.allKills = allKills;
        saver.adToken = adToken;
        saver.curTrailIndex = curTrailIndex;
        saver.curCharacterIndex = curCharacterIndex;
        saver.curBGMIndex = curBGMIndex;
        for (int i = 0; i < 20; i++)
        {
            saver.trailsArray[i] = trailsArray[i];
            saver.charactersArray[i] = charactersArray[i];
            saver.bgmsArray[i] = bgmsArray[i];
        }
        saver.masterVolume = masterVolume;
        saver.soundEffectVolume = soundEffectVolume;
        saver.bgmVolume = bgmVolume;

        //allScores = 0;
        //coin = 0;
        //allCoins = 0;
        //allDeaths = 0;
        //allJumps = 0;
        //allKills = 0;
        //adToken = 0;
        //curTrailIndex = 0;
        //curCharacterIndex = 0;
        //curBGMIndex = 0;
        //trailsArray = null;
        //charactersArray = null;
        //bgmsArray = null;
        //masterVolume = 0f;
        //soundEffectVolume = 0f;
        //bgmVolume = 0f;

        binary.Serialize(fstream, saver);
        fstream.Close();
    }

    public void SoundLoad()
    {
        if (File.Exists(Application.persistentDataPath + "/saveFile.WJM"))
        {
            trailsArray = new int[20];
            charactersArray = new int[20];
            bgmsArray = new int[20];

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

            //PlayerPrefs.SetFloat("MasterVolume", saver.masterVolume);
            //PlayerPrefs.SetFloat("SoundEffectVolume", saver.soundEffectVolume);
            //PlayerPrefs.SetFloat("BGMVolume", saver.bgmVolume);gm.highScore = saver.highScore;

            highScore = saver.highScore;
            allScores = saver.allScores;
            coin = saver.coin;
            allCoins = saver.allCoins;
            allDeaths = saver.allDeaths;
            allJumps = saver.allJumps;
            allKills = saver.allKills;
            adToken = saver.adToken;
            curTrailIndex = saver.curTrailIndex;
            curCharacterIndex = saver.curCharacterIndex;
            curBGMIndex = saver.curBGMIndex;
            for (int i = 0; i < 20; i++)
            {
                trailsArray[i] = saver.trailsArray[i];
                charactersArray[i] = saver.charactersArray[i];
                bgmsArray[i] = saver.bgmsArray[i];
            }
        }
    }

    public void SoundSave()
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream fstream = File.Create(Application.persistentDataPath + "/saveFile.WJM");

        SaveManager saver = new SaveManager();

        saver.masterVolume = am.masterVolume;
        saver.soundEffectVolume = am.soundEffectVolume;
        saver.bgmVolume = am.bgmVolume;
        //PlayerPrefs.SetFloat("MasterVolume", 0);
        //PlayerPrefs.SetFloat("SoundEffectVolume", 0);
        //PlayerPrefs.SetFloat("BGMVolume", 0);

        saver.highScore = highScore;
        saver.allScores = allScores;
        saver.coin = coin;
        saver.allCoins = allCoins;
        saver.allDeaths = allDeaths;
        saver.allJumps = allJumps;
        saver.allKills = allKills;
        saver.adToken = adToken;
        saver.curTrailIndex = curTrailIndex;
        saver.curCharacterIndex = curCharacterIndex;
        saver.curBGMIndex = curBGMIndex;
        for (int i = 0; i < 20; i++)
        {
            saver.trailsArray[i] = trailsArray[i];
            saver.charactersArray[i] = charactersArray[i];
            saver.bgmsArray[i] = bgmsArray[i];
        }

        //highScore = 0;
        //allScores = 0;
        //coin = 0;
        //allCoins = 0;
        //allDeaths = 0;
        //allJumps = 0;
        //allKills = 0;
        //adToken = 0;
        //curTrailIndex = 0;
        //curCharacterIndex = 0;
        //curBGMIndex = 0;
        //trailsArray = null;
        //charactersArray = null;
        //bgmsArray = null;
        //masterVolume = 0f;
        //soundEffectVolume = 0f;
        //bgmVolume = 0f;

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
    public int[] trailsArray = new int[20];
    public int[] charactersArray = new int[20];
    public int[] bgmsArray = new int[20];

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
}