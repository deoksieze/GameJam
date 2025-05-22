using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public string currentLocation;

    public Dictionary<string, LocationScriptableObject> locationsData = new Dictionary<string, LocationScriptableObject>();
    private List<LocationScript> allZones;

    public int currentWaveIndex;

    [Header("Spawner Attributes")]
    private float spawnerTimer;
    public float waveInterval;
    public Transform player;
    public int enemiesAlive;
    public int maxEnemiesAllowed;
    public bool maxEnemiesReached = false;

    [Header("Spawn Points")]
    public List<Transform> relativeSpawnPoints;

    void Start()
    {
        // Опционально — если currentLocation уже установлен
        if (!string.IsNullOrEmpty(currentLocation) && locationsData.ContainsKey(currentLocation))
        {
            CalculateWaveQuota();
        }

        allZones = new List<LocationScript>(Object.FindObjectsByType<LocationScript>(FindObjectsSortMode.None));
    }

    void Update()
    {
        if (!locationsData.ContainsKey(currentLocation)) return;

        var location = locationsData[currentLocation];

        if (location.IsHub)
        {
            // В хабе враги не спаунятся
            return;
        }

        var waves = location.Waves;

        if (currentWaveIndex >= waves.Count) return;

        if (waves[currentWaveIndex].SpawnCount == 0)
        {
            StartCoroutine(BeginNextWave());
        }

        spawnerTimer += Time.deltaTime;

        if (spawnerTimer >= waves[currentWaveIndex].SpawnInterval)
        {
            spawnerTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator BeginNextWave()
    {
        yield return new WaitForSeconds(waveInterval);

        var waves = locationsData[currentLocation].Waves;

        if (currentWaveIndex < waves.Count - 1)
        {
            currentWaveIndex++;
            CalculateWaveQuota();
        }
    }

    public void CalculateWaveQuota()
    {
        foreach (var kvp in locationsData)
        {
            string locationName = kvp.Key;
            var location = kvp.Value;

            if (location.IsHub) continue; // Пропускаем хабы

            var waves = location.Waves;

            for (int i = 0; i < waves.Count; i++)
            {
                int waveQuota = 0;

                foreach (var enemyGroup in waves[i].EnemyGroups)
                {
                    waveQuota += enemyGroup.EnemyCount;
                }

                waves[i].WaveQuouta = waveQuota;
                Debug.Log($"[{locationName}] Wave {i} quota: {waveQuota}");
            }
        }
    }


    void SpawnEnemies()
    {
        if (!locationsData.ContainsKey(currentLocation)) return;

        var location = locationsData[currentLocation];
        
        var wave = location.Waves[currentWaveIndex];
        // var wave = locationsData[currentLocation].Waves[currentWaveIndex];

        if (wave.SpawnCount < wave.WaveQuouta && !maxEnemiesReached)
        {
            foreach (var enemyGroup in wave.EnemyGroups)
            {
                if (enemyGroup.SpawnCount < enemyGroup.EnemyCount)
                {
                    if (enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }

                    Vector3 spawnPos = player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position;
                    var enemy = Instantiate(enemyGroup.EnemyPrefab, spawnPos, Quaternion.identity);
                    EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();

                    foreach (var zone in allZones)
                    {
                        if (zone.locationData.IsHub) continue;
                        if (zone.locationData.LocationName == currentLocation)
                        {
                            zone.RegisterEnemy(enemyStats);
                        }
                    }

                    enemiesAlive++;
                    enemyGroup.SpawnCount++;
                    wave.SpawnCount++;
                }
            }
        }

        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }

    public void OnEnemyKilled()
    {
        enemiesAlive--;
    }
}
