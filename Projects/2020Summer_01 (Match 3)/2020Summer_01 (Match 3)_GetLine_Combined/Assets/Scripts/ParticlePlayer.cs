using System.Collections;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
    public ParticleSystem[] allParticles;
    public float lifeTime = 1f;

    void Start()
    {
        allParticles = GetComponentsInChildren<ParticleSystem>();

        Destroy(gameObject, lifeTime);
    }

    public void Play()
    {
        foreach (ParticleSystem ps in allParticles)
        {
            ps.Stop();
            ps.Play();
        }
    }
}
