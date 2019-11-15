using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] neonSounds;
    [SerializeField] AudioClip[] sparkSounds;
    [SerializeField] AudioSource audioLoopSrc;
    [SerializeField] AudioSource audioOneShotSrc;
    [SerializeField] Animator anim;
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
        audioLoopSrc.volume = mainMenu.soundEffectVolume * mainMenu.masterVolume;
    }

    public void Dice ()
    {
        r = Random.Range(0, 100);
        anim.SetInteger("RanNum", r);
    }

    public void PlayNeonSound1()
    {
        audioOneShotSrc.volume = mainMenu.soundEffectVolume * mainMenu.masterVolume;
        audioOneShotSrc.PlayOneShot(neonSounds[0]);
    }
	public void PlayNeonSound2()
    {
        audioOneShotSrc.volume = mainMenu.soundEffectVolume * mainMenu.masterVolume;
        audioOneShotSrc.PlayOneShot(neonSounds[1]);
    }
	public void PlayNeonSound3()
    {
        audioOneShotSrc.volume = mainMenu.soundEffectVolume * mainMenu.masterVolume;
        audioOneShotSrc.PlayOneShot(neonSounds[2]);
    }



    public void PlaySparkSound1()
    {
        audioOneShotSrc.volume = mainMenu.soundEffectVolume * mainMenu.masterVolume;
        audioOneShotSrc.PlayOneShot(sparkSounds[0]);
    }
	public void PlaySparkSound2()
    {
        audioOneShotSrc.volume = mainMenu.soundEffectVolume * mainMenu.masterVolume;
        audioOneShotSrc.PlayOneShot(sparkSounds[1]);
    }
	public void PlaySparkSound3()
    {
        audioOneShotSrc.volume = mainMenu.soundEffectVolume * mainMenu.masterVolume;
        audioOneShotSrc.PlayOneShot(sparkSounds[2]);
    }
	public void PlaySparkSound4()
    {
        audioOneShotSrc.volume = mainMenu.soundEffectVolume * mainMenu.masterVolume;
        audioOneShotSrc.PlayOneShot(sparkSounds[3]);
    }
	public void PlaySparkSound4_1()
    {
        audioOneShotSrc.volume = mainMenu.soundEffectVolume * mainMenu.masterVolume;
        audioOneShotSrc.PlayOneShot(sparkSounds[4]);
    }
	public void PlaySparkSound4_2()
    {
        audioOneShotSrc.volume = mainMenu.soundEffectVolume * mainMenu.masterVolume;
        audioOneShotSrc.PlayOneShot(sparkSounds[5]);
    }
	public void PlaySparkSound5()
    {
        audioOneShotSrc.volume = mainMenu.soundEffectVolume * mainMenu.masterVolume;
        audioOneShotSrc.PlayOneShot(sparkSounds[6]);
    }
}
