using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager : MonoBehaviour
{
    [SerializeField] GameObject warningPrefab;
    [SerializeField] RunManager runManager;

    void FixedUpdate()
    {
        if (Random.Range(1, 1000) < (5 + runManager.distance / 4))
        {
            switch (Random.Range(1, 5))
            {
                case 1:
                    transform.position = new Vector3(3f, Random.Range(-0.7f, 1.5f), 0);
                    transform.rotation = Quaternion.Euler(0, 0, -90);
                    break;
                case 2:
                    transform.position = new Vector3(-3f, Random.Range(-0.7f, 1.5f), 0);
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    break;
                case 3:
                case 4:
                case 5:
                    transform.position = new Vector3(Random.Range(-3f, 3f), 1.5f, 0);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
            }

            Instantiate(warningPrefab, transform.position, transform.rotation);
        }
    }
}
