using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
	public float speed = 5;
	
	private void Update ()
	{
		transform.Translate(Vector2.left * speed * Time.deltaTime);
	}
	
    private void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == this.tag)
		{
			Destroy(other.gameObject);
			Destroy(gameObject);
		} else {
			Destroy(other.gameObject);
			Destroy(gameObject);
			Debug.Log ("Beep!");
		}
	}
	
	
}
