using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform robotPrefab = null;

    float spawnTime = 5.0f;
    float timeLeft = 0;

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0.0f)
        {
            Instantiate(robotPrefab, transform.position, transform.rotation);
            timeLeft = spawnTime;
        }
    }
}
