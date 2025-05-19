using UnityEngine;

public class CigaretteController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedCigarette = Instantiate(weaponData.Prefab);
        spawnedCigarette.transform.position = transform.position;
        spawnedCigarette.transform.parent = transform;
    }
}
