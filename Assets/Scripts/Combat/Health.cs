using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Inventories;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealthPoints = 100;
    [SerializeField] private float healthPoints;
    [SerializeField] float timeBeforeDestroy = 1f;
    [SerializeField] GameObject deathFX;
    Equipment equipment;
    Shield[] shields;
    bool dead = false;
    float defaultMaxHealthPoints;
    float lastArmourValue;
    // Start is called before the first frame update
    void Start()
    {
        healthPoints = maxHealthPoints;
        defaultMaxHealthPoints = maxHealthPoints;
        equipment = GetComponent<Equipment>();
        if(equipment)
        {
            // print("Equipment Found");
            equipment.equipmentUpdated += checkArmour; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(healthPoints < 0)
        {
            DestroyTarget();
        }
        checkShield();     
    }

    public void TakeDamage(float damage)
    {
        healthPoints -= damage;
    }

    public bool IsDead()
    {
        return dead;
    }

    public float GetHealth()
    {
        return healthPoints;
    }

    public float GetMaxHealth()
    {
        return maxHealthPoints;
    }

    private void DestroyTarget()
    {
        GameObject deathFXObject = Instantiate(deathFX, transform.position, transform.rotation);
        dead = true;
        Destroy(deathFXObject, timeBeforeDestroy + 1f);
        Destroy(gameObject, timeBeforeDestroy);
    }

    private void checkArmour()
    {
        ArmourItem armourItem = equipment.GetItemInSlot(EquipLocation.Armour) as ArmourItem;
        if(armourItem)
        {
            maxHealthPoints = defaultMaxHealthPoints +armourItem.GetArmourAmount();
            healthPoints = healthPoints + armourItem.GetArmourAmount();
            lastArmourValue = armourItem.GetArmourAmount();
        }
        if(!armourItem)
        {
            healthPoints = healthPoints - lastArmourValue;
            maxHealthPoints = defaultMaxHealthPoints;
            lastArmourValue = 0f;
        }
    }

    private void checkShield()
    {
        shields = this.GetComponentsInChildren<Shield>();
        foreach (Shield shield in shields)
        {
            if(shield)
            {
                shield.SetShieldHost(gameObject);
            }
        }
    }

}
