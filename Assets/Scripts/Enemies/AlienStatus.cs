using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AlienStatus : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public float characterSpeed;

    public int bulletDamage;
    public float bulletSpeed;
    public float firerate;
    public bool onCooldown;
    public bool friendlyFire;

    public GameObject weaponHolderRight;
    public GameObject blasterObject;
    public HealthBar healthBar;
    public AlienController alienController;
    public Collider alienCollider;

    private float cooldownTime;
    private bool isDying, isPlayerDead;
    private GameObject gameManagerObject;
    private GameManager gameManager;

    private void Awake()
    {
        alienController = GetComponent<AlienController>();
        alienCollider = GetComponent<Collider>();
        gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
        cooldownTime = 1 / firerate;
    }

    private void Start()
    {
        isDying = false;
        isPlayerDead = false;
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(health);
        StartCoroutine(ShotCooldown(cooldownTime));
    }

    public void FireShot()
    {
        if (!onCooldown && !isDying && !isPlayerDead)
        {
            StartCoroutine(ShotCooldown(cooldownTime));

            BulletSpawner bulletSpawner = blasterObject.GetComponent<BulletSpawner>();

            Debug.Log("Pew!");
            bulletSpawner.SpawnBullet(bulletSpeed, bulletDamage, friendlyFire);
        }
    }

    public void TakeDamage(int damage)
    {        
        health -= damage;
        healthBar.SetHealth(health);
        Debug.Log($"Alien {gameObject} is hit! Taking {damage} damage! {gameObject} health = {health}");

        if (health <= 0 && !isDying)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log($"Alien {gameObject} died!");
        isDying = true;
        weaponHolderRight.GetComponent<LookAtConstraint>().enabled = false;

        Destroy(healthBar.gameObject);
        alienCollider.enabled = false;
        alienController.Die();
        gameManager.EnemyDied(gameObject);
    }

    public void PlayerDied()
    {
        isPlayerDead = true;
    }

    private IEnumerator ShotCooldown(float cooldown)
    {
        onCooldown = true;
        yield return new WaitForSecondsRealtime(cooldown);
        onCooldown = false;
    }

}
