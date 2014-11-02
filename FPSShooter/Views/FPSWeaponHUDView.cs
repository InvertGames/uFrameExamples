using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using UnityEngine.UI;


public partial class FPSWeaponHUDView
{
    public Text _AmmoText;
    public Text _RoundSizeText;
    public Slider _AmmoSlider;

    /// Subscribes to the property and is notified anytime the value changes.
    public override void AmmoChanged(Int32 value) {
        base.AmmoChanged(value);
        _AmmoText.text = value.ToString();
        _AmmoSlider.value = value;
    }
    
    /// Subscribes to the property and is notified anytime the value changes.
    public override void RoundSizeChanged(Int32 value) {
        base.RoundSizeChanged(value);
        _RoundSizeText.text = value.ToString();
        _AmmoSlider.maxValue = value;
    }
}
