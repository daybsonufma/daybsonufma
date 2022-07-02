using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class UIConfigurator : MonoBehaviour
{
    public TextMeshProUGUI txtFPS;

    public Slider sliderCameraDistance;
    public TextMeshProUGUI txtCameraDistance;
    public Slider sliderCameraHeight;
    public TextMeshProUGUI txtCameraHeight;
    public Slider sliderCameraSpeed;
    public TextMeshProUGUI txtCameraSpeed;

    [Space]

    public TMP_Dropdown dropdownType;
    public Slider sliderIntensity;
    public Toggle toggleShadow;
    public Toggle toggleAnimate;
    public TextMeshProUGUI txtIntensity;
    public CameraTour cameraTour;

    public Slider toggleLightAngleX;
    public Slider toggleLightAngleY;
    public Slider toggleLightAngleZ;

    public Slider sliderAngleSpot;
    public TextMeshProUGUI txtAngleSpot;

    public Slider sliderRangePoint;
    public TextMeshProUGUI txtRangePoint;

    public Light MainLight;
    private LineGizmo lineGizmoLight;


    public TMP_Dropdown dropdownShader;
    public GameObject Car1;
    public GameObject Car2;


    //public List<MeshRenderer> Car1Materials;
    public List<MeshRenderer> Car2Materials;

    public Shader Phong;
    public Shader BlinPhong;
    public Shader Lambert;
    public Shader Holographic;
    public Shader DefaultShader;

    private Shader currentShader;

    public Slider specularPowerShader;
    public TextMeshProUGUI txtSpecularPower;

    public Slider sliderTransparency;
    public TextMeshProUGUI txtTransparency;

    public GameObject buttonsMainColor;
    public GameObject buttonsSpecColor;

    public Toggle toggleElements;
    public List<GameObject> sceneElements;

    private void Awake()
    {
        cameraTour = FindObjectOfType<CameraTour>();
        lineGizmoLight = MainLight.GetComponent<LineGizmo>();

        sliderCameraDistance.onValueChanged.AddListener((v) =>
        {
            cameraTour.distance = v;
            txtCameraDistance.text = $"{v.ToString("F2")} m";
            cameraTour.ApplyDistance();
        });
        sliderCameraHeight.onValueChanged.AddListener((v) =>
        {
            cameraTour.height = v;
            txtCameraHeight.text = $"{v.ToString("F2")} m";
            cameraTour.ApplyHeight();
        });
        sliderCameraSpeed.onValueChanged.AddListener((v) =>
        {
            cameraTour.speed = v;
            txtCameraSpeed.text = $"{v.ToString("F2")} °/s";
        });


        dropdownType.onValueChanged.AddListener((o) => OnLightTypeChange(o));
        dropdownShader.onValueChanged.AddListener((o) => OnShaderTypeChange(o));
        sliderIntensity.onValueChanged.AddListener((v) =>
        {
            MainLight.intensity = v;
            txtIntensity.text = v.ToString("F2");
        });


        sliderAngleSpot.onValueChanged.AddListener((v) =>
        {
            if (MainLight.type == LightType.Spot)
            {
                MainLight.spotAngle = v;
                txtAngleSpot.text = $"{v.ToString("F2")} °";
            }
        });


        sliderRangePoint.onValueChanged.AddListener((v) =>
        {
            if (MainLight.type == LightType.Point)
            {
                MainLight.range = v;
                txtRangePoint.text = $"{v.ToString("F2")}";
            }
        });

        specularPowerShader.onValueChanged.AddListener((v) =>
        {
            foreach (var item in Car2Materials)
            {
                item.materials.ToList().ForEach(sm =>
                {
                    sm.SetFloat("_SpecPower", v);
                    txtSpecularPower.text = v.ToString("F2");
                });
            }
        });

        sliderTransparency.onValueChanged.AddListener((v) =>
        {
            foreach (var item in Car2Materials)
            {
                item.materials.ToList().ForEach(sm =>
                {
                    sm.SetFloat("_RimEffect", v);
                    txtTransparency.text = v.ToString("F2");
                });
            }
        });

        toggleShadow.onValueChanged.AddListener((c) => MainLight.shadows = c ? LightShadows.Soft : LightShadows.None);

        toggleAnimate.onValueChanged.AddListener((c) => cameraTour.enabled = c);

        toggleElements.onValueChanged.AddListener((c) => sceneElements.ForEach(o => o.SetActive(c)));

        buttonsMainColor.GetComponentsInChildren<Button>().ToList().ForEach(b =>
        {
            b.onClick.AddListener(() =>
            {
                var colors = b.GetComponent<Button>().colors;
                foreach (var item in Car2Materials)
                {
                    item.materials.ToList().ForEach(sm =>
                    {
                        sm.SetColor("_MainColor", colors.normalColor);
                    });
                }
            });
        });


        buttonsSpecColor.GetComponentsInChildren<Button>().ToList().ForEach(b =>
        {
            b.onClick.AddListener(() =>
            {
                var colors = b.GetComponent<Button>().colors;
                foreach (var item in Car2Materials)
                {
                    item.materials.ToList().ForEach(sm =>
                    {
                        sm.SetColor("_SpecularColor", colors.normalColor);
                    });
                }
            });
        });

        // toggleLightAngleX.onValueChanged.AddListener((c) => lineGizmoLight.UpdateLineRender(c, 0));
        // toggleLightAngleY.onValueChanged.AddListener((c) => lineGizmoLight.UpdateLineRender(c, 1));
        // toggleLightAngleZ.onValueChanged.AddListener((c) => lineGizmoLight.UpdateLineRender(c, 2));
    }

    private void OnShaderTypeChange(int o)
    {
        sliderTransparency.interactable = false;
        currentShader = DefaultShader;
        switch (o)
        {
            case 0:
                //Car1.GetComponentsInChildren<MeshRenderer>().ToList().ForEach(m => m.sharedMaterials.ToList().ForEach(sm => sm.shader = Phong));
                currentShader = Lambert;
                break;
            case 1:
                currentShader = Phong;
                break;
            case 2:
                currentShader = BlinPhong;
                break;
            case 3:
                sliderTransparency.interactable = true;
                currentShader = Holographic;
                break;
            default:
                currentShader = DefaultShader;
                break;
        }

        foreach (var item in Car2Materials)
        {
            item.materials.ToList().ForEach(sm =>
            {
                if (!sm.name.Equals("Glass_"))
                    sm.shader = currentShader;
            });
        }

        //Car1Materials.ForEach(m => m.materials.ToList().ForEach(sm => sm.shader = Phong));
    }


    private void Start()
    {
        MainLight.type = LightType.Directional;

        txtIntensity.text = sliderIntensity.value.ToString("F2");

        sliderCameraDistance.value = cameraTour.distance;

        sliderCameraHeight.value = cameraTour.height;

        sliderCameraSpeed.value = cameraTour.speed;
    }

    private void Update()
    {
        txtFPS.text = $"{(int)(1 / Time.smoothDeltaTime)}fps";
    }

    public void OnLightTypeChange(int i)
    {
        sliderAngleSpot.interactable = false;
        sliderRangePoint.interactable = false;
        switch (i)
        {
            case 0:
                MainLight.type = LightType.Directional;
                break;
            case 1:
                MainLight.type = LightType.Point;
                sliderRangePoint.interactable = true;
                sliderRangePoint.value = MainLight.range;
                break;
            case 2:
                MainLight.type = LightType.Spot;
                sliderAngleSpot.interactable = true;
                break;
            default:
                MainLight.type = LightType.Directional;
                break;
        }
    }
}
