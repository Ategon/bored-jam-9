using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedObject : MonoBehaviour
{
    [SerializeField] private float parallaxSpeed;

    void FixedUpdate()
    {
        float distance = Time.deltaTime * parallaxSpeed;

        transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);

        if (transform.position.x == -8) Destroy(this.gameObject);
    }
}
