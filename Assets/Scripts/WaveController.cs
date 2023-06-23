using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public List<EnemySpawner> enemySpawners; // Lista de todos los objetos con el script "EnemySpawner"
    public Transform player; // Referencia al transform del jugador
    public float initialSpawnDelay = 60f; // Retraso inicial para generar la primera horda
    public float spawnDelayDecrease = 5f; // Reducción de tiempo entre hordas
    public float minSpawnDelay = 10f; // Retraso mínimo entre hordas
    public float maxSpawnDistance = 50f; // Distancia máxima entre el spawner y el jugador para spawnear enemigos
    public int enemiesPerRoundMin = 20; // Cantidad total de enemigos por ronda
    public int enemiesPerRoundMax = 25;
    public int enemiesPerRound;
    public float timeRemaining;

    private float currentSpawnDelay; // Retraso actual para generar la siguiente horda

    public TextMeshProUGUI spawnDelayText; // Referencia al componente TextMeshProUGUI del texto a modificar

    private void Start()
    {
        currentSpawnDelay = initialSpawnDelay;
        InitialSpawn();
        StartCoroutine(SpawnEnemiesCoroutine());
        enemiesPerRound = Random.Range(enemiesPerRoundMin, enemiesPerRoundMax); 
        Debug.Log(enemiesPerRound);

        float timeRemaining = currentSpawnDelay;
    }

    private void Update()
    {
        enemiesPerRound = Random.Range(enemiesPerRoundMin, enemiesPerRoundMax);
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        while (true)
        {
            timeRemaining = currentSpawnDelay;

            while (timeRemaining > 0f)
            {
                yield return null;
                timeRemaining -= Time.deltaTime;

                // Actualizar el texto con el tiempo restante
                spawnDelayText.text = timeRemaining.ToString("F1");
            }

            int spawnerCount = enemySpawners.Count;
            int baseEnemiesPerSpawner = enemiesPerRound / spawnerCount;
            int remainingEnemies = enemiesPerRound % spawnerCount;

            List<int> spawnerIndices = Enumerable.Range(0, spawnerCount).ToList();

            foreach (EnemySpawner spawner in enemySpawners)
            {
                int enemiesToSpawn = baseEnemiesPerSpawner;

                if (remainingEnemies > 0)
                {
                    enemiesToSpawn++;
                    remainingEnemies--;
                }

                spawner.SpawnEnemy(enemiesToSpawn, 0.2f);
            }

            // Barajar aleatoriamente la lista de índices de spawners
            spawnerIndices = spawnerIndices.OrderBy(x => Random.value).ToList();

            foreach (int index in spawnerIndices)
            {
                if (remainingEnemies > 0)
                {
                    enemySpawners[index].SpawnEnemy(1, 0.2f);
                    remainingEnemies--;
                }
                else
                {
                    break;
                }
            }

            // Actualizar el texto con el valor de currentSpawnDelay
            spawnDelayText.text = currentSpawnDelay.ToString();

            // Reducir el retraso para la siguiente horda, pero asegurarse de que no sea menor que el mínimo
            currentSpawnDelay -= spawnDelayDecrease;
            currentSpawnDelay = Mathf.Max(currentSpawnDelay, minSpawnDelay);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, maxSpawnDistance);
    }

    public void InitialSpawn()
    {
        int enemiesPerRound = Random.Range(enemiesPerRoundMin, enemiesPerRoundMax);
        int spawnerCount = enemySpawners.Count;
        int baseEnemiesPerSpawner = enemiesPerRound / spawnerCount;
        int remainingEnemies = enemiesPerRound % spawnerCount;

        List<int> spawnerIndices = Enumerable.Range(0, spawnerCount).ToList();

        foreach (EnemySpawner spawner in enemySpawners)
        {
            int enemiesToSpawn = baseEnemiesPerSpawner;

            if (remainingEnemies > 0)
            {
                enemiesToSpawn++;
                remainingEnemies--;
            }

            spawner.SpawnEnemy(enemiesToSpawn, 0.2f);
        }

        // Barajar aleatoriamente la lista de índices de spawners
        spawnerIndices = spawnerIndices.OrderBy(x => Random.value).ToList();

        foreach (int index in spawnerIndices)
        {
            if (remainingEnemies > 0)
            {
                enemySpawners[index].SpawnEnemy(1, 0.2f);
                remainingEnemies--;
            }
            else
            {
                break;
            }
        }
    }

    public void ForceWave()
    {
        int enemiesPerRound = Random.Range(enemiesPerRoundMin, enemiesPerRoundMax);
        int spawnerCount = enemySpawners.Count;
        int baseEnemiesPerSpawner = enemiesPerRound / spawnerCount;
        int remainingEnemies = enemiesPerRound % spawnerCount;

        List<int> spawnerIndices = Enumerable.Range(0, spawnerCount).ToList();

        foreach (EnemySpawner spawner in enemySpawners)
        {
            int enemiesToSpawn = baseEnemiesPerSpawner;

            if (remainingEnemies > 0)
            {
                enemiesToSpawn++;
                remainingEnemies--;
            }

            spawner.SpawnEnemy(enemiesToSpawn, 0.2f);
        }

        // Barajar aleatoriamente la lista de índices de spawners
        spawnerIndices = spawnerIndices.OrderBy(x => Random.value).ToList();

        foreach (int index in spawnerIndices)
        {
            if (remainingEnemies > 0)
            {
                enemySpawners[index].SpawnEnemy(1, 0.2f);
                remainingEnemies--;
            }
            else
            {
                break;
            }
        }
        currentSpawnDelay = Mathf.Max(currentSpawnDelay, minSpawnDelay);
        timeRemaining = currentSpawnDelay;
    }
}
