using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float health;
    public float speed;
    
    public Weapon weapon;
    public PlayerController playerController;
    public PlayerInventory playerInventory;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerInventory = GetComponent<PlayerInventory>();

        health = 100;
        speed = 7;

        weapon = new Weapon();       
    }

    private void Update()
    {
        playerController.playerSpeed = speed;
    }

    public void ChangeWeapon(Weapon.WeaponName newWeapon)
    {
        weapon.setWeapon(newWeapon);
        playerController.ChangeWeaponType(weapon.weaponType);
        playerInventory.switchWeaponRightHand(weapon.weaponName);
    }
}
