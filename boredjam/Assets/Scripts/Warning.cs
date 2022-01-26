using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    [SerializeField] private GameObject missle;

    private float warningTimer = 2f;

    void FixedUpdate()
    {
        warningTimer -= Time.deltaTime;

        if (warningTimer <= 0)
        {
            Instantiate(missle, transform.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 180));
            Destroy(this.gameObject);
        }
    }
}
