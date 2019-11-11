﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    AudioManager am;
    LanguageSet ls;

    [Space]

    [Header("Texts")]

    public Text startText;
    public Text howToText;
    public Text shopText;
    public Text customizationText;
    public Text quitCheckText;
    public Text getTokenText;
    public Text getCoinText;

    public Text coinButtonText;

    public Text[] tutText;

    public Text trailText;
    public Text skinText;

    public Text soundText;
    public Text masterText;
    public Text soundEffectText;
    public Text bgmText;

    public Text congratulationText;

    public Text yesText;
    public Text noText;

    [Space]

    [Header("UI")]

    public Text highScoreText;
    public Text coinText;
    public Text adTokenText;

    [Space]

    [Header("SaveLoad")]

    public int highScore;
    public int allScores;
    public int coin;
    public int adToken;
    public int curTrailIndex;
    public int curCharacterIndex;
    public int curBGMIndex;
    public int[] trailsArray = new int[21];
    public int[] charactersArray = new int[21];
    public int[] bgmsArray = new int[21];
    public float masterVolume;
    public float soundEffectVolume;
    public float bgmVolume;
    public List<int> availableTrails;
    public List<int> availableCharacters;
    public List<int> availableBGMs;

    [Space]

    [Header("Booleans")]

    [SerializeField] bool shop;
    [SerializeField] bool howTo;
    [SerializeField] bool customization;
    [SerializeField] bool sound;
    [SerializeField] bool credit;
    [SerializeField] bool popUp;
    [SerializeField] bool quitCheck;

    [Space]

    [Header("Panels")]

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject shopObj;
    [SerializeField] GameObject howToObj;
    [SerializeField] GameObject cusObj;
    [SerializeField] GameObject soundObj;
    [SerializeField] GameObject creditObj;
    [SerializeField] GameObject adTokenButton;
    [SerializeField] GameObject adCoinButton;
    [SerializeField] GameObject popUpUI;
    [SerializeField] GameObject quitChenkUI;

    [Space]

    [Header("Buttons")]

    [SerializeField] Image shopButton;
    [SerializeField] Image howToButton;
    [SerializeField] Image gameStartButton;
    [SerializeField] Image customizationButton;
    [SerializeField] Image soundButton;

    [Space]

    [Header("Shop")]

    [SerializeField] GameObject coinObject;
    public Image prizeImage;
    public Text prizeName;
    public SpriteRenderer prizeObject;
    public GameObject prizeTrailObject;
    [SerializeField] GameObject prizeCoinObject;
    [SerializeField] Transform shopTransform;

    [Space]

    [Header("HowTo")]

    public int howToIndex;
    public GameObject backButton;
    public GameObject nextButton;
    public GameObject[] scenes;

    [Space]

    [Header("Customization")]

    public int trailIndex;
    public GameObject curTrail;
    public Text trailName;
    public Transform show;
    [SerializeField] Image trailBackButton;
    [SerializeField] Image trailNextButton;
    public Trail[] trails;
    [System.Serializable]
    public struct Trail
    {
        public string name;
        public GameObject trail;
    }

    public int characterIndex;
    public SpriteRenderer curSprite;
    public Text characterName;
    [SerializeField] Image characterBackButton;
    [SerializeField] Image characterNextButton;
    public Character[] characters;
    [System.Serializable]
    public struct Character
    {
        public string name;
        public Sprite sprite;
    }

    [Space]

    [Header("Sound")]

    public int bgmIndex;
    public Image bgmImage;
    public Text bgmName;
    [SerializeField] Image bgmBackButton;
    [SerializeField] Image bgmNextButton;
    public BGM[] bgms;
    [System.Serializable]
    public struct BGM
    {
        public string name;
        public AudioClip bgm;
    }
    public Slider masterVolumeSlider;
    public Slider soundEffectVolumeSlider;
    public Slider bgmVolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        SaveLoad.saveload.mainMenu = this;
        am = AudioManager.audioManager;
        ls = LanguageSet.ls;
        ls.mm = this;
        am.mainMenu = this;
        am.SetAudioSources();
        SaveLoad.saveload.MainMenuLoad();
        highScoreText.text = highScore.ToString();
        coinText.text = coin.ToString();
        adTokenText.text = adToken.ToString();
        MainMenuLanguageSet();

        //PlayerPrefs.SetInt("CurTrailIndex", 0);
        //PlayerPrefs.SetInt("CurCharacterIndex", 0);
        //PlayerPrefs.SetInt("CurBGMIndex", 0);

        //curTrailIndex = PlayerPrefs.GetInt("CurTrailIndex");
        //curCharacterIndex = PlayerPrefs.GetInt("CurCharacterIndex");
        //curBGMIndex = PlayerPrefs.GetInt("CurBGMIndex");
        //PlayerPrefs.SetInt("TrailsArray0", 1);
        //PlayerPrefs.SetInt("CharactersArray0", 1);
        //PlayerPrefs.SetInt("BGMsArray0", 1);

        //for (int i = 0; i < bgms.Length; i++)
        //{
        //    PlayerPrefs.SetInt("BGMsArray" + i, 1);
        //}

        trailsArray[0] = 1;
        charactersArray[0] = 1;
        charactersArray[1] = 1;
        bgmsArray[0] = 1;
        bgmsArray[1] = 1;

        //for (int i = 1; i < 21; i++)
        //{
        //    PlayerPrefs.SetInt("TrailsArray" + i, 0);
        //    PlayerPrefs.SetInt("CharactersArray" + i, 0);
        //    trailsArray[i] = PlayerPrefs.GetInt("TrailsArray" + i);
        //    charactersArray[i] = PlayerPrefs.GetInt("CharactersArray" + i);
        //}

        for (int i = 0; i < 21; i++)
        {
            //bgmsArray[i] = PlayerPrefs.GetInt("BGMsArray" + i);

            if (trailsArray[i] == 1) availableTrails.Add(i);
            if (charactersArray[i] == 1) availableCharacters.Add(i);
            if (bgmsArray[i] == 1) availableBGMs.Add(i);
        }

        masterVolumeSlider.value = masterVolume;
        soundEffectVolumeSlider.value = soundEffectVolume;
        bgmVolumeSlider.value = bgmVolume;
        masterVolumeSlider.onValueChanged = am.setMasterVolume;
        soundEffectVolumeSlider.onValueChanged = am.setSoundEffectVolume;
        bgmVolumeSlider.onValueChanged = am.setBGMVolume;
        SaveLoad.saveload.MainMenuSave();

        SaveLoad.saveload.SoundLoad();
        am.SetMasterVolume();
        am.SetSoundEffectVolume();
        am.SetBGMVolume();

        AudioManager.PlayBGM(bgms[curBGMIndex].bgm);
        Debug.Log(curBGMIndex);
    }

    public void Shop ()
    {
        AudioManager.PlaySound("touch");
        if (!shop)
        {
            howTo = false;
            shop = true;
            customization = false;
            sound = false;
            if (curTrail != null) Destroy(curTrail);
            mainMenu.SetActive(false);
            shopObj.SetActive(true);
            howToObj.SetActive(false);
            cusObj.SetActive(false);
            soundObj.SetActive(false);
            adTokenButton.SetActive(false);
            adCoinButton.SetActive(true);
            howToButton.color = new Color32 (200, 200, 200, 128);
            shopButton.color = new Color32(255, 255, 255, 255);
            customizationButton.color = new Color32 (200, 200, 200, 128);
            soundButton.color = new Color32(200, 200, 200, 128);
            gameStartButton.color = new Color32(200, 200, 200, 128);
            if (prizeTrailObject != null)
            {
                Destroy(prizeTrailObject);
                prizeTrailObject = null;
            }
            if (prizeCoinObject != null)
            {
                Destroy(prizeCoinObject);
                prizeCoinObject = null;
            }
            prizeImage.gameObject.SetActive(false);
            prizeName.text = ls.language.whatsPrize;
        }
        else
        {
            shop = false;
            prizeObject.gameObject.SetActive(false);
            mainMenu.SetActive(true);
            shopObj.SetActive(false);
            howToObj.SetActive(false);
            cusObj.SetActive(false);
            soundObj.SetActive(false);
            adTokenButton.SetActive(true);
            adCoinButton.SetActive(false);
            howToButton.color = new Color32 (255, 255, 255, 255);
            customizationButton.color = new Color32 (255, 255, 255, 255);
            soundButton.color = new Color32(255, 255, 255, 255);
            gameStartButton.color = new Color32(255, 255, 255, 255);
        }
    }

    public void HowTo ()
    {
        AudioManager.PlaySound("touch");
        if (!howTo)
        {
            howTo = true;
            shop = false;
            customization = false;
            sound = false;
            if (curTrail != null) Destroy(curTrail);
            prizeObject.gameObject.SetActive(false);
            mainMenu.SetActive(false);
            howToObj.SetActive(true);
            shopObj.SetActive(false);
            cusObj.SetActive(false);
            soundObj.SetActive(false);
            adTokenButton.SetActive(false);
            adCoinButton.SetActive(false);
            howToButton.color = new Color32(255, 255, 255, 255);
            shopButton.color = new Color32 (200, 200, 200, 128);
            customizationButton.color = new Color32 (200, 200, 200, 128);
            soundButton.color = new Color32(200, 200, 200, 128);
            gameStartButton.color = new Color32(200, 200, 200, 128);
        }
        else
        {
            howTo = false;
            mainMenu.SetActive(true);
            howToObj.SetActive(false);
            shopObj.SetActive(false);
            cusObj.SetActive(false);
            soundObj.SetActive(false);
            adTokenButton.SetActive(true);
            adCoinButton.SetActive(false);
            shopButton.color = new Color32 (255, 255, 255, 255);
            customizationButton.color = new Color32(255, 255, 255, 255);
            soundButton.color = new Color32(255, 255, 255, 255);
            gameStartButton.color = new Color32 (255, 255, 255, 255);
        }
    }

    public void GameStart()
    {
        AudioManager.PlaySound("touch");
        SaveLoad.saveload.mainMenu = null;
        PlayerPrefs.SetInt("CurTrailIndex", curTrailIndex);
        PlayerPrefs.SetInt("CurCharacterIndex", curCharacterIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Customization()
    {
        AudioManager.PlaySound("touch");
        if (!customization)
        {
            howTo = false;
            shop = false;
            customization = true;
            sound = false;
            prizeObject.gameObject.SetActive(false);
            SaveLoad.saveload.MainMenuLoad();
            for (int i = 0; i < availableTrails.Count; i++)
            {
                if (availableTrails[i] == curTrailIndex)
                {
                    trailIndex = i;
                    break;
                }
            }
            for (int i = 0; i < availableCharacters.Count; i++)
            {
                if (availableCharacters[i] == curCharacterIndex)
                {
                    characterIndex = i;
                    break;
                }
            }
            trailName.text = trails[availableTrails[trailIndex]].name;
            curTrail = Instantiate(trails[availableTrails[trailIndex]].trail, show.position, Quaternion.Euler(0, 0, 0), show);
            curSprite.sprite = characters[availableCharacters[characterIndex]].sprite;
            characterName.text = characters[availableCharacters[characterIndex]].name;
            //characterImage = characters[characterIndex].image;
            //bgmImage = bgms[bgmIndex].image;

            trailBackButton.color = new Color32(255, 255, 255, 255);
            trailNextButton.color = new Color32(255, 255, 255, 255);
            characterBackButton.color = new Color32(255, 255, 255, 255);
            characterNextButton.color = new Color32(255, 255, 255, 255);

            if (trailIndex == 0) trailBackButton.color = new Color32(200, 200, 200, 128);
            if (trailIndex == availableTrails.Count - 1) trailNextButton.color = new Color32(200, 200, 200, 128);
            if (characterIndex == 0) characterBackButton.color = new Color32(200, 200, 200, 128);
            if (characterIndex == availableCharacters.Count - 1) characterNextButton.color = new Color32(200, 200, 200, 128);
            mainMenu.SetActive(false);
            howToObj.SetActive(false);
            shopObj.SetActive(false);
            cusObj.SetActive(true);
            soundObj.SetActive(false);
            adTokenButton.SetActive(false);
            adCoinButton.SetActive(false);
            howToButton.color = new Color32 (200, 200, 200, 128);
            shopButton.color = new Color32(200, 200, 200, 128);
            customizationButton.color = new Color32(255, 255, 255, 255);
            soundButton.color = new Color32(200, 200, 200, 128);
            gameStartButton.color = new Color32 (200, 200, 200, 128);
        }
        else
        {
            Destroy(curTrail);
            customization = false;
            mainMenu.SetActive(true);
            howToObj.SetActive(false);
            shopObj.SetActive(false);
            cusObj.SetActive(false);
            soundObj.SetActive(false);
            adTokenButton.SetActive(true);
            adCoinButton.SetActive(false);
            howToButton.color = new Color32(255, 255, 255, 255);
            shopButton.color = new Color32 (255, 255, 255, 255);
            soundButton.color = new Color32(255, 255, 255, 255);
            gameStartButton.color = new Color32 (255, 255, 255, 255);
        }
    }

    public void Sound()
    {
        AudioManager.PlaySound("touch");
        if (!sound)
        {
            howTo = false;
            shop = false;
            customization = false;
            sound = true;
            SaveLoad.saveload.SoundLoad();
            if (curTrail != null) Destroy(curTrail);
            if (curBGMIndex == 0) bgmBackButton.color = new Color32(200, 200, 200, 128);
            else if (curBGMIndex == availableBGMs.Count - 1) bgmNextButton.color = new Color32(200, 200, 200, 128);
            prizeObject.gameObject.SetActive(false);
            mainMenu.SetActive(false);
            shopObj.SetActive(false);
            howToObj.SetActive(false);
            cusObj.SetActive(false);
            soundObj.SetActive(true);
            adTokenButton.SetActive(false);
            adCoinButton.SetActive(false);
            //bgmIndex = PlayerPrefs.GetInt("CurBGMIndex");
            for (int i = 0; i < availableBGMs.Count; i++)
            {
                if (availableBGMs[i] == curBGMIndex)
                {
                    bgmIndex = i;
                    break;
                }
            }
            bgmName.text = bgms[availableBGMs[bgmIndex]].name;
            AudioManager.PlayBGM(bgms[availableBGMs[bgmIndex]].bgm);
            howToButton.color = new Color32(200, 200, 200, 128);
            shopButton.color = new Color32(200, 200, 200, 128);
            customizationButton.color = new Color32(200, 200, 200, 128);
            soundButton.color = new Color32(255, 255, 255, 255);
            gameStartButton.color = new Color32(200, 200, 200, 128);
            masterVolumeSlider.value = masterVolume;
            soundEffectVolumeSlider.value = soundEffectVolume;
            bgmVolumeSlider.value = bgmVolume;
        }
        else
        {
            sound = false;
            mainMenu.SetActive(true);
            shopObj.SetActive(false);
            howToObj.SetActive(false);
            cusObj.SetActive(false);
            soundObj.SetActive(false);
            adTokenButton.SetActive(true);
            adCoinButton.SetActive(false);
            howToButton.color = new Color32(255, 255, 255, 255);
            shopButton.color = new Color32(255, 255, 255, 255);
            customizationButton.color = new Color32(255, 255, 255, 255);
            gameStartButton.color = new Color32(255, 255, 255, 255);
        }
    }

    public void Credit()
    {
        if (!credit)
        {
            credit = true;
            creditObj.SetActive(true);
        }
        else
        {
            credit = false;
            creditObj.SetActive(false);
        }
    }

    public void Buy ()
    {
        AudioManager.PlaySound("touch");
        if (coin >= 1000)
        {
            List<int> buyableTrails = new List<int>();
            List<int> buyableCharacters = new List<int>();
            List<int> buyableBGMs = new List<int>();
            for (int i = 0; i < trails.Length; i++)
            {
                if (trailsArray[i] == 0) buyableTrails.Add(i);
            }
            for (int i = 0; i < characters.Length; i++)
            {
                if (charactersArray[i] == 0) buyableCharacters.Add(i);
            }
            //for (int i = 0; i < bgms.Length; i++)
            //{
            //    if (bgmsArray[i] == 0) buyableBGMs.Add(i);
            //}
            if (buyableTrails.Count == 0 && buyableCharacters.Count == 0 && buyableBGMs.Count == 0)
            {
                Debug.LogError("There's no buyable items.");
            }
            else
            {
                int r;
                int index;
                if (buyableTrails.Count != 0 && buyableCharacters.Count == 0 && buyableBGMs.Count == 0)
                {
                    r = 0;
                }
                else if (buyableTrails.Count == 0 && buyableCharacters.Count != 0 && buyableBGMs.Count == 0)
                {
                    r = 1;
                }
                else if (buyableTrails.Count == 0 && buyableCharacters.Count == 0 && buyableBGMs.Count != 0)
                {
                    r = 2;
                }
                else
                {
                    r = Random.Range(0, 2);
                }
                SaveLoad.saveload.MainMenuLoad();
                if (r == 0)
                {
                    prizeObject.gameObject.SetActive(true);
                    prizeImage.gameObject.SetActive(false);
                    Destroy(prizeTrailObject);
                    Destroy(prizeCoinObject);
                    index = Random.Range(0, buyableTrails.Count);
                    trailsArray[buyableTrails[index]] = 1;
                    //PlayerPrefs.SetInt("TrailsArray" + buyableTrails[index], 1);
                    GameObject prizeTrail = Instantiate(trails[buyableTrails[index]].trail, prizeObject.transform.position, Quaternion.Euler(0, 0, 0), prizeObject.transform);
                    prizeTrailObject = prizeTrail;
                    prizeName.text = trails[buyableTrails[index]].name;
                    curTrailIndex = buyableTrails[index];
                    //PlayerPrefs.SetInt("CurTrailIndex", buyableTrails[index]);
                }
                else if (r == 1)
                {
                    prizeObject.gameObject.SetActive(false);
                    prizeImage.gameObject.SetActive(true);
                    Destroy(prizeTrailObject);
                    Destroy(prizeCoinObject);
                    index = Random.Range(0, buyableCharacters.Count);
                    charactersArray[buyableCharacters[index]] = 1;
                    //PlayerPrefs.SetInt("CharactersArray" + buyableCharacters[index], 1);
                    prizeImage.sprite = characters[buyableCharacters[index]].sprite;
                    prizeName.text = characters[buyableCharacters[index]].name;
                    curCharacterIndex = buyableCharacters[index];
                    //PlayerPrefs.SetInt("CurCharacterIndex", buyableCharacters[index]);
                }
                //else if (r == 2)
                //{
                //    index = Random.Range(0, buyableBGMs.Count);
                //    PlayerPrefs.SetInt("BGMsArray" + buyableBGMs[index], 1);
                //    prizeName.text = bgms[buyableBGMs[index]].name;
                //    PlayerPrefs.SetInt("CurBGMIndex", buyableBGMs[index]);
                //    AudioManager.PlayBGM(bgms[buyableBGMs[index]].bgm);
                //}
                coin -= 1000;
                coinText.text = coin.ToString();
                availableTrails.Clear();
                availableCharacters.Clear();
                availableBGMs.Clear();

                for (int i = 0; i < trails.Length; i++)
                {
                    //trailsArray[i] = PlayerPrefs.GetInt("TrailsArray" + i);
                    if (trailsArray[i] == 1) availableTrails.Add(i);
                }
                for (int i = 0; i < characters.Length; i++)
                {
                    //charactersArray[i] = PlayerPrefs.GetInt("CharactersArray" + i);
                    if (charactersArray[i] == 1) availableCharacters.Add(i);
                }
                for (int i = 0; i < bgms.Length; i++)
                {
                    //bgmsArray[i] = PlayerPrefs.GetInt("BGMsArray" + i);
                    if (bgmsArray[i] == 1) availableBGMs.Add(i);
                }
                for (int i = 0; i < availableTrails.Count; i++)
                {
                    if (availableTrails[i] == curTrailIndex)
                    {
                        trailIndex = i;
                        break;
                    }
                }
                for (int i = 0; i < availableCharacters.Count; i++)
                {
                    if (availableCharacters[i] == curCharacterIndex)
                    {
                        characterIndex = i;
                        break;
                    }
                }
                Debug.Log("Bought");
                SaveLoad.saveload.MainMenuSave();
            }
        }
        else
        {
            Debug.LogError("Not enough coins");
        }
    }

    public void AdCoinUI(int c)
    {
        prizeObject.gameObject.SetActive(false);
        prizeImage.gameObject.SetActive(false);
        Destroy(prizeTrailObject);
        GameObject coinObj = Instantiate(coinObject, prizeImage.transform.position, prizeImage.transform.rotation, shopTransform);
        coinObj.transform.localScale *= 750;
        prizeCoinObject = coinObj;
        prizeName.text = "+" + c.ToString() + " Coins";
        coinText.text = coin.ToString();
    }

    public void HowToNext ()
    {
        if (howToIndex < scenes.Length - 1)
        {
            AudioManager.PlaySound("touch");
            if (howToIndex == 0) backButton.SetActive(true);
            scenes[howToIndex++].SetActive(false);
            scenes[howToIndex].SetActive(true);
            if (howToIndex == scenes.Length - 1) nextButton.SetActive(false);
        }
    }

    public void HowToBack ()
    {
        if (howToIndex > 0)
        {
            AudioManager.PlaySound("touch");
            if (howToIndex == scenes.Length - 1) nextButton.SetActive(true);
            scenes[howToIndex--].SetActive(false);
            scenes[howToIndex].SetActive(true);
            if (howToIndex == 0) backButton.SetActive(false);
        }
    }

    public void TrailNext()
    {
        if (trailIndex < availableTrails.Count - 1)
        {
            AudioManager.PlaySound("touch");
            trailName.text = trails[availableTrails[++trailIndex]].name;
            Destroy(curTrail);
            curTrail = Instantiate(trails[availableTrails[trailIndex]].trail, show.position, Quaternion.Euler(0, 0, 0), show);
            curTrailIndex = availableTrails[trailIndex];
            //PlayerPrefs.SetInt("CurTrailIndex", availableTrails[trailIndex]);
            if (trailIndex == 0)
            {
                trailBackButton.color = new Color32(200, 200, 200, 128);
                trailNextButton.color = new Color32(255, 255, 255, 255);
            }
            else if (trailIndex == availableTrails.Count - 1)
            {
                trailBackButton.color = new Color32(255, 255, 255, 255);
                trailNextButton.color = new Color32(200, 200, 200, 128);
            }
            else
            {
                trailBackButton.color = new Color32(255, 255, 255, 255);
                trailNextButton.color = new Color32(255, 255, 255, 255);
            }
            SaveLoad.saveload.MainMenuSave();
        }
    }

    public void TrailBack()
    {
        if (trailIndex > 0)
        {
            AudioManager.PlaySound("touch");
            trailName.text = trails[availableTrails[--trailIndex]].name;
            Destroy(curTrail);
            curTrail = Instantiate(trails[availableTrails[trailIndex]].trail, show.position, Quaternion.Euler(0, 0, 0), show);
            curTrailIndex = availableTrails[trailIndex];
            //PlayerPrefs.SetInt("CurTrailIndex", availableTrails[trailIndex]);
            if (trailIndex == 0)
            {
                trailBackButton.color = new Color32(200, 200, 200, 128);
                trailNextButton.color = new Color32(255, 255, 255, 255);
            }
            else if (trailIndex == availableTrails.Count - 1)
            {
                trailBackButton.color = new Color32(255, 255, 255, 255);
                trailNextButton.color = new Color32(200, 200, 200, 128);
            }
            else
            {
                trailBackButton.color = new Color32(255, 255, 255, 255);
                trailNextButton.color = new Color32(255, 255, 255, 255);
            }
            SaveLoad.saveload.MainMenuSave();
        }
    }

    public void CharacterNext()
    {
        if (characterIndex < availableCharacters.Count - 1)
        {
            AudioManager.PlaySound("touch");
            characterName.text = characters[availableCharacters[++characterIndex]].name;
            curSprite.sprite = characters[availableCharacters[characterIndex]].sprite;
            curCharacterIndex = availableCharacters[characterIndex];
            //PlayerPrefs.SetInt("CurCharacterIndex", availableCharacters[characterIndex]);
            if (characterIndex == 0)
            {
                characterBackButton.color = new Color32(200, 200, 200, 128);
                characterNextButton.color = new Color32(255, 255, 255, 255);
            }
            else if (characterIndex == availableCharacters.Count - 1)
            {
                characterBackButton.color = new Color32(255, 255, 255, 255);
                characterNextButton.color = new Color32(200, 200, 200, 128);
            }
            else
            {
                characterBackButton.color = new Color32(255, 255, 255, 255);
                characterNextButton.color = new Color32(255, 255, 255, 255);
            }
            SaveLoad.saveload.MainMenuSave();
        }
    }

    public void CharacterBack()
    {
        if (characterIndex > 0)
        {
            AudioManager.PlaySound("touch");
            characterName.text = characters[availableCharacters[--characterIndex]].name;
            curSprite.sprite = characters[availableCharacters[characterIndex]].sprite;
            curCharacterIndex = availableCharacters[characterIndex];
            //PlayerPrefs.SetInt("CurCharacterIndex", availableCharacters[characterIndex]);
            if (characterIndex == 0)
            {
                characterBackButton.color = new Color32(200, 200, 200, 128);
                characterNextButton.color = new Color32(255, 255, 255, 255);
            }
            else if (characterIndex == availableCharacters.Count - 1)
            {
                characterBackButton.color = new Color32(255, 255, 255, 255);
                characterNextButton.color = new Color32(200, 200, 200, 128);
            }
            else
            {
                characterBackButton.color = new Color32(255, 255, 255, 255);
                characterNextButton.color = new Color32(255, 255, 255, 255);
            }
            SaveLoad.saveload.MainMenuSave();
        }
    }

    public void BGMNext()
    {
        if (bgmIndex < availableBGMs.Count - 1)
        {
            AudioManager.PlaySound("touch");
            bgmName.text = bgms[availableBGMs[++bgmIndex]].name;
            curBGMIndex = availableBGMs[bgmIndex];
            //PlayerPrefs.SetInt("CurBGMIndex", availableBGMs[bgmIndex]);
            AudioManager.PlayBGM(bgms[availableBGMs[bgmIndex]].bgm);
            if (bgmIndex == 0)
            {
                bgmBackButton.color = new Color32(200, 200, 200, 128);
                bgmNextButton.color = new Color32(255, 255, 255, 255);
            }
            else if (bgmIndex == availableBGMs.Count - 1)
            {
                bgmBackButton.color = new Color32(255, 255, 255, 255);
                bgmNextButton.color = new Color32(200, 200, 200, 128);
            }
            else
            {
                bgmBackButton.color = new Color32(255, 255, 255, 255);
                bgmNextButton.color = new Color32(255, 255, 255, 255);
            }
            SaveLoad.saveload.MainMenuSave();
        }
    }

    public void BGMBack()
    {
        if (bgmIndex > 0)
        {
            AudioManager.PlaySound("touch");
            bgmName.text = bgms[availableBGMs[--bgmIndex]].name;
            curBGMIndex = availableBGMs[bgmIndex];
            //PlayerPrefs.SetInt("CurBGMIndex", availableBGMs[bgmIndex]);
            AudioManager.PlayBGM(bgms[availableBGMs[bgmIndex]].bgm);
            if (bgmIndex == 0)
            {
                bgmBackButton.color = new Color32(200, 200, 200, 128);
                bgmNextButton.color = new Color32(255, 255, 255, 255);
            }
            else if (bgmIndex == availableBGMs.Count - 1)
            {
                bgmBackButton.color = new Color32(255, 255, 255, 255);
                bgmNextButton.color = new Color32(200, 200, 200, 128);
            }
            else
            {
                bgmBackButton.color = new Color32(255, 255, 255, 255);
                bgmNextButton.color = new Color32(255, 255, 255, 255);
            }
            SaveLoad.saveload.MainMenuSave();
        }
    }

    public void PopUpUIActivate()
    {
        if (!popUp)
        {
            popUp = true;
            popUpUI.SetActive(true);
        }
        else
        {
            popUp = false;
            popUpUI.SetActive(false);
        }
    }

    public void QuitChecnk()
    {
        if (!quitCheck)
        {
            quitCheck = true;
            mainMenu.SetActive(false);
            shopObj.SetActive(false);
            howToObj.SetActive(false);
            cusObj.SetActive(false);
            soundObj.SetActive(false);
            adTokenButton.SetActive(false);
            adCoinButton.SetActive(false);
            quitChenkUI.SetActive(true);
        }
        else
        {
            quitCheck = false;
            if (shop)
            {
                shop = false;
                Shop();
            }
            else if (howTo)
            {
                howTo = false;
                HowTo();
            }
            else if (customization)
            {
                customization = false;
                Customization();
            }
            else if (sound)
            {
                sound = false;
                Sound();
            }
            else if (mainMenu)
            {
                mainMenu.SetActive(true);
                adTokenButton.SetActive(true);
            }
            quitChenkUI.SetActive(false);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenuLanguageSet()
    {
        startText.text = ls.language.start;
        howToText.text = ls.language.howTo;
        shopText.text = ls.language.shop;
        customizationText.text = ls.language.customization;
        quitCheckText.text = ls.language.quitCheck;
        getTokenText.text = ls.language.getToken;
        getCoinText.text = ls.language.getCoin;

        prizeName.text = ls.language.whatsPrize;
        coinButtonText.text = "1000 " + ls.language.coin;
        for (int i = 0; i < tutText.Length; i++) tutText[i].text = ls.language.tut[i];
        trailText.text = ls.language.trail;
        skinText.text = ls.language.skin;
        soundText.text = ls.language.sound;
        masterText.text = ls.language.master;
        soundEffectText.text = ls.language.soundEffect;
        bgmText.text = ls.language.bgm;

        congratulationText.text = ls.language.congratulation;

        yesText.text = ls.language.yes;
        noText.text = ls.language.no;
    }

    public void SetEnglish()
    {
        LanguageSet.ls.LanguageTest(0);
    }

    public void SetKorean()
    {
        LanguageSet.ls.LanguageTest(1);
    }

    public void SetJapanese()
    {
        LanguageSet.ls.LanguageTest(2);
    }
}
