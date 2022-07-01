using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public int speed;

    public bool onCooldown;

    public Weapon weapon;
    public PlayerController playerController;
    public PlayerInventory playerInventory;
    public GameObject weaponHolder;
    public GameObject healthBarObject;
    public HealthBar healthBar;

    [SerializeField] private GameObject weaponGameObject;

    //[SerializeField] private Weapon.WeaponName weaponName;
    //[SerializeField] private Weapon.WeaponType weaponType;
    //[SerializeField] private float weaponDamage;
    //[SerializeField] private float weaponFirerate;
    //[SerializeField] private float weaponBulletSpeed;

    private bool isDying;
    private Collider playerCollider;

    private void Awake()
    {
        playerCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        healthBarObject = GameObject.FindGameObjectWithTag("PlayerHealthBar");

        playerController = GetComponent<PlayerController>();
        playerInventory = GetComponent<PlayerInventory>();
        healthBar = healthBarObject.GetComponent<HealthBar>();

        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(health);
        onCooldown = false;
        isDying = false;

        weapon = new Weapon();        
    }

    private void Update()
    {
        if(!isDying)
        {
            playerController.playerSpeed = speed;
        }

        //weaponName = weapon.weaponName;
        //weaponType = weapon.weaponType;
        //weaponDamage = weapon.damage;
        //weaponFirerate = weapon.firerate;
        //weaponBulletSpeed = weapon.bulletSpeed;
    }

    public void FireShot()
    {
        if(weapon.weaponType != Weapon.WeaponType.Empty && !onCooldown && !isDying)
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
        healthBar.SetHealth(health);

        if (health <= 0 && !isDying)
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

        healthBar.SetHealth(health);

        Debug.Log($"Healed {incHealth} HP! Current health: {health}");
    }

    public void Die()
    {
        Debug.Log("Player died!");
        isDying = true;

        Destroy(healthBar.gameObject);
        playerCollider.enabled = false;
        playerController.Die();
    }

    public void ChangeWeapon(Weapon newWeapon)
    {
        Debug.Log($"Setting weapon to {newWeapon}");

        weapon.SetWeapon(newWeapon.weaponName);
        playerController.ChangeWeaponType(newWeapon.weaponType);
        weaponGameObject = playerInventory.switchWeaponRightHand(newWeapon.weaponName);
    }

    private IEnumerator ShotCooldown(float cooldown)
    {
        onCooldown = true;
        yield return new WaitForSecondsRealtime(cooldown);
        onCooldown = false;
    }
}
