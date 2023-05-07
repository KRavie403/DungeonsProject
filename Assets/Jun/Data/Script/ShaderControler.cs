using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class ShaderControler : MonoBehaviour //·»´õ¸µ??
{
    public Material effectmat; 
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, effectmat);
    }
}
