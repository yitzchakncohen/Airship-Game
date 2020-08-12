using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Inventories;
using System;
using AirShip.Iventory;

namespace AirShip.Combat
{
    public class TurretSlot : MonoBehaviour
    {
        [SerializeField] TurretSize turretSize;
        [SerializeField] TurretItem itemInTurret;
        [SerializeField] TurretLocations turretLocation;
        [SerializeField] EquipLocation equipLocation;
        GameObject turret;
        Equipment equipment;

        // Start is called before the first frame update
        void Start()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            equipment = player.GetComponent<Equipment>();
            equipment.equipmentUpdated += UpdateTurret;
            TurretOnStart();
        }

        private void TurretOnStart()
        {
            if(itemInTurret == null) return;
            turret = itemInTurret.GetTurret();
            GameObject localTurret = Instantiate(turret, this.transform.position, this.transform.rotation, this.transform);
            localTurret.name = equipLocation.ToString();
            localTurret.tag = this.tag;
            // print("Create " + localTurret.name);
            equipment.AddItem(equipLocation, itemInTurret);
        }

        private void UpdateTurret()
        {
            if(itemInTurret == equipment.GetItemInSlot(equipLocation) as TurretItem) return;
            DestroyOldTurret();
            itemInTurret = equipment.GetItemInSlot(equipLocation) as TurretItem;
            if(itemInTurret != null)
            {
                turret = itemInTurret.GetTurret();
                GameObject localTurret = Instantiate(turret, this.transform.position, this.transform.rotation, this.transform);
                localTurret.name = equipLocation.ToString();
                localTurret.tag = this.tag;
                // print("Create " + localTurret.name);
            }
        }

        private void DestroyOldTurret()
        {
            Transform oldTurret = transform.Find(equipLocation.ToString());
            if(oldTurret != null)
            {
                // print("Destroy " + oldTurret.gameObject);
                Destroy(oldTurret.gameObject);
            }
        }

        // Update is called once per frame
        public TurretLocations GetLocation()
        {
            return turretLocation;
        }
    }
}