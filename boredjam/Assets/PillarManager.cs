using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarManager : MonoBehaviour
{
    [SerializeField] private GameObject pillar;

    void Start()
    {
        StartCoroutine(AttemptSpawnPillar());
    }

    IEnumerator AttemptSpawnPillar()
    {
        while (true)
        {
            if (Random.Range(1, 3) == 1)
            {
                GameObject spawnedPillar = Instantiate(pillar, transform.position, Quaternion.identity);
                spawnedPillar.transform.parent = transform;
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
