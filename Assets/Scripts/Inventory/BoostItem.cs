using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Inventories;

[CreateAssetMenu(menuName = ("GameDevTV/GameDevTV.UI.InventorySystem/Boost Item"))]

public class BoostItem : EquipableItem
{
    [SerializeField] GameObject boost;
    [SerializeField] GameObject boostFX;
    [SerializeField] float speedBurstMultiplier;
    [SerializeField] float burstLength;
    [SerializeField] float burstCoolDown;
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
