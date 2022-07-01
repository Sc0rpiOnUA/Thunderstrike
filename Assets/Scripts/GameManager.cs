using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public int healthkitSmallHealth;

    public GameObject playerPrefab;
    public GameObject playerSpawner;
    public GameObject playerGameObject;
    public PlayerStatus playerStatus;

    public GameObject enemyContainer;
    public GameObject alienPrefab;
    public GameObject alienSpawner;
    public GameObject alienGameObject;
    public AlienStatus alienStatus;
    public AlienController alienController;

    public CinemachineVirtualCamera virtualCamera;

    

    private void Start()
    {
        StartTheGame();
    }

    private void StartTheGame()
    {
        playerGameObject = Instantiate(playerPrefab, playerSpawner.transform.position, playerSpawner.transform.rotation);       
        playerStatus = playerGameObject.GetComponent<PlayerStatus>();
        virtualCamera.Follow = playerGameObject.transform;
        virtualCamera.LookAt = playerGameObject.transform;

        alienGameObject = Instantiate(alienPrefab, alienSpawner.transform.position, alienSpawner.transform.rotation, enemyContainer.transform);
        alienStatus = alienGameObject.GetComponent<AlienStatus>();
        alienController = alienGameObject.GetComponent<AlienController>();
        alienController.SetPlayer(playerGameObject);

    }

    public void ItemPickup(Item item)
    {
        switch(item.itemName)
        {
            case Item.ItemName.Disarmer:
                {
                    playerStatus.ChangeWeapon(new Weapon(Weapon.WeaponName.Empty));
                    item.DestroyItem();
                    break;
                }
            case Item.ItemName.Deagle:
                {
                    playerStatus.ChangeWeapon(new Weapon(Weapon.WeaponName.Deagle));
                    item.DestroyItem();
                    break;
                }
            case Item.ItemName.Glock:
                {
                    playerStatus.ChangeWeapon(new Weapon(Weapon.WeaponName.Glock));
                    item.DestroyItem();
                    break;
                }
            case Item.ItemName.Uzi:
                {
                    playerStatus.ChangeWeapon(new Weapon(Weapon.WeaponName.Uzi));
                    item.DestroyItem();
                    break;
                }
            case Item.ItemName.Healthkit:
                {
                    playerStatus.IncreaseHealth(healthkitSmallHealth);
                    item.DestroyItem();
                    break;
                }
        }
    }
}
