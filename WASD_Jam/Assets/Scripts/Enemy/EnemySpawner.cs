using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public string currentLocation;

    public Dictionary<string, LocationScriptableObject> locationsData = new Dictionary<string, LocationScriptableObject>();
    private List<LocationScript> allZones;

    private LocationScript currentZone;

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
                    //--------------------------------------------------------------------------------------
                    currentZone = allZones.Find(z =>
                    !z.locationData.IsHub &&
                    z.locationData.LocationName == currentLocation);

                    if (currentZone == null)
                        return; // Без зоны — не спауним

                    var spawnPos = GeneratePosition();

                    if (!spawnPos.isFound) return;

                    var enemy = Instantiate(enemyGroup.EnemyPrefab, spawnPos.pos, Quaternion.identity);
                    EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
                    currentZone.RegisterEnemy(enemyStats);

                    // Vector3 spawnPos = player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position;
                    // var enemy = Instantiate(enemyGroup.EnemyPrefab, spawnPos, Quaternion.identity);
                    // EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();

                    // foreach (var zone in allZones)
                    // {
                    //     if (zone.locationData.IsHub) continue;
                    //     if (zone.locationData.LocationName == currentLocation)
                    //     {
                    //         zone.RegisterEnemy(enemyStats);
                    //     }
                    // }

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

    public (bool isFound, Vector2 pos) GeneratePosition()
    {
        Collider2D zoneCollider = currentZone.GetComponent<Collider2D>();
                    if (zoneCollider == null)
                        return (false, Vector2.zero);

                    Vector3 spawnPos = Vector3.zero;
                    bool validPositionFound = false;
                    int maxAttempts = 10;

                    for (int i = 0; i < maxAttempts; i++)
                    {
                        Vector3 candidatePos = player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position;

                        // Проверка на попадание внутрь коллайдера зоны
                        if (zoneCollider.OverlapPoint(candidatePos))
                        {
                            spawnPos = candidatePos;
                            validPositionFound = true;
                            return (validPositionFound, spawnPos);
                        }
                    }
                    Debug.LogWarning("Не удалось найти подходящую позицию для спавна врага в зоне.");
                    return (false, Vector2.zero);
    }
}
