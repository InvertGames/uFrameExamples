using System.Collections;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class FPSPlayerController : FPSPlayerControllerBase
{
    
    public override void NextWeapon(FPSPlayerViewModel fPsPlayer)
    {
        if (fPsPlayer.CurrentWeaponIndex == fPsPlayer.Weapons.Count - 1)
        {
            fPsPlayer.CurrentWeaponIndex = 0;
        }
        else
        {
            fPsPlayer.CurrentWeaponIndex++;
        }
        
    }
    
    public override void PickupWeapon( FPSPlayerViewModel fPsPlayer, FPSWeaponViewModel fpsWeaponViewModel)
    {
        fPsPlayer.Weapons.Add(fpsWeaponViewModel);
        
    }

    public override void PreviousWeapon(FPSPlayerViewModel fPsPlayer)
    {
        if (fPsPlayer.CurrentWeaponIndex == 0)
        {
            fPsPlayer.CurrentWeaponIndex = fPsPlayer.Weapons.Count - 1;
        }
        else
        {
            fPsPlayer.CurrentWeaponIndex--;
        }
    }

    public override void InitializeFPSPlayer(FPSPlayerViewModel player)
    {
        // The weapons are a scene first binding, so we create them in the awake method
        Debug.Log("Initializing player");
        player.Weapons.Add(FPSWeaponController.CreateFPSWeapon("MP5Weapon",WeaponType.MP5));
        player.Weapons.Add(FPSWeaponController.CreateFPSWeapon("UMP5Weapon", WeaponType.UMP5));
        player.Weapons.Add(FPSWeaponController.CreateFPSWeapon("ColtWeapon", WeaponType.Colt));
    }

    public override void SelectWeapon( FPSPlayerViewModel fPsPlayer, int index)
    {
        if (index > fPsPlayer.Weapons.Count) return;
        fPsPlayer.CurrentWeaponIndex = index;
    }
}