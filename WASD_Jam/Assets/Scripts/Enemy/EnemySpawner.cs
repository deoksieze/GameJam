using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups;
        public int waveQuouta; //Количество врагов, которые заспаунятся в эту волну
        public float spawnInterval; //Интервал через которая спунятся ещё враги
        public int spawnCount; //Количество уже появившихся в этой волне протвников
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public GameObject enemyPrefab;
        public int enemyCount; //Количество врагов этого типа, которые появятся в этой волне
        public int spawnCount; //Количество уже появившихся в этой волне протвников этого типа
    }

    public List<Wave> waves;
    public int currentWaveIndex;

    [Header("Spawner Attributes")]
    float spawnerTimer;
    public float waveInterval;
    public Transform player;
    public int enemiesAlive;
    public int maxEnemyiesAllowed;
    public bool maxEnemyiesReached = false;

    [Header("Spawn Points")]
    public List<Transform> relativeSpawnPoints;

    void Start()
    {
        CalculateWaveQuota();
    }

    void Update()
    {

        if (currentWaveIndex < waves.Count && waves[currentWaveIndex].spawnCount == 0)
        {
            StartCoroutine(BeginNextWave());
        }

        spawnerTimer += Time.deltaTime;

        if (spawnerTimer >= waves[currentWaveIndex].spawnInterval)
        {
            spawnerTimer = 0f;
            SpawnEnemies();
        }
    }
    IEnumerator BeginNextWave()
    {
        yield return new WaitForSeconds(waveInterval);

        if (currentWaveIndex < waves.Count - 1)
        {
            currentWaveIndex += 1;
            CalculateWaveQuota();
        }
    }

    public void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveIndex].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveIndex].waveQuouta = currentWaveQuota;
        Debug.Log(currentWaveQuota);

    }

    void SpawnEnemies()
    {
        //Проверяем не запспаунились ли все враги в этой волне
        if (waves[currentWaveIndex].spawnCount < waves[currentWaveIndex].waveQuouta && !maxEnemyiesReached)
        {
            //Проходимся по каждой группе врагов
            foreach (var enemyGroup in waves[currentWaveIndex].enemyGroups)
            {
                //Проверем для этой группы, заспаунились ли все её представители в этой волне
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    if (enemiesAlive >= maxEnemyiesAllowed)
                    {
                        maxEnemyiesReached = true;
                        return;
                    }

                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);

                    enemiesAlive++;
                    enemyGroup.spawnCount++;
                    waves[currentWaveIndex].spawnCount++;
                }
            }
        }

        if (enemiesAlive < maxEnemyiesAllowed)
        {
            maxEnemyiesReached = false;
        }
    }

    public void onEnemyKilled()
    {
        enemiesAlive--;
    }
}
