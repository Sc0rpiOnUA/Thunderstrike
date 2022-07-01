using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public int healthkitSmallHealth;
    public int enemiesToGenerate;

    public GameObject playerPrefab;
    public GameObject playerSpawner;
    public GameObject playerGameObject;
    public PlayerStatus playerStatus;

    public GameObject enemyContainer;
    public GameObject alienPrefab;
    public GameObject alienSpawner;
    public List<GameObject> alienObjectList;
    public List<AlienStatus> alienStatusList;
    public List<AlienController> alienControllerList;

    public CinemachineVirtualCamera virtualCamera;

    private EnemySpawner spawner;

    private void Awake()
    {
        spawner = alienSpawner.GetComponent<EnemySpawner>();
    }

    private void Start()
    {
        StartTheGame();
    }

    private void Update()
    {
        if(enemiesToGenerate > 0)
        {
            GenerateEnemy();
            enemiesToGenerate--;
        }
    }

    private void StartTheGame()
    {
        playerGameObject = Instantiate(playerPrefab, playerSpawner.transform.position, playerSpawner.transform.rotation);       
        playerStatus = playerGameObject.GetComponent<PlayerStatus>();
        virtualCamera.Follow = playerGameObject.transform;
        virtualCamera.LookAt = playerGameObject.transform;
    }

    public void GenerateEnemy()
    {
        alienObjectList.Add(spawner.SpawnEnemy(alienPrefab));
        alienStatusList.Add(alienObjectList[^1].GetComponent<AlienStatus>());
        alienControllerList.Add(alienObjectList[^1].GetComponent<AlienController>());
        alienControllerList[^1].SetPlayer(playerGameObject);
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
