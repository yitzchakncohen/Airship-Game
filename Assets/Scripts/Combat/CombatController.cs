using AirShip.Inventory;
using UnityEngine;

namespace AirShip.Combat
{
    public class CombatController : MonoBehaviour
    {
        //This class controls firing of weapons. 
        ControlsKeyMapping controls;
        TurretSlot[] turretSlots;

        void Start()
        {
            //Find Turret slots
            GetTurrets();
            //Get controls component
            controls = GetComponentInChildren<ControlsKeyMapping>();
        }

        private void GetTurrets()
        {
            turretSlots = GetComponentsInChildren<TurretSlot>();
        }

        // Update is called once per frame
        void Update()
        {
            //Weapons input
            if(Input.GetKeyDown(controls.GetKeyCode(Controls.FireMain)))
            {
                FireMainWeapon();
            }
            if(Input.GetKeyDown(controls.GetKeyCode(Controls.FireRight)))
            {
                FireRightWeapon();
            }
            if(Input.GetKeyDown(controls.GetKeyCode(Controls.FireLeft)))
            {
                FireLeftWeapon();
            }
        }

        private void FireMainWeapon()
        {
            //Check tags for which weapons to fire as "Main Weapons"
            foreach (TurretSlot slot in turretSlots)
            {
                if(slot.GetLocation() == TurretLocations.Main)
                {
                    FireTurret(slot);
                }
            }
        }

        private void FireRightWeapon()
        {
            //Check tags for which weapons to fire as "Right Weapons"
            foreach (TurretSlot slot in turretSlots)
            {
                if(slot.GetLocation() == TurretLocations.Right)
                {
                    FireTurret(slot);
                }
            }
        }
        
        private void FireLeftWeapon()
        {
            //Check tags for which weapons to fire as "Left Weapons"
            foreach (TurretSlot slot in turretSlots)
            {
                if(slot.GetLocation() == TurretLocations.Left)
                {
                    FireTurret(slot);
                }
            }
        }

        private static void FireTurret(TurretSlot slot)
        {
            //Get the turret in the slot.
            Turret turret = slot.GetComponentInChildren<Turret>();
            if (turret != null)
            {
                //Fire!
                turret.Fire();
            }
        }
    }
}
