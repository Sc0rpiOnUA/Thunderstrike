using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponCardDisplay : MonoBehaviour
{
    public int indexToDraw;

    public WeaponCard[] weaponCards;

    public Image icon;
    public TextMeshProUGUI weaponName;
    public Slider weaponDamage;
    public Slider weaponFirerate;
    public Slider weaponBulletSpeed;

    private void Start()
    {
        icon.sprite = weaponCards[indexToDraw].icon;
        weaponName.text = weaponCards[indexToDraw].weaponName;
        weaponDamage.value = weaponCards[indexToDraw].weaponDamage;
        weaponFirerate.value = weaponCards[indexToDraw].weaponFirerate;
        weaponBulletSpeed.value = weaponCards[indexToDraw].weaponBulletSpeed;
    }
}
