using System.Collections;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public GameObject clearParticlePrefab;

    public void ClearParticleAt(int x, float y, int z = 0)
    {
        GameObject clearParticle = Instantiate(clearParticlePrefab, new Vector3(x, y, z), Quaternion.identity) as GameObject;

        ParticlePlayer particlePlayer = clearParticle.GetComponent<ParticlePlayer>();

        if (particlePlayer != null)
        {
            particlePlayer.Play();
        }
    }
}
