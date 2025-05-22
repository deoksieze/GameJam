using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    //Current stats:
    float currentHealth;
    public float currentMoveSpeed;
    float currentDamage;
    public bool isActive;

    public float despawnDistance = 20;
    public float shootTimer;
    Transform player;

    void Awake()
    {
        isActive = true;
        currentMoveSpeed = enemyData.MoveSpeed;
        currentDamage = enemyData.Damage;
        currentHealth = enemyData.MaxHealth;
    }

    void Start()
    {
        player = FindAnyObjectByType<PlayerStats>().transform;
    }


    void Update()
    {
        if (!isActive) return;

        if (Vector2.Distance(player.position, transform.position) >= despawnDistance)
            {
                ReturnEnemy();
            }

        if (enemyData.IsLongRangeEnemy)
        {
            shootTimer += Time.deltaTime;
            if (Vector3.Distance(player.position, transform.position) <= enemyData.MaxShootDistance && shootTimer > enemyData.ShootInterval)
            {
                shootTimer = 0f;
                Shoot();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }

    private void OnDestroy()
    {
        EnemySpawner enemySpawner = FindAnyObjectByType<EnemySpawner>();
        enemySpawner.OnEnemyKilled();
    }

    void ReturnEnemy() {
        EnemySpawner es = FindAnyObjectByType<EnemySpawner>();
        transform.position = player.position + es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Count)].position;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(enemyData.Bullet);
        bullet.transform.position = transform.position;
        EnemyWeapon enemyWeapon = bullet.GetComponent<EnemyWeapon>();
        enemyWeapon.SetUpBullet(enemyData.BulletSpeed, enemyData.Damage, player.position - transform.position, enemyData.DestroyBulletAfter);
    }
}
