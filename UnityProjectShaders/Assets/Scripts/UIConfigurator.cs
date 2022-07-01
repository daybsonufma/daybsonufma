using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIConfigurator : MonoBehaviour
{
    public TMP_Dropdown dropdownType;
    public Slider sliderIntensity;
    public Toggle toggleShadow;
    public Toggle toggleAnimateLight;
    public TextMeshProUGUI txtIntensity;

    public Light MainLight;


    private void Awake()
    {
        dropdownType.onValueChanged.AddListener((o) => OnLightTypeChange(o));
        sliderIntensity.onValueChanged.AddListener((v) =>
        {
            MainLight.intensity = v;
            txtIntensity.text = v.ToString("F2");
        });

        toggleShadow.onValueChanged.AddListener((c) => MainLight.shadows = c ? LightShadows.Soft : LightShadows.None);
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
