using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject gameManagerObject;
    public GameManager gameManager;
    public Item item;
    public enum ItemName { Deagle, Glock, Uzi, Healthkit, Disarmer};
    public ItemName itemName;
    public Collider itemCollider;

    private void Start()
    {
        item = gameObject.GetComponent<Item>();

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
            Debug.Log("Item collided with a player!");
            gameManager.ItemPickup(item);
        }
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
