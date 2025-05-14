using UnityEngine;

public class FireballController : WeaponController
{
    protected override void Start()
    {
        base.Start();   
    }

    protected override void Attack() 
    {
        base.Attack();
        GameObject spawnedFireball = Instantiate(weaponData.Prefab);
        spawnedFireball.transform.position = transform.position; //Позиция родителя
        spawnedFireball.GetComponent<FireballBehaviour>().DirectionChecker(pm.lastMovedVector);
    }
}
