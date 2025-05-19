using System;
using UnityEngine;

public class NailsWeaponController : WeaponController
{
    protected override void Start()
    {
        base.Start();   
    }

    protected override void Attack() 
    {
        base.Attack();
        GameObject spawnedNail = Instantiate(weaponData.Prefab);
        spawnedNail.transform.position = transform.position; //Позиция родителя
        spawnedNail.GetComponent<NailsWeaponBehaviour>().DirectionChecker(pm.lastMovedVector);
    }
}
