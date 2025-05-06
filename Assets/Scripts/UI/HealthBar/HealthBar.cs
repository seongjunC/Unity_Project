using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : BillbordObejct
{
    [SerializeField] protected Slider slider;
    [SerializeField] protected Slider easeSlider;
    [SerializeField] protected float easeSpeed;

    protected override void Update()
    {
        base.Update();
    }

}
