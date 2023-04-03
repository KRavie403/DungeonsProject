using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent (typeof(Camera))]
public class Chromatic : MonoBehaviour
{
    public Material chromaticMaterial;
    [Range(0f, 0.5f)]
    public float rgbSplit;
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        chromaticMaterial.SetFloat("_RGBSplit", rgbSplit);
        Graphics.Blit (source, destination, chromaticMaterial);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
