using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using Invert.StateMachine;
using UniRx;
using UnityEngine;

public class FPSWeaponController : FPSWeaponControllerBase
{
    public FPSWeaponViewModel CreateFPSWeapon(string identifier, WeaponType fpsWeaponType)
    {
        var weapon = CreateFPSWeapon();
        weapon.Identifier = identifier;
        weapon.WeaponType = fpsWeaponType;
        return weapon;
    }

    public override void NextZoom(FPSWeaponViewModel fpsWeapon)
    {
        if (fpsWeapon.MaxZooms - 1 == fpsWeapon.ZoomIndex)
        {
            fpsWeapon.ZoomIndex = 0;
        }
        else
        {
            fpsWeapon.ZoomIndex++;
        }
    }

    public override void InitializeFPSWeapon(FPSWeaponViewModel fpsWeapon)
    {
        fpsWeapon.StateProperty
            .Where(p => p is Reloading) // Subscribe only to when the state is changed to reloading
            .Subscribe(r =>
            {
                // When we've entered the reloading state create a timer that moves it to finished reloading
                Observable.Timer(TimeSpan.FromSeconds(fpsWeapon.ReloadTime))
                    .Subscribe(l =>
                    {
                        fpsWeapon.Ammo = fpsWeapon.RoundSize;
                        ExecuteCommand(fpsWeapon.FinishedReloading);
                    });
            }).DisposeWith(fpsWeapon); // Make sure this is disposed with the weapon
    }

    public override void BulletFired(FPSWeaponViewModel fPSWeapon)
    {
        base.BulletFired(fPSWeapon);
        fPSWeapon.Ammo -= 1;
    }
}