using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

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
    public Image howToImage;
    public Text howToInfo;
    public HowToScene[] scenes;
    [System.Serializable]
    public struct HowToScene
    {
        public Image image;
        public string info;
    }

    [Space]

    [Header("Customization")]

    public int trailIndex;
    public Image trailImage;
    public Text trailName;
    public Transform show;
    public GameObject curTrail;
    public Trail[] trails;
    [System.Serializable]
    public struct Trail
    {
        public string name;
        public GameObject trail;
    }

    public int characterIndex;
    public Image characterImage;
    public Text characterName;
    public Character[] characters;
    [System.Serializable]
    public struct Character
    {
        public string name;
        public Image image;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Customization()
    {
        if (!customization)
        {
            customization = true;
            trailIndex = PlayerPrefs.GetInt("TrailIndex");
            trailName.text = trails[trailIndex].name;
            curTrail = Instantiate(trails[trailIndex].trail, show.position, Quaternion.Euler(0, 0, 0), show);
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
        Debug.Log("Bought");
    }

    public void HowToNext ()
    {
        if (howToIndex < scenes.Length - 1)
        {
            howToImage = scenes[++howToIndex].image;
            howToInfo.text = scenes[howToIndex].info;
        }
    }

    public void HowToBack ()
    {
        if (howToIndex > 0)
        {
            howToImage = scenes[--howToIndex].image;
            howToInfo.text = scenes[howToIndex].info;
        }
    }

    public void TrailNext()
    {
        if (trailIndex < trails.Length - 1)
        {
            trailName.text = trails[++trailIndex].name;
            Destroy(curTrail);
            curTrail = Instantiate(trails[trailIndex].trail, show.position, Quaternion.Euler(0, 0, 0), show);
            PlayerPrefs.SetInt("TrailIndex", trailIndex);
        }
    }

    public void TrailBack()
    {
        if (trailIndex > 0)
        {
            trailName.text = trails[--trailIndex].name;
            Destroy(curTrail);
            curTrail = Instantiate(trails[trailIndex].trail, show.position, Quaternion.Euler(0, 0, 0), show);
            PlayerPrefs.SetInt("TrailIndex", trailIndex);
        }
    }

    public void CharacterNext()
    {
        if (characterIndex < characters.Length - 1)
        {
            characterName.text = characters[++characterIndex].name;
            //characterImage = characters[characterIndex].image;
        }
    }

    public void CharacterBack()
    {
        if (characterIndex > 0)
        {
            characterName.text = characters[--characterIndex].name;
            //characterImage = characters[characterIndex].image;
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
