using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    public enum WeaponName { Empty, Deagle, Glock, Uzi}
    public WeaponName weaponName;
    public enum WeaponType { Empty, Pistol}
    public WeaponType weaponType;

    public float defaultDamage;
    public float defaultFirerate;
    public float defaultBulletSpeed;

    public float damage;
    public float firerate;
    public float bulletSpeed;

    public Weapon()
    {
        SetWeapon(WeaponName.Empty);
    }

    public Weapon(WeaponName newWeaponName)
    {
        SetWeapon(newWeaponName);
    }

    public void SetWeapon(WeaponName newWeaponName)
    {
        weaponName = newWeaponName;

        switch (newWeaponName)
        {
            case WeaponName.Empty:
                {
                    weaponType = WeaponType.Empty;

                    defaultDamage = 0;
                    defaultFirerate = 0;
                    defaultBulletSpeed = 0;                    
                    break;
                }
            case WeaponName.Deagle:
                {
                    weaponType = WeaponType.Pistol;

                    defaultDamage = 20;
                    defaultFirerate = 1;
                    defaultBulletSpeed = 20;
                    break;
                }
            case WeaponName.Glock:
                {
                    weaponType = WeaponType.Pistol;

                    defaultDamage = 5;
                    defaultFirerate = 4;
                    defaultBulletSpeed = 25;
                    break;
                }
            case WeaponName.Uzi:
                {
                    weaponType = WeaponType.Pistol;

                    defaultDamage = 2;
                    defaultFirerate = 10;
                    defaultBulletSpeed = 30;
                    break;
                }

        }

        damage = defaultDamage;
        firerate = defaultFirerate;
        bulletSpeed = defaultBulletSpeed;
    }
}

