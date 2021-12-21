using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxSpeed, length;
    private float startPos;
    float amountRan;

    void Start()
    {
        startPos = transform.position.x;
    }

    void FixedUpdate()
    {
        amountRan += Time.deltaTime;
        float distance = amountRan * parallaxSpeed;

        transform.position = Vector3.right * (startPos - distance);

        if (transform.position.x < length*-1.5) amountRan -= length*3/parallaxSpeed;
    }
}
