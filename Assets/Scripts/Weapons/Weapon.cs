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

    public float damage;
    public float firerate;

    public Weapon()
    {
        setWeapon(WeaponName.Empty);
    }

    public Weapon(WeaponName newWeaponName)
    {
        setWeapon(newWeaponName);
    }

    public void setWeapon(WeaponName newWeaponName)
    {
        weaponName = newWeaponName;

        switch (newWeaponName)
        {
            case WeaponName.Empty:
                {
                    weaponType = WeaponType.Empty;

                    defaultDamage = 0;
                    defaultFirerate = 0;
                    damage = defaultDamage;
                    firerate = defaultFirerate;
                    break;
                }
            case WeaponName.Deagle:
                {
                    weaponType = WeaponType.Pistol;

                    defaultDamage = 10;
                    defaultFirerate = 1;
                    damage = defaultDamage;
                    firerate = defaultFirerate;
                    break;
                }
            case WeaponName.Glock:
                {
                    weaponType = WeaponType.Pistol;

                    defaultDamage = 5;
                    defaultFirerate = 2;
                    damage = defaultDamage;
                    firerate = defaultFirerate;
                    break;
                }
            case WeaponName.Uzi:
                {
                    weaponType = WeaponType.Pistol;

                    defaultDamage = 1;
                    defaultFirerate = 10;
                    damage = defaultDamage;
                    firerate = defaultFirerate;
                    break;
                }

        }
    }
}

