using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    [SerializeField] private Light2D[] lights;

    void Start()
    {
        StartCoroutine(AttemptSpawnLight());
    }

    IEnumerator AttemptSpawnLight()
    {
        while (true)
        {
            if (Random.Range(1, 5) == 1)
            {
                Light2D spawnedLight = Instantiate(lights[Random.Range(0, lights.Length)], transform.position, Quaternion.identity);
                spawnedLight.transform.parent = transform;
                spawnedLight.GetComponent<Light2D>().intensity = Random.Range(0, 0.75f);
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
