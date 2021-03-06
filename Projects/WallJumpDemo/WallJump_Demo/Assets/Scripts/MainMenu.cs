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

    public Text tutCheckYesText;
    public Text tutCheckText;

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
    [SerializeField] bool adTokenPopUp;
    [SerializeField] bool coinPopUp;
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
    [SerializeField] GameObject adTokenPopUpUI;
    [SerializeField] GameObject coinPopUpUI;
    [SerializeField] GameObject quitChenkUI;
    [SerializeField] GameObject checkTutorial;

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
    [SerializeField] GameObject buyButton;

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

        trailsArray[0] = 1;
        trailsArray[12] = 1;
        charactersArray[0] = 1;
        charactersArray[1] = 1;
        bgmsArray[0] = 1;
        bgmsArray[1] = 1;

        for (int i = 0; i < 21; i++)
        {
            if (trailsArray[i] == 1) availableTrails.Add(i);
            if (charactersArray[i] == 1) availableCharacters.Add(i);
            if (bgmsArray[i] == 1) availableBGMs.Add(i);
        }

        masterVolumeSlider.onValueChanged = am.setMasterVolume;
        soundEffectVolumeSlider.onValueChanged = am.setSoundEffectVolume;
        bgmVolumeSlider.onValueChanged = am.setBGMVolume;
        masterVolumeSlider.value = masterVolume;
        soundEffectVolumeSlider.value = soundEffectVolume;
        bgmVolumeSlider.value = bgmVolume;
        SaveLoad.saveload.MainMenuSave();

        SaveLoad.saveload.SoundLoad();
        am.SetMasterVolume();
        am.SetSoundEffectVolume();
        am.SetBGMVolume();

        AudioManager.PlayBGM(bgms[curBGMIndex].bgm);
        ADManager.adManager.HideBanner();
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
            credit = false;
            if (curTrail != null) Destroy(curTrail);
            mainMenu.SetActive(false);
            shopObj.SetActive(true);
            howToObj.SetActive(false);
            cusObj.SetActive(false);
            soundObj.SetActive(false);
            adTokenButton.SetActive(false);
            adCoinButton.SetActive(true);
            creditObj.SetActive(false);
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
            if (PlayerPrefs.GetInt("NoBuyable") == 1)
            {
                buyButton.SetActive(false);
                prizeName.text = ls.language.noMoreItems;
            }
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
            creditObj.SetActive(false);
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
            credit = false;
            if (curTrail != null) Destroy(curTrail);
            prizeObject.gameObject.SetActive(false);
            PlayerPrefs.SetInt("TutChecked", 1);
            mainMenu.SetActive(false);
            howToObj.SetActive(true);
            shopObj.SetActive(false);
            cusObj.SetActive(false);
            soundObj.SetActive(false);
            adTokenButton.SetActive(false);
            adCoinButton.SetActive(false);
            creditObj.SetActive(false);
            checkTutorial.SetActive(false);
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
            creditObj.SetActive(false);
            shopButton.color = new Color32 (255, 255, 255, 255);
            customizationButton.color = new Color32(255, 255, 255, 255);
            soundButton.color = new Color32(255, 255, 255, 255);
            gameStartButton.color = new Color32 (255, 255, 255, 255);
        }
    }

    public void GameStart()
    {
        AudioManager.PlaySound("touch");
        if (PlayerPrefs.GetInt("TutChecked") == 1) StartGame();
        else
        {
            mainMenu.SetActive(true);
            howToObj.SetActive(false);
            shopObj.SetActive(false);
            cusObj.SetActive(false);
            soundObj.SetActive(false);
            adTokenButton.SetActive(true);
            adCoinButton.SetActive(false);
            creditObj.SetActive(false);
            checkTutorial.SetActive(true);
        }
    }

    public void StartGame()
    {
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
            credit = false;
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
            creditObj.SetActive(false);
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
            creditObj.SetActive(false);
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
            credit = false;
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
            creditObj.SetActive(false);
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
            creditObj.SetActive(false);
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
            howTo = false;
            shop = false;
            customization = false;
            sound = false;
            credit = true;
            creditObj.SetActive(true);
            mainMenu.SetActive(false);
            shopObj.SetActive(false);
            howToObj.SetActive(false);
            cusObj.SetActive(false);
            soundObj.SetActive(false);
            adTokenButton.SetActive(false);
            adCoinButton.SetActive(false);
            howToButton.color = new Color32(255, 255, 255, 255);
            shopButton.color = new Color32(255, 255, 255, 255);
            customizationButton.color = new Color32(255, 255, 255, 255);
            soundButton.color = new Color32(255, 255, 255, 255);
            gameStartButton.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            credit = false;
            creditObj.SetActive(false);
            mainMenu.SetActive(true);
            shopObj.SetActive(false);
            howToObj.SetActive(false);
            cusObj.SetActive(false);
            soundObj.SetActive(false);
            adTokenButton.SetActive(true);
            adCoinButton.SetActive(false);
        }
    }

    public void Buy ()
    {
        AudioManager.PlaySound("touch");
        if (coin >= 500)
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
            if (buyableTrails.Count == 0 && buyableCharacters.Count == 0 && buyableBGMs.Count == 0)
            {
                PlayerPrefs.SetInt("NoBuyable", 1);
                prizeName.text = ls.language.noMoreItems;
                buyButton.SetActive(false);
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
                    GameObject prizeTrail = Instantiate(trails[buyableTrails[index]].trail, prizeObject.transform.position, Quaternion.Euler(0, 0, 0), prizeObject.transform);
                    prizeTrailObject = prizeTrail;
                    prizeName.text = trails[buyableTrails[index]].name;
                    curTrailIndex = buyableTrails[index];
                }
                else if (r == 1)
                {
                    prizeObject.gameObject.SetActive(false);
                    prizeImage.gameObject.SetActive(true);
                    Destroy(prizeTrailObject);
                    Destroy(prizeCoinObject);
                    index = Random.Range(0, buyableCharacters.Count);
                    charactersArray[buyableCharacters[index]] = 1;
                    prizeImage.sprite = characters[buyableCharacters[index]].sprite;
                    prizeName.text = characters[buyableCharacters[index]].name;
                    curCharacterIndex = buyableCharacters[index];
                }
                //else if (r == 2)
                //{
                //    index = Random.Range(0, buyableBGMs.Count);
                //    PlayerPrefs.SetInt("BGMsArray" + buyableBGMs[index], 1);
                //    prizeName.text = bgms[buyableBGMs[index]].name;
                //    PlayerPrefs.SetInt("CurBGMIndex", buyableBGMs[index]);
                //    AudioManager.PlayBGM(bgms[buyableBGMs[index]].bgm);
                //}
                if (prizeCoinObject != null) Destroy(prizeCoinObject);
                coin -= 500;
                coinText.text = coin.ToString();
                availableTrails.Clear();
                availableCharacters.Clear();
                availableBGMs.Clear();

                for (int i = 0; i < trails.Length; i++)
                {
                    if (trailsArray[i] == 1) availableTrails.Add(i);
                }
                for (int i = 0; i < characters.Length; i++)
                {
                    if (charactersArray[i] == 1) availableCharacters.Add(i);
                }
                for (int i = 0; i < bgms.Length; i++)
                {
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
                for (int i = 0; i < trails.Length; i++)
                {
                    if (trailsArray[i] == 0) buyableTrails.Add(i);
                }
                for (int i = 0; i < characters.Length; i++)
                {
                    if (charactersArray[i] == 0) buyableCharacters.Add(i);
                }
                if (buyableTrails.Count == 0 && buyableCharacters.Count == 0)
                {
                    PlayerPrefs.SetInt("NoBuyable", 1);
                    prizeName.text = ls.language.noMoreItems;
                    buyButton.SetActive(false);
                }
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

    public void AdTokenPopUpUIActivate()
    {
        if (!adTokenPopUp)
        {
            adTokenPopUp = true;
            adTokenPopUpUI.SetActive(true);
        }
        else
        {
            adTokenPopUp = false;
            adTokenPopUpUI.SetActive(false);
        }
    }

    public void CoinPopUpUIActivate()
    {
        if (!coinPopUp)
        {
            coinPopUp = true;
            coinPopUpUI.SetActive(true);
        }
        else
        {
            coinPopUp = false;
            coinPopUpUI.SetActive(false);
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
            creditObj.SetActive(false);
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
            else if (credit)
            {
                credit = false;
                Credit();
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
        coinButtonText.text = "500 " + ls.language.coin;
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

        tutCheckYesText.text = ls.language.yes;
        tutCheckText.text = ls.language.tutCheck1 + "\n" + ls.language.tutCheck2;
    }

    public void GoToMainMenu1()
    {
        PlayerPrefs.SetInt("NoBuyable", 0);
        PlayerPrefs.SetInt("TutChecked", 0);
        PlayerPrefs.SetInt("FirstPlayed", 0); ;
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
