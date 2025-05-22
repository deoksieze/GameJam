using UnityEngine;

public class LocationScript : MonoBehaviour
{
    public LocationScriptableObject locationData;
    public EnemySpawner enemySpawner;


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
        }
    }
}
