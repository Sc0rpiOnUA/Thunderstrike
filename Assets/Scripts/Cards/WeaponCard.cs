using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Weapon Card", menuName = "Card/Weapon Card")]
public class WeaponCard : ScriptableObject
{
    public Sprite icon;

    public Weapon.WeaponName weapon;
    public string weaponName;
    public int weaponDamage;
    public int weaponFirerate;
    public int weaponBulletSpeed;

    private void OnEnable()
    {
        Weapon weaponObject = new Weapon(weapon);

        weaponName = weaponObject.weaponName.ToString();
        weaponDamage = (int)weaponObject.defaultDamage;
        weaponFirerate = (int)weaponObject.defaultFirerate;
        weaponBulletSpeed = (int)weaponObject.defaultBulletSpeed;
    }

    
}
