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
    public AudioClip[] audioClips;
    public MeshRenderer meshRenderer;

    private AudioSource audioSource;
    private Collider selfCollider;
    

    private void Awake()
    {
        selfCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        item = gameObject.GetComponent<Item>();

        gameManagerObject = GameObject.FindWithTag("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
        selfCollider = GetComponent<Collider>();
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
        selfCollider.enabled = false;
        meshRenderer.enabled = false;

        PlayPickupSound();
        StartCoroutine(DestroyAfterTimeCoroutine(2f));
    }

    private void PlayPickupSound()
    {
        int length = audioClips.Length;
        if (length > 0)
        {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(audioClips[Random.Range(0, length)]);
        }
    }

    private IEnumerator DestroyAfterTimeCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
