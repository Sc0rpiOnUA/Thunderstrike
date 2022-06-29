using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float health;
    public float speed;

    public bool onCooldown;

    public GameObject[] bullets;

    public Weapon weapon;
    public PlayerController playerController;
    public PlayerInventory playerInventory;
    public GameObject weaponHolder;

    [ReadOnly] [SerializeField] private GameObject weaponGameObject;

    [ReadOnly] [SerializeField] private Weapon.WeaponName weaponName;
    [ReadOnly] [SerializeField] private Weapon.WeaponType weaponType;
    [ReadOnly] [SerializeField] private float weaponDamage;
    [ReadOnly] [SerializeField] private float weaponFirerate;
    [ReadOnly] [SerializeField] private float weaponBulletSpeed;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerInventory = GetComponent<PlayerInventory>();

        health = 100;
        speed = 7;

        onCooldown = false;

        weapon = new Weapon();        
    }

    private void Update()
    {
        playerController.playerSpeed = speed;

        weaponName = weapon.weaponName;
        weaponType = weapon.weaponType;
        weaponDamage = weapon.damage;
        weaponFirerate = weapon.firerate;
        weaponBulletSpeed = weapon.bulletSpeed;
    }

    public void FireShot()
    {
        if(weapon.weaponType != Weapon.WeaponType.Empty && !onCooldown)
        {
            float cooldownTime;
            cooldownTime = 1 / weapon.firerate;
            StartCoroutine(ShotCooldown(cooldownTime));

            BulletSpawner bulletSpawner = weaponGameObject.GetComponent<BulletSpawner>();

            Debug.Log("Boom!");
            bulletSpawner.SpawnBullet(weaponBulletSpeed);
        }
    }

    public void ChangeWeapon(Weapon newWeapon)
    {
        Debug.Log($"Setting weapon to {newWeapon}");

        //if (newWeapon.weaponType != Weapon.WeaponType.Empty)
        //{
        //    Debug.Log("Searching for weapon!");
        //    weaponGameObject = FindChildWithTag(weaponHolder, "Weapon");
        //}
        //else
        //{
        //    weaponGameObject = null;
        //}

        weapon.setWeapon(newWeapon.weaponName);
        playerController.ChangeWeaponType(newWeapon.weaponType);
        weaponGameObject = playerInventory.switchWeaponRightHand(newWeapon.weaponName);
    }

    GameObject FindChildWithTag(GameObject parent, string tag)
    {
        Debug.Log($"Beginning search");

        GameObject child = null;

        foreach (Transform transform in parent.transform)
        {
            Debug.Log($"Comparing tag {tag} for transform {transform}");
            if (transform.CompareTag(tag))
            {
                child = transform.gameObject;
                break;
            }
        }

        return child;
    }


    private IEnumerator ShotCooldown(float cooldown)
    {
        onCooldown = true;
        yield return new WaitForSecondsRealtime(cooldown);
        onCooldown = false;
    }
}
