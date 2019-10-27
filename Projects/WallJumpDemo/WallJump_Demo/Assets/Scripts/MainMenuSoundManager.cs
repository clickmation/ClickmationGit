using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] neonSounds;
    [SerializeField] AudioClip[] sparkSounds;
    [SerializeField] AudioSource audioLoopSrc;
    [SerializeField] AudioSource audioOneShotSrc;
    AudioManager am;
    MainMenu mainMenu;
    int r;

    void Start ()
    {
        if (AudioManager.audioManager != null)
        {
            am = AudioManager.audioManager;
            mainMenu = am.mainMenu;
        }
        audioLoopSrc.volume = mainMenu.soundEffectVolume;
    }

    public void PlayNeonSound()
    {
        audioOneShotSrc.volume = mainMenu.soundEffectVolume;
        r = Random.Range(0, neonSounds.Length);
        audioOneShotSrc.PlayOneShot(neonSounds[r]);
    }

    public void PlaySparkSound()
    {
        audioOneShotSrc.volume = mainMenu.soundEffectVolume;
        r = Random.Range(0, sparkSounds.Length);
        audioOneShotSrc.PlayOneShot(sparkSounds[r]);
    }
}
