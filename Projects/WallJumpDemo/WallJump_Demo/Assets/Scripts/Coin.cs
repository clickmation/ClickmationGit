using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    [SerializeField] bool coinS;
    [SerializeField] GameObject triggerParticle;
    public int coinAddAmount;
    public float feverAdder;

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!coinS || (coinS && other.GetComponent<Movement>().attacking))
            {
                GameMaster.gameMaster.AddCoin(coinAddAmount);
                GameMaster.gameMaster.FeverAdd(feverAdder);
                AudioManager.PlaySound("coin");
                Destroy(Instantiate(triggerParticle, transform.position, Quaternion.Euler(0, 0, 0)), 3f);
                Destroy(this.gameObject);
            }
        }
    }
}
