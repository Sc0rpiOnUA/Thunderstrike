using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int healthkitSmallHealth;
    public int healthkitChancePercentage;
    public int enemiesToSpawn;
    public int enemiesAlive;
    public int stageNumber;
    public bool friendlyFire;

    [Header("Text UI")]
    public GameObject enemiesAliveContainer;
    public TextMeshProUGUI enemiesAliveText;
    public TextMeshProUGUI stageNumberText;

    [Header("Player")]
    public GameObject playerPrefab;
    public GameObject playerSpawner;
    public GameObject playerGameObject;
    public PlayerStatus playerStatus;

    [Header("Enemies")]
    public GameObject enemyContainer;
    public GameObject alienPrefab;
    public GameObject alienSpawner;
    public List<GameObject> alienObjectList;
    public List<AlienStatus> alienStatusList;
    public List<AlienController> alienControllerList;

    [Header("Items")]
    public GameObject itemContainer;
    public GameObject healthKitSmallPrefab;

    [Header("Misc")]
    public CinemachineVirtualCamera virtualCamera;
    public AudioClip[] music;    
    public Canvas cardCanvas;
    public GameObject weaponCardTemplate;

    private AudioSource audioSource;
    private EnemySpawner spawner;
    private Color enemiesLeftColor, stageColor;

    public enum GameState { Empty, InChestRange, Fighting, Selecting, Idling, Preparing, Dead}
    public GameState gameState;

    public Chest chest;
    private bool isChestActive;

    private void Awake()
    {
        spawner = alienSpawner.GetComponent<EnemySpawner>();
        audioSource = GetComponent<AudioSource>();
        isChestActive = true;
        enemiesLeftColor = enemiesAliveText.color;
        stageColor = stageNumberText.color;
    }

    private void Start()
    {
        StartTheGame();
    }

    private void Update()
    {        
        
        DisplayTextUI();

        if(!audioSource.isPlaying)
        {
            PlayRandomMusic();
        }

        if (gameState == GameState.Fighting && enemiesAlive == 0)
        {
            gameState = GameState.Idling;

            ActivateChest();
            enemiesToSpawn += 1;
        }
    }

    private void StartTheGame()
    {
        gameState = GameState.Empty;
        stageNumber = 0;

        playerGameObject = Instantiate(playerPrefab, playerSpawner.transform.position, playerSpawner.transform.rotation);       
        playerStatus = playerGameObject.GetComponent<PlayerStatus>();
        playerStatus.friendlyFire = friendlyFire;

        virtualCamera.Follow = playerGameObject.transform;
        virtualCamera.LookAt = playerGameObject.transform;

        ActivateChest();
    }

    private void PlayRandomMusic()
    {
        {
            int length = music.Length;
            if (length > 0)
            {
                audioSource.PlayOneShot(music[Random.Range(0, length)]);
            }
        }
    }

    private void DisplayTextUI()
    {
        enemiesAliveText.text = enemiesAlive.ToString();

        switch (gameState)
        {
            case GameState.Empty:
                {
                    stageNumberText.text = "Get to the chest!";
                    stageNumberText.color = stageColor;
                    enemiesAliveContainer.SetActive(false);
                    break;
                }
            case GameState.Idling:
                {
                    stageNumberText.text = "Get to the chest!";
                    stageNumberText.color = stageColor;
                    enemiesAliveContainer.SetActive(false);
                    break;
                }
            case GameState.Fighting:
                {
                    stageNumberText.text = $"Stage: {stageNumber}";
                    stageNumberText.color = enemiesLeftColor;
                    enemiesAliveContainer.SetActive(true);
                    break;
                }
            case GameState.Preparing:
                {
                    stageNumberText.text = "Prepare for battle!";
                    stageNumberText.color = enemiesLeftColor;
                    enemiesAliveContainer.SetActive(false);
                    break;
                }
            case GameState.InChestRange:
                {
                    stageNumberText.text = "Press 'E'";
                    stageNumberText.color = Color.cyan;
                    enemiesAliveContainer.SetActive(false);
                    break;
                }
            case GameState.Selecting:
                {
                    stageNumberText.text = "Select a card!";
                    stageNumberText.color = Color.cyan;
                    enemiesAliveContainer.SetActive(false);
                    break;
                }
            case GameState.Dead:
                {
                    stageNumberText.text = $"Game over! Stage: {stageNumber}";
                    stageNumberText.color = enemiesLeftColor;
                    enemiesAliveContainer.SetActive(false);
                    break;
                }
        }
    }

    public void GenerateEnemy()
    {
        alienObjectList.Add(spawner.SpawnEnemy(alienPrefab));
        alienStatusList.Add(alienObjectList[^1].GetComponent<AlienStatus>());
        alienControllerList.Add(alienObjectList[^1].GetComponent<AlienController>());
        alienControllerList[^1].SetPlayer(playerGameObject);

        alienStatusList[^1].friendlyFire = friendlyFire;
        enemiesAlive += 1;
    }

    public void EnemyDied(GameObject enemy)
    {
        enemiesAlive -= 1;

        int chance = Random.Range(1, 100);
        if(chance <= healthkitChancePercentage)
        {
            SpawnHealthKit(enemy.transform);
        }

        StartCoroutine(DestroyEnemy(enemy, 10f));
    }

    public void SpawnHealthKit(Transform enemyTransform)
    {
        Instantiate(healthKitSmallPrefab, enemyTransform.position, enemyTransform.rotation, itemContainer.transform);
    }

    public void ItemPickup(Item item)
    {
        switch(item.itemName)
        {            
            case Item.ItemName.Healthkit:
                {
                    playerStatus.IncreaseHealth(healthkitSmallHealth);
                    item.DestroyItem();
                    break;
                }
        }
    }
    public void ChestInRange()
    {
        if(gameState == GameState.Empty || gameState == GameState.Idling)
        {
            gameState = GameState.InChestRange;
        }
    }
    public void ChestNotInRange()
    {
        if(gameState == GameState.InChestRange && stageNumber == 0)
        {
            gameState = GameState.Empty;
        }
        else if(gameState == GameState.InChestRange)
        {
            gameState = GameState.Idling;
        }
    }

    public void InteractAttempt()
    {
        if (isChestActive)
        {
            OpenChest();
        }
    }

    public void ActivateChest()
    {
        isChestActive = true;
        chest.ActivateChest();
    }

    public void DeactivateChest()
    {
        isChestActive = false;
        chest.DeactivateChest();
    }

    public void OpenChest()
    {
        if (stageNumber == 0)
        {
            OpenWeaponsChest();
        }
        else 
        {
            OpenWeaponsChest();
        }
        gameState = GameState.Selecting;
    }

    public void OpenWeaponsChest()
    {
        playerStatus.StopMovement();
        GenerateWeaponCard(Weapon.WeaponName.Deagle, CardInteractor.CardPosition.Left);
        GenerateWeaponCard(Weapon.WeaponName.Glock, CardInteractor.CardPosition.Middle);
        GenerateWeaponCard(Weapon.WeaponName.Uzi, CardInteractor.CardPosition.Right);
        DeactivateChest();
    }

    public void GenerateWeaponCard(Weapon.WeaponName generatedWeaponName, CardInteractor.CardPosition cardPosition)
    {
        GameObject newCard = Instantiate(weaponCardTemplate, cardCanvas.transform);
        WeaponCardDisplay weaponCardDisplay = newCard.GetComponent<WeaponCardDisplay>();

        Debug.Log($"Generating Weapon Card! Weapon name = {generatedWeaponName}, position = {cardPosition}");
        weaponCardDisplay.SetCard(generatedWeaponName, cardPosition);
    }

    public void GenerateWeaponCard(Weapon.WeaponName weaponName, int positionIndex)
    {
        GameObject newCard = Instantiate(weaponCardTemplate, cardCanvas.transform);
        WeaponCardDisplay weaponCardDisplay = newCard.GetComponent<WeaponCardDisplay>();

        weaponCardDisplay.SetCard(weaponName, positionIndex);
    }

    public void SelectCard(CardInteractor.CardType cardType, GameObject selectedCard)
    {
        CardInteractor interactor;

        Debug.Log($"Card {selectedCard} received! Iterating...");

        foreach (Transform child in cardCanvas.transform)
        {
            interactor = child.gameObject.GetComponent<CardInteractor>();
            
            if(child.gameObject == selectedCard)
            {
                interactor.SelectCard();
                StartCoroutine(DestroySelectedCard(interactor, 2f));
                EquipCard(selectedCard);
            }
            else
            {
                interactor.DiscardCard();
            }
        }
    }

    public void Die()
    {
        gameState = GameState.Dead;

        foreach(AlienStatus alienStatus in alienStatusList)
        {
            alienStatus.PlayerDied();
        }

        StartCoroutine(DeathCoroutine(5f));
    }

    private void EquipCard(GameObject selectedCard)
    {
        CardInteractor interactor = selectedCard.GetComponent<CardInteractor>();

        if(interactor.cardType == CardInteractor.CardType.Weapon)
        {
            WeaponCardDisplay weaponCardDisplay = interactor.gameObject.GetComponent<WeaponCardDisplay>();
            Weapon.WeaponName newWeaponName = weaponCardDisplay.weaponName;

            playerStatus.ChangeWeapon(new Weapon(newWeaponName));
        }

        Fight(3f, enemiesToSpawn);
    }

    private void Fight(float delay, int enemies)
    {
        gameState = GameState.Preparing;
        StartCoroutine(FightCoroutine(delay, enemies));
    }

    private IEnumerator DeathCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);


        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator FightCoroutine(float delay, int enemies)
    { 
        yield return new WaitForSeconds(delay);        
        for (int i = 0; i < enemies; i++)
        {
            GenerateEnemy();
        }
        gameState = GameState.Fighting;
        stageNumber += 1;
    }

    private IEnumerator DestroyEnemy(GameObject enemy, float timeToDestroy)
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(enemy);
    }

    private IEnumerator DestroySelectedCard(CardInteractor selectedCardInteractor, float timeToDestroy)
    {
        yield return new WaitForSeconds(timeToDestroy);
        selectedCardInteractor.EquipCard();
        playerStatus.ResumeMovement();
    }
}
