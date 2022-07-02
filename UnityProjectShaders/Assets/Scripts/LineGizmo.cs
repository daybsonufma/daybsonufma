using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LineGizmo : MonoBehaviour
{
    protected LineRenderer lineRenderer;
    public float Length;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Start()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + transform.forward * Length);
    }

    public void UpdateLineRender(float c, int axis)
    {
        switch (axis)
        {
            case 0: transform.rotation = transform.rotation * Quaternion.AngleAxis(c, Vector3.right) ; break; //transform.Rotate(Vector3.right, c, Space.World); break;
            case 1: transform.rotation = transform.rotation * Quaternion.AngleAxis(c, Vector3.up); break;
            case 2: transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, c); break;
            default:
                break;
        }
        lineRenderer.SetPosition(1, transform.position + transform.forward * Length);
    }

    void Update()
    {

    }
}
