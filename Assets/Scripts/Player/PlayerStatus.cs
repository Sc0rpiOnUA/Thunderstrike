using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public int speed;

    public bool onCooldown;

    //public GameObject[] bullets;

    public Weapon weapon;
    public PlayerController playerController;
    public PlayerInventory playerInventory;
    public GameObject weaponHolder;

    [SerializeField] private GameObject weaponGameObject;

    [SerializeField] private Weapon.WeaponName weaponName;
    [SerializeField] private Weapon.WeaponType weaponType;
    [SerializeField] private float weaponDamage;
    [SerializeField] private float weaponFirerate;
    [SerializeField] private float weaponBulletSpeed;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerInventory = GetComponent<PlayerInventory>();

        health = maxHealth;
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
            bulletSpawner.SpawnBullet(weapon.bulletSpeed, weapon.damage);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    public void IncreaseHealth(int incHealth)
    {
        if(health + incHealth > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += incHealth;
        }

        Debug.Log($"Healed {incHealth} HP! Current health: {health}");
    }

    public void Die()
    {
        Debug.Log("Player died!");
    }

    public void ChangeWeapon(Weapon newWeapon)
    {
        Debug.Log($"Setting weapon to {newWeapon}");

        weapon.SetWeapon(newWeapon.weaponName);
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
