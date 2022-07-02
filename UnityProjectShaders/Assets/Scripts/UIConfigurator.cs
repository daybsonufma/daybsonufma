using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class UIConfigurator : MonoBehaviour
{
    public TMP_Dropdown dropdownType;
    public Slider sliderIntensity;
    public Toggle toggleShadow;
    public Toggle toggleAnimate;
    public TextMeshProUGUI txtIntensity;
    public CameraTour cameraTour;

    public Slider toggleLightAngleX;
    public Slider toggleLightAngleY;
    public Slider toggleLightAngleZ;

    public Light MainLight;
    private LineGizmo lineGizmoLight;


    public TMP_Dropdown dropdownShader;
    public GameObject Car1;
    public GameObject Car2;


    public List<MeshRenderer> Car1Materials;
    public List<MeshRenderer> Car2Materials;

    public Shader Phong;
    public Shader BlinPhong;
    public Shader Lambert;
    public Shader Holographic;
    public Shader DefaultShader;

    private void Awake()
    {
        cameraTour = FindObjectOfType<CameraTour>();
        lineGizmoLight = MainLight.GetComponent<LineGizmo>();

        dropdownType.onValueChanged.AddListener((o) => OnLightTypeChange(o));
        dropdownShader.onValueChanged.AddListener((o) => OnShaderTypeChange(o));
        sliderIntensity.onValueChanged.AddListener((v) =>
        {
            MainLight.intensity = v;
            txtIntensity.text = v.ToString("F2");
        });

        toggleShadow.onValueChanged.AddListener((c) => MainLight.shadows = c ? LightShadows.Soft : LightShadows.None);

        toggleAnimate.onValueChanged.AddListener((c) => cameraTour.enabled = c);

        toggleLightAngleX.onValueChanged.AddListener((c) => lineGizmoLight.UpdateLineRender(c, 0));
        toggleLightAngleY.onValueChanged.AddListener((c) => lineGizmoLight.UpdateLineRender(c, 1));
        toggleLightAngleZ.onValueChanged.AddListener((c) => lineGizmoLight.UpdateLineRender(c, 2));
    }

    private void OnShaderTypeChange(int o)
    {
        Shader s = DefaultShader;
        switch (o)
        {
            case 0:
                //Car1.GetComponentsInChildren<MeshRenderer>().ToList().ForEach(m => m.sharedMaterials.ToList().ForEach(sm => sm.shader = Phong));
                s = Lambert;
                break;
            case 1:
                s = Phong;
                break;
            case 2:
                s = BlinPhong;
                break;
            case 3:
                s = Holographic;
                break;
            default:
                s = DefaultShader;
                break;
        }

        foreach (var item in Car1Materials)
        {
            item.materials.ToList().ForEach(sm =>
            {
                if (!sm.name.Equals("Glass_"))
                    sm.shader = s;
            });
        }


        foreach (var item in Car2Materials)
        {
            item.materials.ToList().ForEach(sm =>
            {
                sm.shader = s;
            });
        }

        //Car1Materials.ForEach(m => m.materials.ToList().ForEach(sm => sm.shader = Phong));
    }

    private void Start()
    {
        txtIntensity.text = sliderIntensity.value.ToString("F2");
    }

    public void OnLightTypeChange(int i)
    {
        switch (i)
        {
            case 0:
                MainLight.type = LightType.Directional; break;
            case 1:
                MainLight.type = LightType.Point; break;
            case 2:
                MainLight.type = LightType.Spot; break;
            default:
                MainLight.type = LightType.Directional;
                break;
        }
    }
}
