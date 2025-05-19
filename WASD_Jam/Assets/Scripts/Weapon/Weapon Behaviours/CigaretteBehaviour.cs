using System.Collections.Generic;
using UnityEngine;

public class CigaretteBehaviour : MeleeWeaponBehavour
{
    List<GameObject> markedEnemies;
    public float scaleSpeed = 2f; // Скорость увеличения размера
    protected override void Start()
    {
        base.Start();
        markedEnemies = new List<GameObject>();
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") && !markedEnemies.Contains(col.gameObject))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);

            markedEnemies.Add(col.gameObject);
        }
    }

    void Update()
    {
        float scaleChange = scaleSpeed * Time.deltaTime;
        transform.localScale += new Vector3(scaleChange, scaleChange, scaleChange);
    }
}
