using UnityEngine;

public class Money : MonoBehaviour, ICollectable
{
    public int moneyGranted;
    public void Collect()
    {
        PlayerStats player = FindAnyObjectByType<PlayerStats>();
        player.IncreaseMoney(moneyGranted);
        Destroy(gameObject);
    }

}
