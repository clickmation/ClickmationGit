using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Stamina : MonoBehaviour
{
    public float staminaAddAmount;

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.transform.GetComponent<Movement>().stamina += staminaAddAmount;
            AudioManager.PlaySound("coin");
            Destroy(this.gameObject);
        }
    }
}
