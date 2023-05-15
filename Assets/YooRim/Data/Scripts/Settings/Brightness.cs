using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brightness : MonoBehaviour
{
    [SerializeField] Slider brightnessSlider;
    // Start is called before the first frame update
    Image myImage = null;
    void Start()    
    {
        myImage = GetComponent<Image>();
        ChangeValue(brightnessSlider.value);
    }

    void Update()
    {
    }

    public void ChangeValue(float v)
    {
        myImage.color = new Color(0, 0, 0, 1.0f - v);
    }
}
