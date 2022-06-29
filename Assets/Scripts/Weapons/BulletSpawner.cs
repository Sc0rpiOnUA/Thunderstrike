using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletSpawnerObject;
    public GameObject bulletPrefab;
    public GameObject bulletContainer;

    private void Start()
    {
        bulletContainer = GameObject.FindGameObjectWithTag("BulletContainer");
    }
    public void SpawnBullet(float bulletSpeed)
    {
        Debug.Log($"Bullet instantiated! Type of bullet: {bulletPrefab}");
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnerObject.transform.position, bulletSpawnerObject.transform.rotation, bulletContainer.transform);
        Bullet bulletController = bullet.GetComponent<Bullet>();
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        Vector3 force = new Vector3(0, bulletSpeed, 0);

        bulletController.DestroyAfterTime(3);
        bulletRigidbody.AddRelativeForce(force, ForceMode.VelocityChange);
    }
}
