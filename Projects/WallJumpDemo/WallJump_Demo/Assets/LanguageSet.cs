using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSet : MonoBehaviour
{
    public static LanguageSet ls;

    public MainMenu mm;
    [SerializeField] LanguageForm[] languages;
    public LanguageForm language;
    private int languageInt;

    void Awake()
    {
        if (LanguageSet.ls == null) LanguageSet.ls = this;
        if (PlayerPrefs.GetInt("FirstLanguageSet") == 0)
        {
            switch (Application.systemLanguage)
            {
                //case SystemLanguage.English:
                //    language = languages[0];
                //    break;
                case SystemLanguage.Korean:
                    language = languages[1];
                    languageInt = 1;
                    break;
                case SystemLanguage.Japanese:
                    language = languages[2];
                    languageInt = 2;
                    break;
                default:
                    language = languages[0];
                    languageInt = 0;
                    break;
            }
            PlayerPrefs.SetInt("FirstLanguageSet", 1);
            PlayerPrefs.SetInt("Language", languageInt);
        }
        else language = languages[PlayerPrefs.GetInt("Language")];
    }

    public void LanguageTest ()
    {
        languageInt = PlayerPrefs.GetInt("Language");
        languageInt++;
        if (languageInt > 2) languageInt = 0;
        PlayerPrefs.SetInt("Language", languageInt);
        language = languages[PlayerPrefs.GetInt("Language")];
        mm.MainMenuLanguageSet();
    }
}
