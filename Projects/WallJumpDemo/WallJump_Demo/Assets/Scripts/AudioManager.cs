using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	
	public static AudioClip groundJumpSound, landingSound, attackSound, killSound, deathSound, coinSound, jumpPanelSound, wallPanelSound;
	static AudioSource audioSrc;
	
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
		
		audioSrc = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public static void PlaySound (string clip)
	{
		switch(clip) {
			case "groundJump":
				audioSrc.PlayOneShot (groundJumpSound);
				break;
			case "landing":
				audioSrc.PlayOneShot (landingSound);
				break;
			case "attack":
				audioSrc.PlayOneShot (attackSound);
				break;
			case "kill":
				audioSrc.PlayOneShot (killSound);
				break;
			case "death":
				audioSrc.PlayOneShot (deathSound);
				break;
			case "coin":
				audioSrc.PlayOneShot (coinSound);
				break;
			case "jumpPanel":
				audioSrc.PlayOneShot (jumpPanelSound);
				break;
			case "wallPanel":
				audioSrc.PlayOneShot (wallPanelSound);
				break;
            default:
                Debug.LogError("AudioManager : no corresponding audio of name \"" + clip + "\"");
                break;
        }
		
	}
}
