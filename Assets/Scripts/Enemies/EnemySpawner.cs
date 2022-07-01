using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float halfSizeX;
    public float halfSizeZ;
    public GameObject enemyContainer;

    private MeshRenderer meshRenderer;


    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        halfSizeX = meshRenderer.bounds.size.x / 2;
        halfSizeZ = meshRenderer.bounds.size.z / 2;
    }

    public GameObject SpawnEnemy(GameObject enemyPrefab)
    {
        return Instantiate(enemyPrefab, GeneratePosition(), gameObject.transform.rotation, enemyContainer.transform);
    }

    private Vector3 GeneratePosition()
    {
        return new Vector3(Random.Range(-halfSizeX, halfSizeX), gameObject.transform.position.y, Random.Range(-halfSizeZ, halfSizeZ));
    }
}
