using System.Collections;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    [SerializeField] private ParticlesNames _particleNames;

    [SerializeField] private GameObject[] particlesObjects;
    
    #region singleton

    public static ParticlesController instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public void SpawnParticle(ParticlesNames particlesName, Transform pos, float spawnDelay = 0, Vector3 spawnOffset = default(Vector3), float scaleMultiple = 1)
    {
        if (spawnDelay > 0)
        {
            StartCoroutine(spawnWithDelay(particlesName, pos, spawnDelay, spawnOffset, scaleMultiple));
            return;
        }
        GameObject obj = Instantiate(particlesObjects[(int)particlesName], pos.position + spawnOffset, Quaternion.identity, transform);
        obj.transform.localScale *= scaleMultiple;
        obj.gameObject.SetActive(true);
    }

    IEnumerator spawnWithDelay(ParticlesNames particlesName, Transform pos, float spawnDelay = 0, Vector3 spawnOffset = default(Vector3), float scaleMultiple = 0)
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnParticle(particlesName, pos, 0, spawnOffset, scaleMultiple);
    }
}

public enum ParticlesNames
{
    Bomb,
    Explosion,
    Spawn
}
