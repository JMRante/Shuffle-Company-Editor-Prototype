using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageConfig : MonoBehaviour
{
    [HideInInspector]
    public string title;

    private Color mainLightColor;
    private Vector3 lightRotation;

    public GameObject mainLight;

    public void SetTitle(string title)
    {
        this.title = title;
    }

    public void ApplyMainLightColor()
    {
        mainLight.GetComponent<Light>().color = mainLightColor;
    }

    public void ApplyMainLightColorRed(string red)
    {
        mainLightColor.r = float.Parse(red);
        ApplyMainLightColor();
    }

    public void ApplyMainLightColorGreen(string green)
    {
        mainLightColor.g = float.Parse(green);
        ApplyMainLightColor();
    }

    public void ApplyMainLightColorBlue(string blue)
    {
        mainLightColor.b = float.Parse(blue);
        ApplyMainLightColor();
    }

    public void ApplyLightRotation()
    {
        mainLight.transform.rotation = Quaternion.identity;
        mainLight.transform.Rotate(lightRotation);
    }

    public void ApplyLightRotationX(string x)
    {
        lightRotation.x = int.Parse(x);
        ApplyLightRotation();
    }

    public void ApplyLightRotationY(string y)
    {
        lightRotation.y = int.Parse(y);
        ApplyLightRotation();
    }

    public void ApplyLightRotationZ(string z)
    {
        lightRotation.z = int.Parse(z);
        ApplyLightRotation();
    }
}
