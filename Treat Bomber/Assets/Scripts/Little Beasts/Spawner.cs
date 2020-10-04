using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Transform zombiePrefab = null;

    [SerializeField]
    private Transform robotPrefab = null;

    [SerializeField]
    private Transform scarecrowPrefab = null;

    [SerializeField]
    private Transform vampirePrefab = null;

    [SerializeField]
    private float spawnTime = 5.0f;

    [SerializeField]
    private float timeLeft = 0;

    [SerializeField]
    private List<SpawnPoint> spawnPoints = null;

    private float currentTime;
    private float newSpawn;

    private float zombieChance = 1;
    private float robotChance = 0;
    private float scarecrowChance = 0;
    private float vampireChance = 0;

    // Update is called once per frame
    void Update()
    {
        AttemptSpawn();
        IncreaseSpawnChances();
    }

    void AttemptSpawn()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0.0f)
        {
            newSpawn = Random.Range(0, zombieChance + robotChance + scarecrowChance + vampireChance);

            if (newSpawn <= zombieChance)
            {
                Spawn(zombiePrefab);
            }
            else if (newSpawn <= zombieChance + robotChance)
            {
                Spawn(robotPrefab);
            }
            else if (newSpawn <= zombieChance + robotChance + scarecrowChance)
            {
                Spawn(scarecrowPrefab);
            }
            else if (newSpawn <= zombieChance + robotChance + scarecrowChance + vampireChance)
            {
                Spawn(vampirePrefab);
            }

            timeLeft = spawnTime;
        }
    }

    void IncreaseSpawnChances()
    {
        currentTime = Time.time;

        if (currentTime > 5)
        {
            robotChance = 0.1f;
        }
        if (currentTime > 10)
        {
            robotChance = 0.2f;
            scarecrowChance = 0.1f;
        }
        if (currentTime > 15)
        {
            robotChance = 0.3f;
            scarecrowChance = 0.2f;
            vampireChance = 0.1f;
            
            spawnTime = 4.5f;
        }
        if (currentTime > 20)
        {
            robotChance = 0.4f;
            scarecrowChance = 0.3f;
            vampireChance = 0.2f;

            spawnTime = 4.0f;
        }
        if (currentTime > 30)
        {
            robotChance = 0.5f;
            scarecrowChance = 0.4f;
            vampireChance = 0.3f;

            spawnTime = 3.0f;
        }
        if (currentTime > 45)
        {
            robotChance = 0.6f;
            scarecrowChance = 0.5f;
            vampireChance = 0.4f;

            spawnTime = 2.75f;
        }
        if (currentTime > 60)
        {
            robotChance = 0.7f;
            scarecrowChance = 0.6f;
            vampireChance = 0.5f;

            spawnTime = 2.5f;
        }
        if (currentTime > 75)
        {
            robotChance = 0.8f;
            scarecrowChance = 0.7f;
            vampireChance = 0.6f;

            spawnTime = 2.25f;
        }
        if (currentTime > 90)
        {
            robotChance = 0.8f;
            scarecrowChance = 0.7f;
            vampireChance = 0.6f;

            spawnTime = 2.0f;
        }
        if (currentTime > 105)
        {
            robotChance = 0.9f;
            scarecrowChance = 0.8f;
            vampireChance = 0.7f;

            spawnTime = 1.9f;
        }
        if (currentTime > 120)
        {
            robotChance = 1.0f;
            scarecrowChance = 0.9f;
            vampireChance = 0.8f;

            spawnTime = 1.8f;
        }
        if (currentTime > 135)
        {
            scarecrowChance = 1.0f;
            vampireChance = 0.9f;

            spawnTime = 1.7f;
        }
        if (currentTime > 135)
        {
            vampireChance = 1.0f;

            spawnTime = 1.6f;
        }
        if (currentTime > 150)
        {
            spawnTime = 1.5f;
        }
        if (currentTime > 165)
        {
            spawnTime = 1.4f;
        }
        if (currentTime > 180)
        {
            spawnTime = 1.3f;
        }
    }

    void Spawn(Transform prefab)
    {
        SpawnPoint spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

        Transform newBeast = Instantiate(prefab, spawnPoint.transform.position, spawnPoint.transform.rotation);

        newBeast.GetComponent<LittleBeast>().movement = spawnPoint.startMovement;
    }
}
