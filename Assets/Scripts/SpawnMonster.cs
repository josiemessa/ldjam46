using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    public GameObject monster;
    public GameObject player;
    public float spawnCooldown = 1f;
    public float amountToSpawn = 1f;
    public float spawnDistanceMax = 0f;
    public float spawnDistanceMin = 0f;
    public float maxAmount = 10f;

    // TODO get this to read from the gameObject rather than the number spawned
    // when player can kill monsters
    private float totalSpawned = 0f;

    void Start()
    {
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
            GameObject spawnedMonster = Instantiate(monster,spawnPos , Quaternion.identity);
            spawnedMonster.GetComponent<MonsterMove>().playerLoc = player.transform;
            
            amountSpawned++;
        }

        totalSpawned += amountSpawned;
    }

    Vector2 calculatePosition()
    {
        // This has to be Vector3 otherwise it gets upset...
        Vector3 offset = (Random.insideUnitCircle * spawnDistanceMax);
        Vector2 spawnPosition = player.transform.position + offset;
        
        // lock spawn position to inside game boundary
        spawnPosition.x = Mathf.Min(spawnPosition.x, 16);
        spawnPosition.x = Mathf.Max(spawnPosition.x, -16);
        spawnPosition.y = Mathf.Min(spawnPosition.y, 9);
        spawnPosition.y = Mathf.Max(spawnPosition.y, -9);

        if (Vector2.Distance(spawnPosition, player.transform.position) < spawnDistanceMin)
        {
            return calculatePosition();
        }
        return spawnPosition;
    }
}
