﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform zombiePrefab = null;

    float spawnTime = 5.0f;
    float timeLeft = 0;

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0.0f)
        {
            Instantiate(zombiePrefab, transform);
            timeLeft = spawnTime;
        }
    }
}
