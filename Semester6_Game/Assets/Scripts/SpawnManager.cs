using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnManager : SingleTon<SpawnManager> {

    protected SpawnManager() { }

    public float spawnRadius = 1;
    public float spawnHeight = 1;

    private Vector3 originalSpawnPos;
    public Light mainLight;
    public List<PlayerHealth_NET> Players = new List<PlayerHealth_NET>();
    private float originalSpawnRadius;

    void Start()
    {
        originalSpawnRadius = spawnRadius;
        originalSpawnPos = transform.position;
    }

    public Vector3 GetSpawnPos()
    {
        Vector3 randomPos = Random.insideUnitSphere * spawnRadius;
        randomPos.y = spawnHeight;
        return randomPos;
    }

    public void SetSpawnRadius(float radius)
    {
        spawnRadius = radius;
    }

    public void SetSpawnOrigin(Vector3 pos)
    {
        transform.position = pos;
    }

    public void InitializeOriginalState()
    {
        spawnRadius = originalSpawnRadius;
        transform.position = originalSpawnPos;
    }
}
