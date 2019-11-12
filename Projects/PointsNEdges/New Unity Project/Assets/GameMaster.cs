using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
	
	[SerializeField] private Transform player;
	public GameObject[] projectiles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void TurnRight ()
	{
		player.localScale = new Vector3(1.0f,1.0f,1.0f);
	}
	
	public void TurnLeft ()
	{
		player.localScale = new Vector3(-1.0f,1.0f,1.0f);
	}
}
