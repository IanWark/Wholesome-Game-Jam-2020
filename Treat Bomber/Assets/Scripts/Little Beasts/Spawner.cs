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

    // Update is called once per frame
    void Update()
    {
        AttemptSpawn();
        DecreaseSpawnTime();
    }

    void AttemptSpawn()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0.0f)
        {
            Spawn(zombiePrefab);

            timeLeft = spawnTime;
        }
    }

    void DecreaseSpawnTime()
    {
        currentTime = Time.time;

        if (spawnTime >= 1.0f)
        {
            spawnTime -= currentTime/100000;
            Debug.Log("Spawn time: " + spawnTime);
        }
    }

    void Spawn(Transform prefab)
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }
}
