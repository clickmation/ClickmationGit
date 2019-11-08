using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] bool coinS;
    [SerializeField] GameObject triggerParticle;
    public int coinAddAmount;
    public int score;
    public float feverAdder;
    [SerializeField] int coinSPercent;

    void Start()
    {
        if (coinS)
        {
            int r = Random.Range(0, 100);
            if (r > coinSPercent) Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!coinS || (coinS && other.GetComponent<Movement>().attacking))
            {
                GameMaster.gameMaster.AddCoin(coinAddAmount);
                GameMaster.gameMaster.FeverAdd(feverAdder);
                GameMaster.gameMaster.AddScore(score * GameMaster.gameMaster.scoreMultiplier);
                if (!coinS) AudioManager.PlaySound("coin");
                else AudioManager.PlaySound("coinS");
                Destroy(Instantiate(triggerParticle, transform.position, Quaternion.Euler(0, 0, 0)), 3f);
                Destroy(this.gameObject);
            }
            else if (coinS && !other.GetComponent<Movement>().attacking)
            {
                AudioManager.PlaySound("coinSFail");
                anim.SetTrigger("Death");
            }
        }
    }
}
