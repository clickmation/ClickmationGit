using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage;
	[SerializeField]
	public GameObject deathParticle;

    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.transform.GetComponent<Movement>() != null)
        {
            if (other.transform.position.y - this.transform.position.y <= 1.1f)
            {
                other.transform.GetComponent<Movement>().stamina -= damage;
            }
			GameObject clone;
			clone = Instantiate(deathParticle, this.transform.position, Quaternion.identity) as GameObject;
			Destroy(clone, 3f);
            Destroy(this.gameObject);
        }
    }
}
