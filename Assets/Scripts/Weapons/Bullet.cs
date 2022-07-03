using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool friendlyFire;
    private float damage;
    public AudioClip[] shotSounds;
    private AudioSource audioSource;
    private Collider selfCollider;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        selfCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();

        damage = 0;
        if(tag == "PlayerBullet")
        {
            selfCollider.isTrigger = false;
        }
        else if (tag == "EnemyBullet")
        {
            selfCollider.isTrigger = true;
        }

        PlayShotSound();
    }

    public void DestroyAfterTime(float seconds)
    {
        StartCoroutine(DestroyAfterTimeCoroutine(seconds));
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        string otherColliderTag = otherCollider.gameObject.tag;
        ResolveCollision(otherColliderTag, otherCollider);

        Debug.Log($"Bullet was triggered by {otherCollider}, the tag is {otherColliderTag}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        Collider otherCollider = collision.GetContact(0).otherCollider;
        string otherColliderTag = otherCollider.gameObject.tag;
        ResolveCollision(otherColliderTag, otherCollider);

        Debug.Log($"Bullet hit {otherCollider}, the tag is {otherColliderTag}");
   
    }

    private void PlayShotSound()
    {
        int length = shotSounds.Length;
        if (length > 0)
        {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(shotSounds[Random.Range(0, length)]);
        }
    }

    private void ResolveCollision(string otherColliderTag, Collider otherCollider)
    {
        if (otherColliderTag == "Environment")
        {
            OnHitDestroy();
        }
        else if (otherColliderTag == "Player")
        {
            Debug.Log($"Player is taking {damage} damage from a bullet!");
            otherCollider.gameObject.GetComponent<PlayerStatus>().TakeDamage(damage);

            OnHitDestroy();
        }
        else if (otherColliderTag == "Enemy" && (tag == "PlayerBullet" || friendlyFire))
        {
            Debug.Log($"Enemy is taking {damage} damage from a bullet!");
            otherCollider.gameObject.GetComponent<AlienStatus>().TakeDamage(damage);
            OnHitDestroy();
        }
    }

    public void SetDamage(float newDamage)
    {
        damage = newDamage;

        Debug.Log($"Setting bullet damage to {newDamage}. Damage = {damage}");
    }

    private void OnHitDestroy()
    {
        selfCollider.enabled = false;
        meshRenderer.enabled = false;

        StartCoroutine(DestroyAfterTimeCoroutine(2f));
    }

    private IEnumerator DestroyAfterTimeCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

}
