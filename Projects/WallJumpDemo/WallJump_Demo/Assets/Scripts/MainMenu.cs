using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [Space]

    [Header("UI")]

    public Text highScoreText;

    [Space]

    [Header("SaveLoad")]

    public int highScore;
    public int coin;
    public int curTrailIndex;
    public int curCharacterIndex;
    public int[] trailsArray = new int[20];
    public int[] charactersArray = new int[20];
    public int[] bgmsArray = new int[20];
    public List<int> availableTrails;
    public List<int> availableCharacters;
    public List<int> availableBGMs;

    [Space]

    [Header("Booleans")]

    [SerializeField] bool shop;
    [SerializeField] bool howTo;
    [SerializeField] bool customization;

    [Space]

    [Header("Panels")]

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject shopObj;
    [SerializeField] GameObject howToObj;
    [SerializeField] GameObject cusObj;

    [Space]

    [Header("Buttons")]

    [SerializeField] Button shopButton;
    [SerializeField] Button howToButton;
    [SerializeField] Button gameStartButton;
    [SerializeField] Button customizationButton;

    [Space]

    [Header("Shop")]

    //public int shopIndex;
    public Image prizeImage;
    public Text prizeName;
    public Prize[] prizes;

    [System.Serializable]
    public struct Prize
    {
        public string name;
        public Image image;
    }

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
        public Image image;
        public AudioClip bgm;
    }

    // Start is called before the first frame update
    void Start()
    {
        SaveLoad.saveload.Load();
        if (PlayerPrefs.GetInt("HighScore") != 0) highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = highScore.ToString();
        PlayerPrefs.SetInt("HighScore", 0);
        coin += PlayerPrefs.GetInt("Coin");
        PlayerPrefs.SetInt("Coin", 0);
        curTrailIndex = PlayerPrefs.GetInt("CurTrailIndex");
        curCharacterIndex = PlayerPrefs.GetInt("CurCharacterIndex");
        PlayerPrefs.SetInt("TrailsArray0", 1);
        PlayerPrefs.SetInt("CharactersArray0", 1);
        PlayerPrefs.SetInt("BGMsArray0", 1);

        PlayerPrefs.SetInt("TrailsArray1", 1);
        PlayerPrefs.SetInt("TrailsArray2", 1);
        PlayerPrefs.SetInt("CharactersArray1", 1);

        for (int i = 0; i < 20; i++)
        {
            trailsArray[i] = PlayerPrefs.GetInt("TrailsArray" + i);
            charactersArray[i] = PlayerPrefs.GetInt("CharactersArray" + i);
            bgmsArray[i] = PlayerPrefs.GetInt("BGMsArray" + i);
            if (trailsArray[i] == 1) availableTrails.Add(i);
            if (charactersArray[i] == 1) availableCharacters.Add(i);
            if (bgmsArray[i] == 1) availableBGMs.Add(i);
        }
        SaveLoad.saveload.Save();
    }

    public void Shop ()
    {
        if (!shop)
        {
            shop = true;
            mainMenu.SetActive(false);
            shopObj.SetActive(true);
            howToButton.interactable = false;
            gameStartButton.interactable = false;
            customizationButton.interactable = false;
        }
        else
        {
            shop = false;
            mainMenu.SetActive(true);
            shopObj.SetActive(false);
            howToButton.interactable = true;
            gameStartButton.interactable = true;
            customizationButton.interactable = true;
        }
    }

    public void HowTo ()
    {
        if (!howTo)
        {
            howTo = true;
            mainMenu.SetActive(false);
            howToObj.SetActive(true);
            shopButton.interactable = false;
            gameStartButton.interactable = false;
            customizationButton.interactable = false;
        }
        else
        {
            howTo = false;
            mainMenu.SetActive(true);
            howToObj.SetActive(false);
            shopButton.interactable = true;
            gameStartButton.interactable = true;
            customizationButton.interactable = true;
        }
    }

    public void GameStart()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Customization()
    {
        if (!customization)
        {
            customization = true;
            SaveLoad.saveload.Load();
            trailIndex = PlayerPrefs.GetInt("CurTrailIndex");
            characterIndex = PlayerPrefs.GetInt("CurCharacterIndex");
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
            //for (int i = 0; i < availableBGMs.Count; i++)
            //{
            //    if (availableBGMs[i] == bgmIndex)
            //    {
            //        bgmIndex = i;
            //        break;
            //    }
            //}
            SaveLoad.saveload.Save();
            trailName.text = trails[trailIndex].name;
            curTrail = Instantiate(trails[trailIndex].trail, show.position, Quaternion.Euler(0, 0, 0), show);
            curSprite.sprite = characters[characterIndex].sprite;
            characterName.text = characters[characterIndex].name;
            //characterImage = characters[characterIndex].image;
            //bgmName.text = bgms[bgmIndex].name;
            //bgmImage = bgms[bgmIndex].image;
            //bgmsetting;
            mainMenu.SetActive(false);
            cusObj.SetActive(true);
            shopButton.interactable = false;
            howToButton.interactable = false;
            gameStartButton.interactable = false;
        }
        else
        {
            Destroy(curTrail);
            customization = false;
            cusObj.SetActive(false);
            mainMenu.SetActive(true);
            shopButton.interactable = true;
            howToButton.interactable = true;
            gameStartButton.interactable = true;
        }
    }

    public void Buy ()
    {
        SaveLoad.saveload.Load();
        coin -= 1000;
        Debug.Log("Bought");
        SaveLoad.saveload.Save();
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
            characterName.text = characters[++characterIndex].name;
            curSprite.sprite = characters[characterIndex].sprite;
            PlayerPrefs.SetInt("CurCharacterIndex", characterIndex);
        }
    }

    public void CharacterBack()
    {
        if (characterIndex > 0)
        {
            characterName.text = characters[--characterIndex].name;
            curSprite.sprite = characters[characterIndex].sprite;
            PlayerPrefs.SetInt("CurCharacterIndex", characterIndex);
        }
    }

    public void BGMNext()
    {
        if (bgmIndex < bgms.Length - 1)
        {
            bgmName.text = bgms[++bgmIndex].name;
            //bgmImage = bgms[bgmIndex].image;
            //bgmsetting;
        }
    }

    public void BGMBack()
    {
        if (bgmIndex > 0)
        {
            bgmName.text = bgms[--bgmIndex].name;
            //bgmImage = bgms-bgmIndex].image;
            //bgmsetting;
        }
    }
}
