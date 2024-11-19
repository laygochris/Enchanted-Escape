using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject meteorPrefab; // Meteor prefab to spawn
    [SerializeField] private Transform[] spawnPoints; // Array of possible spawn points
    [SerializeField] private float minSpawnTime = 3f; // Minimum spawn time (3 seconds)
    [SerializeField] private float maxSpawnTime = 9f; // Maximum spawn time (9 seconds)
    [SerializeField] private int maxMeteors = 10; // Max number of meteors that can be spawned
    [SerializeField] private float spawnAcceleration = 0.95f; // Rate at which spawn time decreases over time

    private float currentSpawnTime; // Current spawn interval
    private int activeMeteors = 0;  // Track the current number of meteors

    void Start()
    {
        currentSpawnTime = maxSpawnTime; // Start with max spawn time
        Invoke(nameof(SpawnMeteor), currentSpawnTime); // Start invoking spawn method
    }

    void SpawnMeteor()
    {
        if (activeMeteors < maxMeteors)
        {
            // Randomly pick a spawn point
            int spawnIndex = Random.Range(0, spawnPoints.Length);

            Instantiate(meteorPrefab, spawnPoints[spawnIndex].position, Quaternion.identity).transform.localEulerAngles = Vector3.forward*-135;

            activeMeteors++; // Increment the active meteor count

            // Gradually decrease spawn time to make meteors spawn quicker
            currentSpawnTime = Mathf.Max(minSpawnTime, currentSpawnTime * spawnAcceleration);

            // Adjust the time for the next spawn using Invoke
            CancelInvoke(nameof(SpawnMeteor));
            Invoke(nameof(SpawnMeteor), currentSpawnTime);
        }
    }

    public void MeteorDestroyed()
    {
        activeMeteors--; // Decrease active meteors when one is destroyed
    }
}
