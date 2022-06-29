using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject weaponHolderRight;
    public GameObject[] weaponPrefabs;

    public GameObject switchWeaponRightHand(Weapon.WeaponName weaponName)
    {
        GameObject newWeaponObject = null;

        switch(weaponName)
        {
            case Weapon.WeaponName.Empty:
                {
                    newWeaponObject = null;

                    if (weaponHolderRight.transform.childCount > 0)
                    {
                        Destroy(weaponHolderRight.transform.GetChild(0).gameObject);
                    }
                    break;
                }
            case Weapon.WeaponName.Deagle:
                {
                    if(weaponHolderRight.transform.childCount > 0)
                    {
                        Destroy(weaponHolderRight.transform.GetChild(0).gameObject);
                    }
                    newWeaponObject = Instantiate(weaponPrefabs[0], weaponHolderRight.transform.position, weaponHolderRight.transform.rotation, weaponHolderRight.transform);
                    break;
                }
            case Weapon.WeaponName.Glock:
                {
                    if (weaponHolderRight.transform.childCount > 0)
                    {
                        Destroy(weaponHolderRight.transform.GetChild(0).gameObject);
                    }
                    newWeaponObject = Instantiate(weaponPrefabs[1], weaponHolderRight.transform.position, weaponHolderRight.transform.rotation, weaponHolderRight.transform);
                    break;
                }
            case Weapon.WeaponName.Uzi:
                {
                    if (weaponHolderRight.transform.childCount > 0)
                    {
                        Destroy(weaponHolderRight.transform.GetChild(0).gameObject);
                    }
                    newWeaponObject = Instantiate(weaponPrefabs[2], weaponHolderRight.transform.position, weaponHolderRight.transform.rotation, weaponHolderRight.transform);
                    break;
                }
        }

        return newWeaponObject;
    }
}
