using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

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
