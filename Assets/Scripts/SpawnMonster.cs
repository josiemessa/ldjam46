using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SpawnMonster : MonoBehaviour
{
    public GameObject monster;
    public GameObject player;
    public float spawnCooldown = 1f;
    public int amountToSpawn = 1;
    public int maxAmount = 10;

    // TODO get this to read from the gameObject rather than the number spawned
    // when player can kill monsters
    private int totalSpawned;
    private float elapsedTime;
    private Rect boundary;

    private void Start()
    {
        boundary = gameObject.GetComponentInChildren<ScreenLayout>().OuterArea;

        // take half a second off the spawn cooldown per level
        spawnCooldown -= (PersistentManager.Instance.level - 1) / 2f;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainScene" && !PersistentManager.Instance.Running)
        {
            return;
        }

        if (totalSpawned > 0)
        {
            if (elapsedTime < spawnCooldown)
            {
                elapsedTime += Time.deltaTime;
                return;
            }

            if (totalSpawned >= maxAmount)
            {
                return;
            }
        }

        elapsedTime = 0;

        Spawn();
    }

    private void Spawn()
    {
        // Don't spawn over the max amount
        if (totalSpawned + amountToSpawn > maxAmount)
        {
            amountToSpawn = maxAmount - totalSpawned;
        }
        
        var spawnPos = CalculatePosition(amountToSpawn);
        for (var i = 0; i < amountToSpawn; i++)
        {
            var spawnedMonster = Instantiate(monster, spawnPos[i], Quaternion.identity);
            spawnedMonster.GetComponent<MonsterMove>().playerLoc = player.transform;
        }

        totalSpawned += amountToSpawn;
    }

    private Vector2[] CalculatePosition(int amount)
    {
        var spawnPositions = new Vector2[amount];
        // only generate this once - want the monsters to spawn on the same side which this dictates
        var r = Random.value;

        for (var i = 0; i < amount; i++)
        {
            Vector2 sp;
            if (r > 0.5)
            {
                sp.x = Random.Range(boundary.xMin, boundary.xMax);
                sp.y = player.transform.position.y > 0 ? boundary.yMin : boundary.yMax;
            }
            else
            {
                sp.y = Random.Range(boundary.yMin, boundary.yMax);
                sp.x = player.transform.position.x > 0 ? boundary.xMin : boundary.xMax;
            }

            spawnPositions[i] = sp;
        }

        return spawnPositions;
    }
}