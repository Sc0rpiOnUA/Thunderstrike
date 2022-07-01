using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienStatus : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public float characterSpeed;

    public int bulletDamage;
    public float bulletSpeed;
    public float firerate;
    public bool onCooldown;

    public GameObject blasterObject;
    public HealthBar healthBar;
    public AlienController alienController;
    public Collider alienCollider;

    private bool isDying;

    private void Awake()
    {
        alienController = GetComponent<AlienController>();
        alienCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        isDying = false;
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(health);
    }

    public void FireShot()
    {
        if (!onCooldown && !isDying)
        {
            float cooldownTime;
            cooldownTime = 1 / firerate;
            StartCoroutine(ShotCooldown(cooldownTime));

            BulletSpawner bulletSpawner = blasterObject.GetComponent<BulletSpawner>();

            Debug.Log("Pew!");
            bulletSpawner.SpawnBullet(bulletSpeed, bulletDamage);
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

        Destroy(healthBar.gameObject);
        alienCollider.enabled = false;
        alienController.Die();
        //Destroy(gameObject);
    }

    private IEnumerator ShotCooldown(float cooldown)
    {
        onCooldown = true;
        yield return new WaitForSecondsRealtime(cooldown);
        onCooldown = false;
    }

}
