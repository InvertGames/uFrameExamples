
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


public partial class FPSPlayerHUDView
{
    public FPSWeaponHUDView _WeaponHudView;
    /// Subscribes to the property and is notified anytime the value changes.
    public override void CurrentWeaponChanged(FPSWeaponViewModel value) {
        base.CurrentWeaponChanged(value);
        if (value != null)
        _WeaponHudView.ViewModelObject = value;
    }

    public Slider _HealthSlider;

    /// Subscribes to the property and is notified anytime the value changes.
    public override void HealthChanged(Single value) {
        base.HealthChanged(value);
        _HealthSlider.value = value;
    }

    /// Invokes NextWeaponExecuted when the NextWeapon command is executed.
    public override void NextWeaponExecuted() {
        base.NextWeaponExecuted();

    }

    /// Invokes PickupWeaponExecuted when the PickupWeapon command is executed.
    public override void PickupWeaponExecuted() {
        base.PickupWeaponExecuted();
    }


    public override void Bind()
    {
        base.Bind(); 
        
    }

    public override ViewBase CreateWeaponsView(FPSWeaponViewModel fPSWeapon)
    {
       
        return null;
    }
    


}
