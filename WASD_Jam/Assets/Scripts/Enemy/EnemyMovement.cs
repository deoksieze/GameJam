using UnityEngine;

public class EnemyMovement : MonoBehaviour
{   
    public EnemyScriptableObject enemyData;
    public EnemyStats enemyStats;
    Transform target;

    void Start()
    {
        target = FindAnyObjectByType<Movement>().transform;
    }

    void Update()
    {
        if (!enemyStats.isActive) return;

        float distanse = Vector2.Distance(target.position, transform.position);
        if (distanse >= enemyData.DistanseToPlayer)
        {
            var step = enemyStats.currentMoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }
}
