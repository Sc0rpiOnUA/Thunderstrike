using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerStatus : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public float speed;

    public bool onCooldown;
    public bool friendlyFire;

    public Weapon weapon;
    public PlayerController playerController;
    public PlayerInventory playerInventory;
    public GameObject weaponHolderRight;
    public GameObject healthBarObject;
    public HealthBar healthBar;

    private GameObject gameManagerObject;
    private GameManager gameManager;
    [SerializeField] private GameObject weaponGameObject;

    [SerializeField] private Weapon.WeaponName weaponName;
    [SerializeField] private Weapon.WeaponType weaponType;
    [SerializeField] private float weaponDamage;
    [SerializeField] private float weaponFirerate;
    [SerializeField] private float weaponBulletSpeed;

    private bool canMove, isDying, chestInRange;
    private Collider playerCollider;

    private void Awake()
    {
        canMove = true;
        chestInRange = false;
        playerCollider = GetComponent<Collider>();
        gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
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

        weaponName = weapon.weaponName;
        weaponType = weapon.weaponType;
        weaponDamage = weapon.damage;
        weaponFirerate = weapon.firerate;
        weaponBulletSpeed = weapon.bulletSpeed;
    }

    public void StopMovement()
    {
        canMove = false;
        playerController.StopMovement();
    }
    public void ResumeMovement()
    {
        canMove = true;
        playerController.ResumeMovement();
    }

    public void FireShot()
    {
        if(weapon.weaponType != Weapon.WeaponType.Empty && !onCooldown && !isDying && canMove)
        {
            float cooldownTime;
            cooldownTime = 1 / weapon.firerate;
            StartCoroutine(ShotCooldown(cooldownTime));

            BulletSpawner bulletSpawner = weaponGameObject.GetComponent<BulletSpawner>();

            Debug.Log("Boom!");
            bulletSpawner.SpawnBullet(weapon.bulletSpeed, weapon.damage, friendlyFire);
        }
    }

    public void SetChestInRange(bool isInRange)
    {
        chestInRange = isInRange;
        if(chestInRange)
        {
            gameManager.ChestInRange();
        }
        else
        {
            gameManager.ChestNotInRange();
        }
    }

    public void InteractionPerformed()
    {
        if(!isDying && canMove && chestInRange)
        {
            gameManager.InteractAttempt();
        }
    }

    public void TakeDamage(float damage)
    {
        if(canMove)
        {
            health -= (int)damage;
            healthBar.SetHealth(health);

            if (health <= 0 && !isDying)
            {
                Die();
            }
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
        weaponHolderRight.GetComponent<LookAtConstraint>().enabled = false;

        Destroy(healthBar.gameObject);
        playerCollider.enabled = false;
        playerController.Die();
        gameManager.Die();
    }

    public void ChangeWeapon(Weapon newWeapon)
    {
        Debug.Log($"Setting weapon to {newWeapon}");

        weapon.SetWeapon(newWeapon.weaponName);
        playerController.ChangeWeaponType(newWeapon.weaponType);
        weaponGameObject = playerInventory.switchWeaponRightHand(newWeapon.weaponName);
    }

    public void IncreaseDamage(int percentage)
    {
        Debug.Log($"Increasing damage by {percentage}%");
        weapon.damage *= (1 + ((float)percentage / 100));
        Debug.Log($"Weapon damage = {weapon.damage}");
    }

    public void IncreaseFirerate(int percentage)
    {
        Debug.Log($"Increasing firerate by {percentage}%");
        weapon.firerate *= (1 + ((float)percentage / 100));
        Debug.Log($"Weapon firerate = {weapon.firerate}");
    }

    public void IncreaseBulletSpeed(int percentage)
    {
        Debug.Log($"Increasing bullet speed by {percentage}%");
        weapon.bulletSpeed *= (1 + ((float)percentage / 100));
        Debug.Log($"Weapon vullet speed = {weapon.bulletSpeed}");
    }

    public void IncreaseMaxHealth(int percentage)
    {
        Debug.Log($"Increasing max health by {percentage}%");
        float newHealth = maxHealth * (1 + ((float)percentage / 100));
        maxHealth = (int)newHealth;
        healthBar.SetMaxHealth(maxHealth);
        Debug.Log($"New health = {newHealth}, maxHealth = {maxHealth}");
    }

    public void IncreaseMovementSpeed(int percentage)
    {
        Debug.Log($"Increasing movement speed by {percentage}%");
        speed *= (1 + ((float)percentage / 100));
        Debug.Log($"Weapon movement speed = {speed}");
    }

    public void EscapePressed()
    {
        gameManager.EscapePressed();
    }

    private IEnumerator ShotCooldown(float cooldown)
    {
        onCooldown = true;
        yield return new WaitForSecondsRealtime(cooldown);
        onCooldown = false;
    }
}
