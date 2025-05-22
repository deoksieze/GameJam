using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LocationScriptableObject", menuName = "ScriptableObject/location")]
public class LocationScriptableObject : ScriptableObject
{
    [SerializeField] private string locationName;
    public string LocationName { get => locationName; private set => locationName = value; }

    [SerializeField] private List<Wave> waves = new List<Wave>();
    public List<Wave> Waves { get => waves; private set => waves = value; }

    [SerializeField] private bool isHub = false;
    public bool IsHub => isHub;


    public void ResetWaveData()
    {
        foreach (var wave in Waves)
        {
            wave.SpawnCount = 0;
            foreach (var group in wave.EnemyGroups)
            {
                group.SpawnCount = 0;
            }
        }
    }

}

[System.Serializable]
public class Wave
{
    [SerializeField] private string waveName;
    public string WaveName { get => waveName; private set => waveName = value; }

    [SerializeField] private List<EnemyGroup> enemyGroups = new List<EnemyGroup>();
    public List<EnemyGroup> EnemyGroups { get => enemyGroups; private set => enemyGroups = value; }

    [SerializeField] private int waveQuouta;
    public int WaveQuouta { get => waveQuouta; set => waveQuouta = value; }

    [SerializeField] private float spawnInterval;
    public float SpawnInterval { get => spawnInterval; private set => spawnInterval = value; }

    [SerializeField] private int spawnCount;
    public int SpawnCount { get => spawnCount; set => spawnCount = value; } // <- Разрешаем изменять, если нужно в ходе выполнения
}

[System.Serializable]
public class EnemyGroup
{
    [SerializeField] private string enemyName;
    public string EnemyName { get => enemyName; private set => enemyName = value; }

    [SerializeField] private GameObject enemyPrefab;
    public GameObject EnemyPrefab { get => enemyPrefab; private set => enemyPrefab = value; }

    [SerializeField] private int enemyCount;
    public int EnemyCount { get => enemyCount; private set => enemyCount = value; }

    [SerializeField] private int spawnCount;
    public int SpawnCount { get => spawnCount; set => spawnCount = value; } // <- Аналогично, можно изменять во время игры
}