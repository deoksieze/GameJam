using System.Collections.Generic;
using UnityEngine;

public class LocationScript : MonoBehaviour
{
    public LocationScriptableObject locationData;
    public EnemySpawner enemySpawner;

    public List<EnemyStats> enemyiesInLocation = new List<EnemyStats>();

    void Start()
    {
        locationData.ResetWaveData();
        enemySpawner = FindAnyObjectByType<EnemySpawner>();
        enemySpawner.locationsData.Add(locationData.LocationName, locationData);

        if (locationData.IsHub)
        {
            enemySpawner.currentLocation = locationData.LocationName;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered location trigger: " + locationData.LocationName);

            enemySpawner.currentLocation = locationData.LocationName;

            foreach (var enemy in enemyiesInLocation)
            {
                if (enemy != null) enemy.isActive = true;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var enemy in enemyiesInLocation)
            {
                if (enemy != null) enemy.isActive = false;
            }
        }
    }

    public void RegisterEnemy(EnemyStats enemy)
    {
        if (!enemyiesInLocation.Contains(enemy))
            enemyiesInLocation.Add(enemy);
    }
    
    public void UnregisterEnemy(EnemyStats enemy)
    {
        enemyiesInLocation.Remove(enemy);
    }

}
