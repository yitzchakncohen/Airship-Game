using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

public class ShieldSlot : MonoBehaviour
{
    [SerializeField] ShieldItem itemInSlot;
    [SerializeField] EquipLocation equipLocation;
    GameObject shield;
    Equipment equipment;
    Vector3 startingRotation = new Vector3(90,0,0);
    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        equipment = player.GetComponent<Equipment>();
        equipment.equipmentUpdated += UpdateShield;
        ShieldOnStart();
    }

    private void ShieldOnStart()
    {
        if(itemInSlot == null) return;
        shield = itemInSlot.GetShield();
        GameObject localShield = Instantiate(shield, this.transform.position, this.transform.rotation, this.transform);
        localShield.name = equipLocation.ToString();
        localShield.transform.eulerAngles = startingRotation;
        equipment.AddItem(equipLocation, itemInSlot);
    }

    private void UpdateShield()
    {
        if(itemInSlot == equipment.GetItemInSlot(equipLocation) as ShieldItem) return;
        DestroyOldShield();
        itemInSlot = equipment.GetItemInSlot(equipLocation) as ShieldItem;
        if(itemInSlot != null)
        {
            shield = itemInSlot.GetShield();
            GameObject localShield = Instantiate(shield, this.transform.position, this.transform.rotation, this.transform);
            localShield.name = equipLocation.ToString();
            localShield.transform.eulerAngles = startingRotation;
        }
    }

    private void DestroyOldShield()
    {
        Transform oldShield = transform.Find(equipLocation.ToString());
        if(oldShield != null)
        {
            Destroy(oldShield.gameObject);
        }
    }
}
