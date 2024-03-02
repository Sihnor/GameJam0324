using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatBar : MonoBehaviour
{
    [SerializeField] private Image HeatBarImage;

    private void Awake()
    {
        EventManager.Instance.FOnHeatChange += UpdateHeatBar;
    }

       private void UpdateHeatBar(float heat)
        {
            this.HeatBarImage.fillAmount = heat / 100;
        }
}
