using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefab; // Prefab del enemigo a spawnear

    public void SpawnEnemy(int n, float currentSpawnDelay)
    {
        //Instantiate(enemyPrefab[Random.Range(0,enemyPrefab.Length)], transform.position, transform.rotation);
        StartCoroutine(SpawnEnemiesCoroutine(n, currentSpawnDelay));
    }

    private IEnumerator SpawnEnemiesCoroutine(int n, float currentSpawnDelay)
    {
        for(int i = 0; i < n; i++)
        {
            Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], transform.position, transform.rotation);
            yield return new WaitForSeconds(currentSpawnDelay);
        }
    }
}