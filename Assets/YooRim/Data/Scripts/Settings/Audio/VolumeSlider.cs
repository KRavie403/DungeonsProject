using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{

    [SerializeField] private Slider _BGMSlider, _EffectSlider;
    void Start()
    {
        _BGMSlider.onValueChanged.AddListener(val => SoundManager.Inst.ChangeMasterVolume(val));
        _EffectSlider.onValueChanged.AddListener(val => SoundManager.Inst.ChangeMasterVolume(val));
    }

}
