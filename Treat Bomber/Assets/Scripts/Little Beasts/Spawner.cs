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
        DecreaseSpawnTime();
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

    void DecreaseSpawnTime()
    {
        currentTime = Time.time;

        if (spawnTime >= 1.0f)
        {
            spawnTime -= currentTime/100000;
        }
    }

    void IncreaseSpawnChances()
    {
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
        }
        if (currentTime > 20)
        {
            robotChance = 0.4f;
            scarecrowChance = 0.3f;
            vampireChance = 0.2f;
        }
        if (currentTime > 30)
        {
            robotChance = 0.5f;
            scarecrowChance = 0.4f;
            vampireChance = 0.3f;
        }
    }

    void Spawn(Transform prefab)
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }
}
