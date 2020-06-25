using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField] KeyCode mainWeaponKey;
    [SerializeField] KeyCode leftWeaponKey;
    [SerializeField] KeyCode rightWeaponKey;
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
    }

    private void FireMainWeapon()
    {
        foreach (TurretSlot slot in turretSlots)
        {
            if(slot.GetLocation() == TurretLocations.Main)
            {
                Turret turret = slot.GetComponentInChildren<Turret>();
                turret.Fire();
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
                turret.Fire();
            }
        }
    }
}
