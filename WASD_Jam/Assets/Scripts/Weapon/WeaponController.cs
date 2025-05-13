using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Timeline;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public GameObject prefab;
    public float damage;
    public float speed;
    public float cooldownDuration;
    float currentCooldowan;
    public int pierce;

    protected Movement pm;

    protected virtual void Start() {
        pm = FindFirstObjectByType<Movement>();
        currentCooldowan = cooldownDuration;
    }

    protected virtual void Update() {
        
        currentCooldowan -= Time.deltaTime;

        if (currentCooldowan <= 0f) {
            Attack();
        }
    }
    
    protected virtual void Attack()
    {
        currentCooldowan = cooldownDuration;
    }
}
