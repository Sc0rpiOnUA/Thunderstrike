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

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        
    }

    public void FireShot()
    {
        if (!onCooldown)
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
        Debug.Log($"Alien {gameObject} is hit! Taking {damage} damage! {gameObject} health = {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log($"Alien {gameObject} died!");
        Destroy(gameObject);
    }

    private IEnumerator ShotCooldown(float cooldown)
    {
        onCooldown = true;
        yield return new WaitForSecondsRealtime(cooldown);
        onCooldown = false;
    }

}
