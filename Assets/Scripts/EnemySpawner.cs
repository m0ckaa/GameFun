using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject enemyPrefab; // Drag your enemy prefab here in the Inspector
    public Transform[] spawnPoints; // Array to hold spawn points
    public float spawnInterval = 1f; // Time interval between spawns
    public float spawnDuration = 10f; // Total duration to spawn enemies

    private float timer = 0f; // Timer to keep track of elapsed time

    void Start()
    {
        // Optionally, find spawn points dynamically if not set in the Inspector
        if (spawnPoints.Length == 0)
        {
            spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint").Select(obj => obj.transform).ToArray();
        }

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        float elapsedTime = 0f;

        while (elapsedTime < spawnDuration)
        {
            // Spawn an enemy at a random spawn point
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);

            // Wait for the next spawn
            yield return new WaitForSeconds(spawnInterval);
            elapsedTime += spawnInterval;
        }
    }
}
