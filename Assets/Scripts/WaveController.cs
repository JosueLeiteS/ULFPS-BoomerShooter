using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveController : MonoBehaviour
{
    public List<EnemySpawner> enemySpawners; // Lista de todos los objetos con el script "EnemySpawner"
    public Transform player; // Referencia al transform del jugador
    public float initialSpawnDelay = 60f; // Retraso inicial para generar la primera horda
    public float spawnDelayDecrease = 5f; // Reducción de tiempo entre hordas
    public float minSpawnDelay = 10f; // Retraso mínimo entre hordas
    public float maxSpawnDistance = 50f; // Distancia máxima entre el spawner y el jugador para spawnear enemigos

    private float currentSpawnDelay; // Retraso actual para generar la siguiente horda

    public TextMeshProUGUI spawnDelayText;

    private void Start()
    {
        currentSpawnDelay = initialSpawnDelay;
        Test();
        StartCoroutine(SpawnEnemiesCoroutine());
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        while (true)
        {
            float timeRemaining = currentSpawnDelay;

            while (timeRemaining > 0f)
            {
                yield return null;
                timeRemaining -= Time.deltaTime;

                // Actualizar el texto con el tiempo restante
                spawnDelayText.text = timeRemaining.ToString("F1");
            }

            foreach (EnemySpawner spawner in enemySpawners)
            {
                if (Vector3.Distance(spawner.transform.position, player.position) < maxSpawnDistance)
                {
                    spawner.SpawnEnemy();
                }
            }

            // Actualizar el texto con el valor de currentSpawnDelay
            spawnDelayText.text = currentSpawnDelay.ToString();

            // Reducir el retraso para la siguiente horda, pero asegurarse de que no sea menor que el mínimo
            currentSpawnDelay -= spawnDelayDecrease;
            currentSpawnDelay = Mathf.Max(currentSpawnDelay, minSpawnDelay);
        }
    }

    void Test()
    {
        foreach (EnemySpawner spawner in enemySpawners)
        {
            if (Vector3.Distance(spawner.transform.position, player.position) < maxSpawnDistance)
            {
                spawner.SpawnEnemy();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(player.position, maxSpawnDistance);
    }
}
