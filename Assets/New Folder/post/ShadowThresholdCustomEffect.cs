using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ShadowThresholdCustomEffect : MonoBehaviour
{
    public Material shadowMaterial;
    public Color shadowColor;
    [Range(0,1)]
    public float shadowThreshold;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        shadowMaterial.SetFloat("_ShadowThreshold", shadowThreshold);
        shadowMaterial.SetColor("_ShadowColor", shadowColor);
        Graphics.Blit(source, destination, shadowMaterial);
    }

}
