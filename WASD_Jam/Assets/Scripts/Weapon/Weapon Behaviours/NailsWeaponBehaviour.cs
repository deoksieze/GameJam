using UnityEngine;

public class NailsWeaponBehaviour : ProjectileWeaponBehaviour
{
    
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
            transform.position += direction * weaponData.Speed * Time.deltaTime; //Set the movemetn of the fireball 
    }
}
