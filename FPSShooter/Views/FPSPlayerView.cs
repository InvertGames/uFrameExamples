using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class FPSPlayerView
{
    public List<FPSWeaponViewBase> _WeaponsList = new List<FPSWeaponViewBase>();

    public override void Awake()
    {
        base.Awake();

    }

    public override ViewBase CreateWeaponsView(FPSWeaponViewModel fPSWeapon)
    {
        var prefabName = fPSWeapon.WeaponType.ToString() + "Weapon";
        var weapon = InstantiateView(prefabName, fPSWeapon);
        weapon.transform.parent = _WeaponsContainer;
        weapon.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        weapon.transform.localPosition = new Vector3(0f, 0f, 0f);
        weapon.InitializeData(fPSWeapon);
        return weapon;
    }

    public override void WeaponsAdded(ViewBase item)
    {
        base.WeaponsAdded(item);
        _WeaponsList.Add(item as FPSWeaponViewBase);
        CurrentWeaponIndexChanged(FPSPlayer.CurrentWeaponIndex);
    }

    public override void WeaponsRemoved(ViewBase item)
    {
        base.WeaponsRemoved(item);
        Destroy(item.gameObject);
        _WeaponsList.Remove(item as FPSWeaponViewBase);
        CurrentWeaponIndexChanged(FPSPlayer.CurrentWeaponIndex);
    }

    public override void Bind()
    {

        base.Bind();

        this.BindKey(FPSPlayer.SelectWeapon, KeyCode.Alpha1, 0);
        this.BindKey(FPSPlayer.SelectWeapon, KeyCode.Alpha2, 1);
        this.BindKey(FPSPlayer.SelectWeapon, KeyCode.Alpha3, 2);
        this.BindKey(FPSPlayer.NextWeapon, KeyCode.RightArrow);
        this.BindKey(FPSPlayer.PreviousWeapon, KeyCode.LeftArrow);
        this.BindViewCollision(CollisionEventType.Enter, _ => ExecuteApplyDamage(10));
    }

    public override void AfterBind()
    {
        base.AfterBind();
        ExecuteSelectWeapon(0);
    }

    public override void CurrentWeaponIndexChanged(int value)
    {
        base.CurrentWeaponIndexChanged(value);
        Debug.Log(value);
        for (var i = 0; i < this._WeaponsList.Count; i++)
            _WeaponsList[i].gameObject.SetActive(i == value);
    }

    public override void Write(ISerializerStream stream)
    {
        base.Write(stream);


    }

    public override void Read(ISerializerStream stream)
    {
        base.Read(stream);


    }
}