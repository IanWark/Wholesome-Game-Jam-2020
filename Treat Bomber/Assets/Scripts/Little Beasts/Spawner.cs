using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Transform prefab = null;

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
            Instantiate(prefab, transform.position, transform.rotation);
            timeLeft = spawnTime;
        }
    }
}
