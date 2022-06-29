using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject playerSpawner;

    public GameObject playerGameObject;
    public PlayerStatus playerStatus;
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
        
    }

    public void ItemPickup(Item.ItemName itemName)
    {
        switch(itemName)
        {
            case Item.ItemName.Disarmer:
                {
                    playerStatus.ChangeWeapon(Weapon.WeaponName.Empty);
                    break;
                }
            case Item.ItemName.Deagle:
                {
                    playerStatus.ChangeWeapon(Weapon.WeaponName.Deagle);
                    break;
                }
            case Item.ItemName.Glock:
                {
                    playerStatus.ChangeWeapon(Weapon.WeaponName.Glock);
                    break;
                }
            case Item.ItemName.Uzi:
                {
                    playerStatus.ChangeWeapon(Weapon.WeaponName.Uzi);
                    break;
                }
        }
    }
}
