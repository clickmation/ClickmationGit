using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    AudioManager am;

    [Space]

    [Header("UI")]

    public Text highScoreText;
    public Text coinText;
    public Text adTokenText;

    [Space]

    [Header("SaveLoad")]

    public int highScore;
    public int coin;
    public int adToken;
    public int curTrailIndex;
    public int curCharacterIndex;
    public int curBGMIndex;
    public int[] trailsArray = new int[20];
    public int[] charactersArray = new int[20];
    public int[] bgmsArray = new int[20];
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
    [Space]

    [Header("Panels")]

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject shopObj;
    [SerializeField] GameObject howToObj;
    [SerializeField] GameObject cusObj;
    [SerializeField] GameObject soundObj;

    [Space]

    [Header("Buttons")]

    [SerializeField] Image shopButton;
    [SerializeField] Image howToButton;
    [SerializeField] Image gameStartButton;
    [SerializeField] Image customizationButton;
    [SerializeField] Image soundButton;

    [Space]

    [Header("Shop")]

    //public int shopIndex;
    public Text prizeName;
    public SpriteRenderer prizeObject;
    public GameObject prizeTrailObject;

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
    public Character[] characters;
    [System.Serializable]
    public struct Character
    {
        public string name;
        public Sprite sprite;
    }

    public int bgmIndex;
    public Image bgmImage;
    public Text bgmName;
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

    //[Space]

    //[Header("Sound")]

    //public 

    // Start is called before the first frame update
    void Start()
    {
        SaveLoad.saveload.mainMenu = this;
        am = AudioManager.audioManager;
        am.mainMenu = this;
        am.SetAudioSources();
        SaveLoad.saveload.Load();
        if (PlayerPrefs.GetInt("HighScore") != 0) highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = highScore.ToString();
        PlayerPrefs.SetInt("HighScore", 0);
        coin += PlayerPrefs.GetInt("Coin");
        PlayerPrefs.SetInt("Coin", 0);
        coinText.text = coin.ToString();
        if (PlayerPrefs.GetInt("AdToken") != 11) adToken = PlayerPrefs.GetInt("AdToken");
        adTokenText.text = adToken.ToString();
        PlayerPrefs.SetInt("AdToken", 11);

        //PlayerPrefs.SetInt("CurTrailIndex", 0);
        //PlayerPrefs.SetInt("CurCharacterIndex", 0);
        //PlayerPrefs.SetInt("CurBGMIndex", 0);

        curTrailIndex = PlayerPrefs.GetInt("CurTrailIndex");
        curCharacterIndex = PlayerPrefs.GetInt("CurCharacterIndex");
        curBGMIndex = PlayerPrefs.GetInt("CurBGMIndex");
        PlayerPrefs.SetInt("TrailsArray0", 1);
        PlayerPrefs.SetInt("CharactersArray0", 1);
        PlayerPrefs.SetInt("BGMsArray0", 1);


        //for (int i = 1; i < 20; i++)
        //{
        //    PlayerPrefs.SetInt("TrailsArray" + i, 0);
        //    PlayerPrefs.SetInt("CharactersArray" + i, 0);
        //    PlayerPrefs.SetInt("BGMsArray" + i, 0);
        //}
        //PlayerPrefs.SetInt("TrailsArray1", 0);
        //PlayerPrefs.SetInt("TrailsArray2", 0);
        //PlayerPrefs.SetInt("TrailsArray3", 0);
        //PlayerPrefs.SetInt("TrailsArray4", 0);
        //PlayerPrefs.SetInt("TrailsArray5", 0);
        //PlayerPrefs.SetInt("TrailsArray6", 0);
        //PlayerPrefs.SetInt("CharactersArray1", 0);
        //PlayerPrefs.SetInt("CharactersArray2", 0);
        //PlayerPrefs.SetInt("CharactersArray3", 0);
        //PlayerPrefs.SetInt("CharactersArray4", 0);
        //PlayerPrefs.SetInt("CharactersArray5", 0);
        //PlayerPrefs.SetInt("CharactersArray6", 0);
        //PlayerPrefs.SetInt("CharactersArray7", 0);

        for (int i = 0; i < 20; i++)
        {
            trailsArray[i] = PlayerPrefs.GetInt("TrailsArray" + i);
            if (trailsArray[i] == 1) availableTrails.Add(i);
        }
        for (int i = 0; i < 20; i++)
        {
            charactersArray[i] = PlayerPrefs.GetInt("CharactersArray" + i);
            if (charactersArray[i] == 1) availableCharacters.Add(i);
        }
        for (int i = 0; i < 20; i++)
        {
            bgmsArray[i] = PlayerPrefs.GetInt("BGMsArray" + i);
            if (bgmsArray[i] == 1) availableBGMs.Add(i);
        }

        masterVolumeSlider.onValueChanged = am.setMasterVolume;
        soundEffectVolumeSlider.onValueChanged = am.setSoundEffectVolume;
        bgmVolumeSlider.onValueChanged = am.setBGMVolume;
        masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        soundEffectVolume = PlayerPrefs.GetFloat("SoundEffectVolume");
        bgmVolume = PlayerPrefs.GetFloat("BGMVolume");
        SaveLoad.saveload.Save();
        masterVolumeSlider.value = masterVolume;
        soundEffectVolumeSlider.value = soundEffectVolume;
        bgmVolumeSlider.value = bgmVolume;
        am.SetMasterVolume();
        am.SetSoundEffectVolume();
        am.SetBGMVolume();
        AudioManager.PlayBGM(bgms[curBGMIndex].bgm);
    }

    public void Shop ()
    {
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
            howToButton.color = new Color32 (255, 255, 255, 255);
            customizationButton.color = new Color32 (255, 255, 255, 255);
            soundButton.color = new Color32(255, 255, 255, 255);
            gameStartButton.color = new Color32(255, 255, 255, 255);
        }
    }

    public void HowTo ()
    {
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
            shopButton.color = new Color32 (255, 255, 255, 255);
            customizationButton.color = new Color32(255, 255, 255, 255);
            soundButton.color = new Color32(255, 255, 255, 255);
            gameStartButton.color = new Color32 (255, 255, 255, 255);
        }
    }

    public void GameStart()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetInt("AdToken", adToken);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Customization()
    {
        if (!customization)
        {
            howTo = false;
            shop = false;
            customization = true;
            sound = false;
            prizeObject.gameObject.SetActive(false);
            trailIndex = PlayerPrefs.GetInt("CurTrailIndex");
            characterIndex = PlayerPrefs.GetInt("CurCharacterIndex");
            bgmIndex = PlayerPrefs.GetInt("CurBGMIndex");
            for (int i = 0; i < availableTrails.Count; i++)
            {
                if (availableTrails[i] == trailIndex)
                {
                    trailIndex = i;
                    break;
                }
            }
            for (int i = 0; i < availableCharacters.Count; i++)
            {
                if (availableCharacters[i] == characterIndex)
                {
                    characterIndex = i;
                    break;
                }
            }
            for (int i = 0; i < availableBGMs.Count; i++)
            {
                if (availableBGMs[i] == bgmIndex)
                {
                    bgmIndex = i;
                    break;
                }
            }
            SaveLoad.saveload.Save();
            trailName.text = trails[availableTrails[trailIndex]].name;
            curTrail = Instantiate(trails[availableTrails[trailIndex]].trail, show.position, Quaternion.Euler(0, 0, 0), show);
            curSprite.sprite = characters[availableCharacters[characterIndex]].sprite;
            characterName.text = characters[availableCharacters[characterIndex]].name;
            //characterImage = characters[characterIndex].image;
            bgmName.text = bgms[availableBGMs[bgmIndex]].name;
            //bgmImage = bgms[bgmIndex].image;
            AudioManager.PlayBGM(bgms[availableBGMs[bgmIndex]].bgm);
            mainMenu.SetActive(false);
            howToObj.SetActive(false);
            shopObj.SetActive(false);
            cusObj.SetActive(true);
            soundObj.SetActive(false);
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
            howToButton.color = new Color32(255, 255, 255, 255);
            shopButton.color = new Color32 (255, 255, 255, 255);
            soundButton.color = new Color32(255, 255, 255, 255);
            gameStartButton.color = new Color32 (255, 255, 255, 255);
        }
    }

    public void Sound()
    {
        if (!sound)
        {
            howTo = false;
            shop = true;
            customization = false;
            sound = true;
            if (curTrail != null) Destroy(curTrail);
            prizeObject.gameObject.SetActive(false);
            mainMenu.SetActive(false);
            shopObj.SetActive(false);
            howToObj.SetActive(false);
            cusObj.SetActive(false);
            soundObj.SetActive(true);
            howToButton.color = new Color32(200, 200, 200, 128);
            shopButton.color = new Color32(200, 200, 200, 128);
            customizationButton.color = new Color32(200, 200, 200, 128);
            soundButton.color = new Color32(255, 255, 255, 255);
            gameStartButton.color = new Color32(200, 200, 200, 128);
            masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            soundEffectVolume = PlayerPrefs.GetFloat("SoundEffectVolume");
            bgmVolume = PlayerPrefs.GetFloat("BGMVolume");
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
            howToButton.color = new Color32(255, 255, 255, 255);
            shopButton.color = new Color32(255, 255, 255, 255);
            customizationButton.color = new Color32(255, 255, 255, 255);
            gameStartButton.color = new Color32(255, 255, 255, 255);
        }
    }

    public void Buy ()
    {
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
            for (int i = 0; i < bgms.Length; i++)
            {
                if (bgmsArray[i] == 0) buyableBGMs.Add(i);
            }
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
                    r = Random.Range(0, 3);
                }
                SaveLoad.saveload.Load();
                prizeObject.gameObject.SetActive(true);
                if (r == 0)
                {
                    Destroy(prizeTrailObject);
                    index = Random.Range(0, buyableTrails.Count);
                    PlayerPrefs.SetInt("TrailsArray" + buyableTrails[index], 1);
                    GameObject prizeTrail = Instantiate(trails[buyableTrails[index]].trail, prizeObject.transform.position, Quaternion.Euler(0, 0, 0), prizeObject.transform);
                    prizeTrailObject = prizeTrail;
                    prizeName.text = trails[buyableTrails[index]].name;
                    PlayerPrefs.SetInt("CurTrailIndex", buyableTrails[index]);
                }
                else if (r == 1)
                {
                    index = Random.Range(0, buyableCharacters.Count);
                    PlayerPrefs.SetInt("CharactersArray" + buyableCharacters[index], 1);
                    prizeObject.sprite = characters[buyableCharacters[index]].sprite;
                    prizeName.text = characters[buyableCharacters[index]].name;
                    PlayerPrefs.SetInt("CurCharacterIndex", buyableCharacters[index]);
                }
                else if (r == 2)
                {
                    index = Random.Range(0, buyableBGMs.Count);
                    PlayerPrefs.SetInt("BGMsArray" + buyableBGMs[index], 1);
                    prizeName.text = bgms[buyableBGMs[index]].name;
                    PlayerPrefs.SetInt("CurBGMIndex", buyableBGMs[index]);
                    AudioManager.PlayBGM(bgms[buyableBGMs[index]].bgm);
                }
                coin -= 1000;
                coinText.text = coin.ToString();
                availableTrails.Clear();
                availableCharacters.Clear();
                availableBGMs.Clear();

                for (int i = 0; i < trails.Length; i++)
                {
                    trailsArray[i] = PlayerPrefs.GetInt("TrailsArray" + i);
                    if (trailsArray[i] == 1) availableTrails.Add(i);
                }
                for (int i = 0; i < characters.Length; i++)
                {
                    charactersArray[i] = PlayerPrefs.GetInt("CharactersArray" + i);
                    if (charactersArray[i] == 1) availableCharacters.Add(i);
                }
                for (int i = 0; i < bgms.Length; i++)
                {
                    bgmsArray[i] = PlayerPrefs.GetInt("BGMsArray" + i);
                    if (bgmsArray[i] == 1) availableBGMs.Add(i);
                }
                Debug.Log("Bought");
                SaveLoad.saveload.Save();
            }
        }
        else
        {
            Debug.LogError("Not enough coins");
        }
    }

    public void HowToNext ()
    {
        if (howToIndex < scenes.Length - 1)
        {
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
            trailName.text = trails[availableTrails[++trailIndex]].name;
            Destroy(curTrail);
            curTrail = Instantiate(trails[availableTrails[trailIndex]].trail, show.position, Quaternion.Euler(0, 0, 0), show);
            PlayerPrefs.SetInt("CurTrailIndex", availableTrails[trailIndex]);
        }
    }

    public void TrailBack()
    {
        if (trailIndex > 0)
        {
            trailName.text = trails[availableTrails[--trailIndex]].name;
            Destroy(curTrail);
            curTrail = Instantiate(trails[availableTrails[trailIndex]].trail, show.position, Quaternion.Euler(0, 0, 0), show);
            PlayerPrefs.SetInt("CurTrailIndex", availableTrails[trailIndex]);
        }
    }

    public void CharacterNext()
    {
        if (characterIndex < availableCharacters.Count - 1)
        {
            characterName.text = characters[availableCharacters[++characterIndex]].name;
            curSprite.sprite = characters[availableCharacters[characterIndex]].sprite;
            PlayerPrefs.SetInt("CurCharacterIndex", availableCharacters[characterIndex]);
        }
    }

    public void CharacterBack()
    {
        if (characterIndex > 0)
        {
            characterName.text = characters[availableCharacters[--characterIndex]].name;
            curSprite.sprite = characters[availableCharacters[characterIndex]].sprite;
            PlayerPrefs.SetInt("CurCharacterIndex", availableCharacters[characterIndex]);
        }
    }

    public void BGMNext()
    {
        if (bgmIndex < availableBGMs.Count - 1)
        {
            bgmName.text = bgms[availableBGMs[++bgmIndex]].name;
            AudioManager.PlayBGM(bgms[availableBGMs[bgmIndex]].bgm);
        }
    }

    public void BGMBack()
    {
        if (bgmIndex > 0)
        {
            bgmName.text = bgms[availableBGMs[--bgmIndex]].name;
            AudioManager.PlayBGM(bgms[availableBGMs[bgmIndex]].bgm);
        }
    }
}
