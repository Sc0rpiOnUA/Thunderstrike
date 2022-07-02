using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Weapon Card", menuName = "Weapon Card")]
public class WeaponCard : ScriptableObject
{
    public Sprite icon;

    public string weaponName;
    public int weaponDamage;
    public int weaponFirerate;
    public int weaponBulletSpeed;
}
