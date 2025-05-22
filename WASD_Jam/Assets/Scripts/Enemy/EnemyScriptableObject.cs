using UnityEngine;


[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObject/enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    //Base stats for the enemyes

    [SerializeField]
    float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }
    [SerializeField]
    float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }
    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }

    [SerializeField]
    float distanseToPlayer;
    public float DistanseToPlayer { get => distanseToPlayer; private set => distanseToPlayer = value; }

    [SerializeField]
    bool isLongRangeEnemy;
    public bool IsLongRangeEnemy { get => isLongRangeEnemy; private set => isLongRangeEnemy = value; }

    [SerializeField]
    GameObject bullet;
    public GameObject Bullet { get => bullet; private set => bullet = value; }

    [SerializeField]
    float bulletSpeed;
    public float BulletSpeed { get => bulletSpeed; private set => bulletSpeed = value; }

    [SerializeField]
    float shootInterval;
    public float ShootInterval { get => shootInterval; private set => shootInterval = value; }

    [SerializeField]
    float maxShootDistance; // За пределами этой дистанции враг перестает стрелять
    public float MaxShootDistance { get => maxShootDistance; private set => maxShootDistance = value; }

    [SerializeField]
    float destryBulletAfter;
    public float DestroyBulletAfter { get => destryBulletAfter; private set => destryBulletAfter = value; }
}
