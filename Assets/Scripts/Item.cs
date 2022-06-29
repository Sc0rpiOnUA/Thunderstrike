using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject gameManagerObject;
    public GameManager gameManager;
    public enum ItemName { Deagle, Glock, Uzi, Healthkit, Disarmer};
    public ItemName item;
    public Collider itemCollider;

    private void Start()
    {
        gameManagerObject = GameObject.FindWithTag("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
        itemCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider otherCollider = other;
        string otherColliderTag = otherCollider.gameObject.tag;

        if (otherColliderTag == "Player")
        {
            gameManager.ItemPickup(item);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
