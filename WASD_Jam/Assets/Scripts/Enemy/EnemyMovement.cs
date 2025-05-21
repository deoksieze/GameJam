using UnityEngine;

public class EnemyMovement : MonoBehaviour
{   
    public EnemyScriptableObject enemyData;
    Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = FindAnyObjectByType<Movement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distanse = Vector2.Distance(target.position, transform.position);
        if (distanse >= enemyData.DistanseToPlayer)
        {
            var step = enemyData.MoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }
}
