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
        transform.position = (transform.position - pivotRotation.position).normalized * distance;
        transform.Translate(Vector3.up * height);
        transform.forward = pivotRotation.position - transform.position;
    }

    void Update()
    {
        transform.RotateAround(pivotRotation.position, Vector3.up, speed * Time.deltaTime);
    }
}
