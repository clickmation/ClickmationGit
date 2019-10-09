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
    public Trail[] trails;
    [System.Serializable]
    public struct Trail
    {
        public string name;
        public Image image;
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
            shopObj.SetActive(true);
            howToButton.interactable = false;
            gameStartButton.interactable = false;
            customizationButton.interactable = false;
        }
        else
        {
            shop = false;
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
            howToObj.SetActive(true);
            shopButton.interactable = false;
            gameStartButton.interactable = false;
            customizationButton.interactable = false;
        }
        else
        {
            howTo = false;
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
            trailName.text = trails[trailIndex].name;
            //trailImage = trails[trailIndex].image;
            characterName.text = characters[characterIndex].name;
            //characterImage = characters[characterIndex].image;
            bgmName.text = bgms[bgmIndex].name;
            //bgmImage = bgms[bgmIndex].image;
            //bgmsetting;
            cusObj.SetActive(true);
            shopButton.interactable = false;
            howToButton.interactable = false;
            gameStartButton.interactable = false;
        }
        else
        {
            customization = false;
            cusObj.SetActive(false);
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
            //trailImage = trails[trailIndex].image;
        }
    }

    public void TrailBack()
    {
        if (trailIndex > 0)
        {
            trailName.text = trails[--trailIndex].name;
            //trailImage = trails[trailIndex].image;
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
