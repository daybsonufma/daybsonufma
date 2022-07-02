using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CameraTour : MonoBehaviour
{
    public float distance;
    public float height;
    public float speed;
    public Transform pivotRotation;

    void Start()
    {
        transform.position = pivotRotation.position.normalized * distance;
        transform.forward = pivotRotation.position - transform.position;
        transform.Translate(Vector3.up * height);
    }

    void Update()
    {
        transform.RotateAround(pivotRotation.position, Vector3.up, speed * Time.deltaTime);
    }

    internal void ApplyDistance()
    {
        transform.position = pivotRotation.position.normalized + new Vector3(distance, height, distance);
        transform.LookAt(pivotRotation);
    }

    internal void ApplyHeight()
    {
        transform.position = new Vector3(transform.position.x, 0.1f + height, transform.position.z);
        transform.forward = pivotRotation.position - transform.position;
    }
}
