using UnityEngine;
using GameDevTV.Inventories;

namespace AirShip.Inventory
{
    [CreateAssetMenu(menuName = ("GameDevTV/GameDevTV.UI.InventorySystem/Boost Item"))]

    public class BoostItem : EquipableItem
    {
        [SerializeField] GameObject boost;
        [SerializeField] GameObject boostFX;
        [SerializeField] float speedBurstMultiplier = 2f;
        [SerializeField] float burstLength = 3f;
        [SerializeField] float burstCoolDown = 3f;
        // Start is called before the first frame update
        public float GetBurst()
        {
            return speedBurstMultiplier;
        }

        public float GetBurstLength()
        {
            return burstLength;
        }

        public GameObject GetBoostObject()
        {
            return boost;
        }

        public float GetBurstCoolDown()
        {
            return burstCoolDown;
        }

        public GameObject GetBurstFX()
        {
            return boostFX;
        }
    }
}
