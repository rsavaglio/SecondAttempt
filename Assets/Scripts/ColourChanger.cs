using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ColourChanger : MonoBehaviour
{
    public Color myColor = Color.red;
    
    [Space(10)]
    public Material myMaterial;
    public Light myPointLight;


    void Update()
    {
        myMaterial.SetColor("_EmissionColor", myColor);
        myPointLight.color = myColor;
    }
}
