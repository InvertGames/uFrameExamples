using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class FPSPlayerViewModel
{
    public override FPSWeaponViewModel ComputeCurrentWeapon()
    {
        if (CurrentWeaponIndex >= 0 && CurrentWeaponIndex < Weapons.Count)
            return Weapons[CurrentWeaponIndex];
        return null;
    }
}