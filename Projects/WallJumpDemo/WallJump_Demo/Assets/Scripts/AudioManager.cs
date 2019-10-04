using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField] public static AudioClip groundJumpSound;
	public static AudioClip landingSound, attackSound, killSound, deathSound, coinSound, jumpPanelSound, wallPanelSound;
	
	
	static AudioSource audioSrc;
	
    void Start()
    {
		audioSrc = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public static void PlaySound (string clip)
	{
		switch(clip){
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
		}
		
	}
}
