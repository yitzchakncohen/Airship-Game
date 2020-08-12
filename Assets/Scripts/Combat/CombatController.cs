﻿using System;
using System.Collections;
using System.Collections.Generic;
using AirShip.Iventory;
using UnityEngine;

namespace AirShip.Combat
{
    public class CombatController : MonoBehaviour
    {
        [SerializeField] public KeyCode mainWeaponKey;
        [SerializeField] public KeyCode leftWeaponKey;
        [SerializeField] public KeyCode rightWeaponKey;
        [SerializeField] TurretSlot mainTurret;
        TurretSlot[] turretSlots;

        // Start is called before the first frame update
        void Start()
        {
            GetTurrets();
        }

        private void GetTurrets()
        {
            turretSlots = FindObjectsOfType<TurretSlot>();
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(mainWeaponKey))
            {
                FireMainWeapon();
            }
            if(Input.GetKeyDown(rightWeaponKey))
            {
                FireRightWeapon();
            }
            if(Input.GetKeyDown(leftWeaponKey))
            {
                FireLeftWeapon();
            }
        }

        private void FireMainWeapon()
        {
            foreach (TurretSlot slot in turretSlots)
            {
                if(slot.GetLocation() == TurretLocations.Main)
                {
                    Turret turret = slot.GetComponentInChildren<Turret>();
                    if(turret != null)
                    {
                        turret.Fire();
                    }
                }
            }
        }

        private void FireRightWeapon()
        {
            foreach (TurretSlot slot in turretSlots)
            {
                if(slot.GetLocation() == TurretLocations.Right)
                {
                    Turret turret = slot.GetComponentInChildren<Turret>();
                    if(turret != null)
                    {
                        turret.Fire();
                    }
                }
            }
        }
        
        private void FireLeftWeapon()
        {
            foreach (TurretSlot slot in turretSlots)
            {
                if(slot.GetLocation() == TurretLocations.Left)
                {
                    Turret turret = slot.GetComponentInChildren<Turret>();
                    if(turret != null)
                    {
                        turret.Fire();
                    }
                }
            }
        }
    }
}
