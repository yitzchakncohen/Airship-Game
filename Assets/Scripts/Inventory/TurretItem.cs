using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

namespace AirShip.Iventory
{
    [CreateAssetMenu(menuName = ("GameDevTV/GameDevTV.UI.InventorySystem/Turret Item"))]
    public class TurretItem : EquipableItem
    {
        [SerializeField] GameObject turret;

        public GameObject GetTurret()
        {
            return turret;
        }
    }
}
