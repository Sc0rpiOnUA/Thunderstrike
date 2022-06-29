using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject playerSpawner;
    public CinemachineVirtualCamera virtualCamera;

    public GameObject playerGameObject;
    public PlayerStatus playerStatus;    

    private void Start()
    {
        StartTheGame();
    }

    private void StartTheGame()
    {
        playerGameObject = Instantiate(playerPrefab, playerSpawner.transform.position, playerSpawner.transform.rotation);
        virtualCamera.Follow = playerGameObject.transform;
        virtualCamera.LookAt = playerGameObject.transform;

        playerStatus = playerGameObject.GetComponent<PlayerStatus>();          
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
        }
    }
}
