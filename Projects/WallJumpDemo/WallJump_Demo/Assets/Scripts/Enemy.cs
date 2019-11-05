using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField]
	public GameObject deathParticle;
    public int score;
    [SerializeField] EnemyType enemyType;
    enum EnemyType
    {
        DEFAULT,
        STOMP,
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.transform.GetComponent<Movement>() != null)
        {
            Movement mov = other.transform.GetComponent<Movement>();
            //if (enemyType == EnemyType.DEFAULT)
            //{
                if (!mov.attacking)
                {
                    GameMaster.gameMaster.Dead();
                }
                else
                {
                    GameMaster.gameMaster.SpawnShockWave(mov.shockWaveKill, 2f);
                    GameMaster.gameMaster.KillCount(1);
                    GameMaster.gameMaster.AddScore(score * GameMaster.gameMaster.scoreMultiplier);
                    mov.camShake.Shake(10);
                    AudioManager.PlaySound("kill");
                    GameObject clone;
                    clone = Instantiate(deathParticle, this.transform.position, Quaternion.identity) as GameObject;
                    Destroy(clone, 3f);
                    Destroy(this.gameObject);
                }
            //}
            //else
            //{
            //    if (!mov.attacking && other.transform.position.y - this.transform.position.y <= 1.1f)
            //    {
            //        mov.Dead();
            //    }
            //    else
            //    {
            //        AudioManager.PlaySound("kill");
            //        GameObject clone;
            //        clone = Instantiate(deathParticle, this.transform.position, Quaternion.identity) as GameObject;
            //        Destroy(clone, 3f);
            //        Destroy(this.gameObject);
            //        mov.Jump(1, Vector2.zero);
            //    }
            //}
        }
    }
}
