using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class FPSWeaponViewModel
{
    public override bool ComputeIsEmpty()
    {
        return Ammo <= 0;
    }
}