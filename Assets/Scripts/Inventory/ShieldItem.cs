using GameDevTV.Inventories;
using UnityEngine;

namespace AirShip.Inventory
{
    [CreateAssetMenu(menuName = ("GameDevTV/GameDevTV.UI.InventorySystem/Shield Item"))]

    public class ShieldItem : EquipableItem
    {
        [SerializeField] GameObject shield;
        
        // Start is called before the first frame update
        public GameObject GetShield()
        {
            return shield;
        }
    }
}
