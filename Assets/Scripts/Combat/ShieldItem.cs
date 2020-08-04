using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

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
