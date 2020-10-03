using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform scarecrowPrefab = null;

    [SerializeField]
    float spawnTime = 5.0f;
    [SerializeField]
    float timeLeft = 0;

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0.0f)
        {
            Instantiate(scarecrowPrefab, transform.position, transform.rotation);
            timeLeft = spawnTime;
        }
    }
}
