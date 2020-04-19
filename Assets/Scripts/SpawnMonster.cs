using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    public GameObject monster;
    public GameObject player;
    public float spawnCooldown = 1f;
    public float amountToSpawn = 1f;
    public float spawnDistanceMin = 0f;
    public float maxAmount = 10f;

    public Rect boundary;

    // TODO get this to read from the gameObject rather than the number spawned
    // when player can kill monsters
    private float totalSpawned = 0f;

    void Start()
    {
        boundary = gameObject.GetComponentInChildren<ScreenLayout>().OuterArea;
        InvokeRepeating("Spawn", 0, spawnCooldown);
    }

    void Spawn()
    {
        if (!PersistentManager.Instance.Running)
        {
            return;
        }

        if (totalSpawned >= maxAmount)
        {
            return;
        }

        int amountSpawned = 0;
        while (amountSpawned < amountToSpawn)
        {
            Vector2 spawnPos = calculatePosition();
            GameObject spawnedMonster = Instantiate(monster, spawnPos, Quaternion.identity);
            spawnedMonster.GetComponent<MonsterMove>().playerLoc = player.transform;

            amountSpawned++;
        }

        totalSpawned += amountSpawned;
    }

    Vector2 calculatePosition()
    {
        Vector2 spawnPosition;
        float r = Random.value;
        if (r > 0.5)
        {
            spawnPosition.x = Random.Range(boundary.xMin, boundary.xMax);
            spawnPosition.y = player.transform.position.y > 0 ? boundary.yMin : boundary.yMax;
        }
        else
        {
            spawnPosition.y = Random.Range(boundary.yMin, boundary.yMax);
            spawnPosition.x = player.transform.position.x > 0 ? boundary.xMin : boundary.xMax;
        }
        return spawnPosition;
    }
}