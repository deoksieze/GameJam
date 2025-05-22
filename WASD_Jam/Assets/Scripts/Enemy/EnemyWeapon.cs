using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    float speed;
    float damage;
    Vector3 direction;
    public void SetUpBullet(float enemySpeed, float enemyDamage, Vector3 direction2Player, float enemyDestroyAfterSeconds)
    {
        speed = enemySpeed;
        damage = enemyDamage;
        direction = direction2Player;
        direction.Normalize();
        Destroy(gameObject, enemyDestroyAfterSeconds);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerStats playerStats = col.GetComponent<PlayerStats>();
            playerStats.TakeDamage(damage);
            Destroy(gameObject);  
        }
    }
    
}

