using GameDevTV.Inventories;
using UnityEngine;

namespace AirShip.Inventory
{
    [CreateAssetMenu(menuName = ("GameDevTV/GameDevTV.UI.InventorySystem/Armour Item"))]
    public class ArmourItem : EquipableItem
    {
        [SerializeField] GameObject armour;
        [SerializeField] float armourAmount;
        // Start is called before the first frame update
        public GameObject GetArmour()
        {
            return armour;
        }
        public float GetArmourAmount()
        {
            return armourAmount;
        }
    }
}
