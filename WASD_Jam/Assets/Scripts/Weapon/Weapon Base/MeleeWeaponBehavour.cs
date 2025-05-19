using UnityEngine;

/// <summary>be
/// Base script of all mellee behaviours [To be placed on a prefab of a wepaon that is melee]
/// </summary>
public class MeleeWeaponBehavour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public WeaponScriptableObject weaponData;
    public float destroyAfterSeconds;

    //Current Stats
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCoolDownDuration;
    protected int currentPierce;

    void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCoolDownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
        }
    }
}
