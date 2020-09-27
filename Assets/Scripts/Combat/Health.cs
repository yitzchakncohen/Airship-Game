using UnityEngine;
using GameDevTV.Inventories;
using AirShip.Inventory;

namespace AirShip.Combat
{
    public class Health : MonoBehaviour
    {
        //Health points Serialized for game testing. 
        [SerializeField] float healthPoints;
        [SerializeField] float baseMaxHealthPoints = 100f; 
        [SerializeField] float timeBeforeDestroy = 1f;
        [SerializeField] GameObject deathFX;
        Equipment equipment;
        Shield[] shields;

        float lastArmourValue;
        float maxHealthPoints;  

        void Start()
        {
            //Set up health points
            maxHealthPoints = baseMaxHealthPoints;
            healthPoints = maxHealthPoints;

            //Check equipment for health upgrades
            equipment = GetComponent<Equipment>();
            if(equipment)
            {
                // Subscribe to equipment update and check for new shields and armour.
                CheckArmour();
                equipment.equipmentUpdated += CheckArmour; 
                equipment.equipmentUpdated += CheckShield; 
            }

            // Check for shields even if there is no equipment.
            CheckShield();
            CheckArmour();
        }

        void Update()
        {
            // Check health
            if(healthPoints < 0)
            {
                DestroyTarget();
            }    
        }

        public void TakeDamage(float damage)
        {
            healthPoints -= damage;
        }

        public bool IsDead()
        {
            if(healthPoints < 0)
            {
                return true;
            }
            return false;
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
            Destroy(deathFXObject, timeBeforeDestroy + 1f);
            Destroy(gameObject, timeBeforeDestroy);
        }

        private void CheckArmour()
        {
            if(equipment)
            {
                ArmourItem armourItem = equipment.GetItemInSlot(EquipLocation.Armour) as ArmourItem;
            
                if(armourItem)
                {
                    //Add armour health to player health.
                    maxHealthPoints = baseMaxHealthPoints + armourItem.GetArmourAmount();
                    healthPoints = healthPoints + armourItem.GetArmourAmount();
                    lastArmourValue = armourItem.GetArmourAmount();
                }
                if(!armourItem)
                {
                    //If armour was removed, remove health bonus.
                    healthPoints = healthPoints - lastArmourValue;
                    maxHealthPoints = baseMaxHealthPoints;
                    lastArmourValue = 0f;
                }
            }
        }

        private void CheckShield()
        {
            //Check for a shield and tell the shield which gameObject it's attached to. 
            //This is here because all Players and NPCs have a health script. 
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
}
