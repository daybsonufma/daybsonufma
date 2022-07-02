using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ObjectLoader : MonoBehaviour
{
    public GameObject obj;
    public int row;
    public int col;

    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public Material material;

    void Start()
    {
        var render = obj.GetComponent<MeshRenderer>();
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                var o = Instantiate(obj);
                o.transform.position = new Vector3(i * render.bounds.size.x,
                                                   j * render.bounds.size.y,
                                                   0);
                o.gameObject.SetActive(true);
            }
        }

        var cam = FindObjectOfType<Camera>();
        cam.transform.position = new Vector3(row * render.bounds.size.x / 2,
                                             col * render.bounds.size.y / 2, 
                                             0);
        var center = new Vector3(row * render.bounds.size.x / 2,
                                             col * render.bounds.size.y / 2,
                                             0);

        float cameraView = 2.0f * Mathf.Tan(0.5f * Mathf.Deg2Rad * cam.fieldOfView); // Visible height 1 meter in front
        float distance = 3 * center.magnitude / cameraView; // Combined wanted distance from the object
        cam.transform.position = center - distance * cam.transform.forward;
    }

    void Update()
    {

    }
}
