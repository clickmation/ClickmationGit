using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;
    public MainMenu mainMenu;
    public GameMaster gm;
	
	public static AudioClip groundJumpSound, landingSound, attackSound, killSound, deathSound, coinSound, jumpPanelSound, wallPanelSound;
    public static AudioSource soundEffectAudioSrc;
    public static AudioSource bgmAudioSrc;
    //[SerializeField] AudioSource SEASrc;
    //[SerializeField] AudioSource BGMSrc;
    public Slider.SliderEvent setMasterVolume;
    public Slider.SliderEvent setSoundEffectVolume;
    public Slider.SliderEvent setBGMVolume;
    float masterVolume;
    float soundEffectVolume;
    float bgmVolume;

    void Awake ()
    {
        if (audioManager == null) audioManager = this;
    }

    public void SetAudioSources ()
    {
        soundEffectAudioSrc = transform.GetChild(0).GetComponent<AudioSource>();
        bgmAudioSrc = transform.GetChild(1).GetComponent<AudioSource>();
    }
	
    void Start()
    {
        groundJumpSound = Resources.Load<AudioClip> ("Jump2");
		landingSound = Resources.Load<AudioClip> ("Land");
		attackSound = Resources.Load<AudioClip> ("Clash");
		killSound = Resources.Load<AudioClip> ("Impact1");
		deathSound = Resources.Load<AudioClip> ("Death");
		coinSound = Resources.Load<AudioClip> ("Coin4");
		jumpPanelSound = Resources.Load<AudioClip> ("Boing2");
		wallPanelSound = Resources.Load<AudioClip> ("Boing1");
    }

    public void SetMasterVolume ()
    {
        if (mainMenu != null) masterVolume = mainMenu.masterVolumeSlider.value;
        else masterVolume = gm.masterVolumeSlider.value;
        soundEffectAudioSrc.volume = masterVolume * soundEffectVolume;
        bgmAudioSrc.volume = masterVolume * bgmVolume;
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
    }

    public void SetSoundEffectVolume ()
    {
        if (mainMenu != null) soundEffectVolume = mainMenu.soundEffectVolumeSlider.value;
        else masterVolume = soundEffectVolume = gm.soundEffectVolumeSlider.value;
        soundEffectAudioSrc.volume = masterVolume * soundEffectVolume;
        PlayerPrefs.SetFloat("SoundEffectVolume", soundEffectVolume);
    }

    public void SetBGMVolume ()
    {
        if (mainMenu != null) masterVolume = bgmVolume = mainMenu.bgmVolumeSlider.value;
        else masterVolume = bgmVolume = gm.bgmVolumeSlider.value;
        bgmAudioSrc.volume = masterVolume * bgmVolume;
        PlayerPrefs.SetFloat("BGMVolume", bgmVolume);
    }
	
	public static void PlaySound (string clip)
	{
		switch(clip) {
			case "groundJump":
				soundEffectAudioSrc.PlayOneShot (groundJumpSound);
				break;
			case "landing":
				soundEffectAudioSrc.PlayOneShot (landingSound);
				break;
			case "attack":
				soundEffectAudioSrc.PlayOneShot (attackSound);
				break;
			case "kill":
				soundEffectAudioSrc.PlayOneShot (killSound);
				break;
			case "death":
				soundEffectAudioSrc.PlayOneShot (deathSound);
				break;
			case "coin":
				soundEffectAudioSrc.PlayOneShot (coinSound);
				break;
			case "jumpPanel":
				soundEffectAudioSrc.PlayOneShot (jumpPanelSound);
				break;
			case "wallPanel":
				soundEffectAudioSrc.PlayOneShot (wallPanelSound);
				break;
            default:
                Debug.LogError("AudioManager : no corresponding audio of name \"" + clip + "\"");
                break;
        }
		
	}
    
    public static void PlayBGM (AudioClip clip)
    {
        if (clip != bgmAudioSrc.clip)
        {
            bgmAudioSrc.clip = clip;
            bgmAudioSrc.Play();
            Debug.Log("Played");
        }
    }
}
