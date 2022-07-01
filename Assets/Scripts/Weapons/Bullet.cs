using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;

    private void Awake()
    {
        damage = 0;
    }

    public void DestroyAfterTime(float seconds)
    {
        StartCoroutine(DestroyAfterTimeCoroutine(seconds));
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        Collider otherCollider = collision.GetContact(0).otherCollider;
        string otherColliderTag = otherCollider.gameObject.tag;

        Debug.Log($"Bullet hit {otherCollider}, the tag is {otherColliderTag}");

        if (otherColliderTag == "Environment")
        {
            Destroy(gameObject);
        }
        else if(otherColliderTag == "Player")
        {
            Debug.Log($"Player is taking {damage} damage from a bullet!");
            otherCollider.gameObject.GetComponent<PlayerStatus>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (otherColliderTag == "Enemy")
        {
            Debug.Log($"Enemy is taking {damage} damage from a bullet!");
            otherCollider.gameObject.GetComponent<AlienStatus>().TakeDamage(damage);            
            Destroy(gameObject);
        }
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;

        Debug.Log($"Setting bullet damage to {newDamage}. Damage = {damage}");
    }

    private IEnumerator DestroyAfterTimeCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

}
