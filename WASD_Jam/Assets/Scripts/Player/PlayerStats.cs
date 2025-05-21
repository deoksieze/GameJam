using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;
    public UI_manager uI_Manager;

    //current Stats:
    float currentHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;
    float currentArmor;
    public float currentMoney;

    //I-Frames. Переменная, чтобы игрок не получам постоянный урон от врагов.
    [Header("I-frames")]
    public float invincibillityDuration;
    float invincibillityTimer;
    bool isInvinciblle;

    void Awake()
    {
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        currentArmor = characterData.Armor;
        currentMoney = 0;
    }

    void Update()
    {
        if (invincibillityTimer > 0)
        {
            invincibillityTimer -= Time.deltaTime;
        }
        else if (isInvinciblle)
        {
            isInvinciblle = false;
        }

        uI_Manager.SetMoney(currentMoney);
        uI_Manager.SetHealth(currentHealth);
    }

    public void IncreaseMoney(int money)
    {
        currentMoney += money;
    }

    public void TakeDamage(float dmg)
    {
        if (!isInvinciblle)
        {
            invincibillityTimer = invincibillityDuration;
            isInvinciblle = true;
            currentHealth -= dmg;
            if (currentHealth <= 0)
            {
                Kill();
            }
        }
    }

    public void Kill()
    {
        Debug.Log("PLAYER IS DEAD");
    }

    public void RestoreHealth(float amount)
    {
        currentHealth = Math.Min(currentHealth + amount, characterData.MaxHealth);
    }
}
