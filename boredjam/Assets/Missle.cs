using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    private float missleTimer = 8f;

    void FixedUpdate()
    {
        missleTimer -= Time.deltaTime;

        if(missleTimer < 0)
        {
            Destroy(this.gameObject);
        }

        transform.Translate(Vector3.up * Time.deltaTime);
    }
}
