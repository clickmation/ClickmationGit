using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
	public float speed = 5;
	
	private void Update ()
	{
		transform.Translate(Vector2.right * speed * Time.deltaTime);
	}
}
