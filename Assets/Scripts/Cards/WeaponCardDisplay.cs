using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponCardDisplay : MonoBehaviour
{
    public WeaponCard[] weaponCards;

    public Weapon.WeaponName weaponName;
    public Image icon;
    public TextMeshProUGUI weaponNameText;
    public Slider weaponDamage;
    public Slider weaponFirerate;
    public Slider weaponBulletSpeed;


    private CardInteractor interactor;
    private Dictionary<Weapon.WeaponName, int> weaponDictionary;
    private CardInteractor.CardType cardType;

    private void Awake()
    {
        interactor = GetComponent<CardInteractor>();
        cardType = CardInteractor.CardType.Weapon;

        weaponDictionary = new Dictionary<Weapon.WeaponName, int>()
        {
            {Weapon.WeaponName.Empty, -1 },
            {Weapon.WeaponName.Deagle, 0 },
            {Weapon.WeaponName.Glock, 1 },
            {Weapon.WeaponName.Uzi, 2 }
        };
    }

    public void SetCard(Weapon.WeaponName newWeaponName, CardInteractor.CardPosition position)
    {
        Debug.Log($"Setting a new card! Weapon name = {newWeaponName}, position = {position}");
        weaponName = newWeaponName;
        FillCardData(newWeaponName);
        interactor.CreateCard(cardType, position);
    }

    public void SetCard(Weapon.WeaponName newWeaponName, int positionIndex)
    {
        weaponName = newWeaponName;
        FillCardData(newWeaponName);
        
        if(positionIndex == 0)
        {
            interactor.CreateCard(cardType, CardInteractor.CardPosition.Left);
        }
        else if (positionIndex == 1)
        {
            interactor.CreateCard(cardType, CardInteractor.CardPosition.Middle);
        }
        if (positionIndex == 2)
        {
            interactor.CreateCard(cardType, CardInteractor.CardPosition.Right);
        }
        else
        {
            Debug.LogError($"Card out of bounds! Index: {weaponDictionary[newWeaponName]}, Position index: {positionIndex}");
        }
    }

    public void FillCardData(Weapon.WeaponName newWeaponName)
    {
        Debug.Log($"Filling card with data! Weapon name = {newWeaponName}");

        if (newWeaponName != Weapon.WeaponName.Empty)
        {
            int indexToDraw = weaponDictionary[newWeaponName];

            weaponName = newWeaponName;
            icon.sprite = weaponCards[indexToDraw].icon;
            weaponNameText.text = weaponCards[indexToDraw].weaponName;
            weaponDamage.value = weaponCards[indexToDraw].weaponDamage;
            weaponFirerate.value = weaponCards[indexToDraw].weaponFirerate;
            weaponBulletSpeed.value = weaponCards[indexToDraw].weaponBulletSpeed;
        }        
    }
}
