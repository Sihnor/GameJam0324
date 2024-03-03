using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

enum ESetting
{
    MouseSenseX,
    MouseSenseY,
    GamepadSenseX,
    GamepadSenseY,
    WindCampfireVolume,
    BreathVolume,
    StepsVolume
}

public class UpdateSliderValue : MonoBehaviour
{
    private Slider Slider;
    [SerializeField] private TextMeshProUGUI SliderValue;
    [SerializeField] private bool IsVolumeSlider = false;
    [SerializeField] private AudioMixer AudioMixer;
    [SerializeField] private String AudioMixerParameter;

    [SerializeField] private ESetting Setting;

    private void Awake()
    {
        this.Slider = GetComponentInChildren<Slider>();
       
        if (this.IsVolumeSlider)
        {
            this.SliderValue.text = (this.Slider.value * 100).ToString("F");
        }
        else
        {
            this.SliderValue.text = this.Slider.value.ToString("F");
        }
        
        this.Slider.onValueChanged.AddListener(UpdateValue);
    }

    private void Start()
    {
        SetValue(); 
    }

    private void UpdateValue(float value)
    {
        if (this.IsVolumeSlider)
        {
            this.SliderValue.text = (this.Slider.value * 100).ToString("F");
        }
        else
        {
            this.SliderValue.text = this.Slider.value.ToString("F");
        }
        
        SetValue();
        
        if (this.AudioMixer == null) return;
        this.AudioMixer.SetFloat(this.AudioMixerParameter, Mathf.Log10(value) * 20);
        
        
    }

    private void SetValue()
    {
        switch (this.Setting)
        {
            case ESetting.MouseSenseX:
                GameManager.Instance.MouseSenseX = this.Slider.value;
                break;
            case ESetting.MouseSenseY:
                GameManager.Instance.MouseSenseY = this.Slider.value;
                break;
            case ESetting.GamepadSenseX:
                GameManager.Instance.GamepadSenseX = this.Slider.value;
                break;
            case ESetting.GamepadSenseY:
                GameManager.Instance.GamepadSenseY = this.Slider.value;
                break;
            case ESetting.WindCampfireVolume:
                GameManager.Instance.WindCampfireVolume = this.Slider.value;
                break;
            case ESetting.BreathVolume:
                GameManager.Instance.BreathVolume = this.Slider.value;
                break;
            case ESetting.StepsVolume:
                GameManager.Instance.StepsVolume = this.Slider.value;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
