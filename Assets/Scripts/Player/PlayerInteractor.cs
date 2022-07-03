using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private Collider interactorCollider;
    private PlayerStatus playerStatus;    

    private void Awake()
    {
        interactorCollider = GetComponent<Collider>();
        playerStatus = GetComponent<PlayerStatus>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Chest")
        {
            playerStatus.SetChestInRange(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Chest")
        {
            playerStatus.SetChestInRange(false);
        }
    }
}
