using UnityEngine;

public class FireballBehaviour : ProjectileWeaponBehaviour
{
    FireballController fc;
    protected override void Start()
    {
        base.Start();
        fc = FindAnyObjectByType<FireballController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * fc.speed * Time.deltaTime; //Set the movemetn of the fireball 
    }
}
