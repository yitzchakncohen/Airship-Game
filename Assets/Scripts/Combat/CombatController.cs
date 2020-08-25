using AirShip.Inventory;
using UnityEngine;

namespace AirShip.Combat
{
    public class CombatController : MonoBehaviour
    {
        ControlsKeyMapping controls;
        TurretSlot[] turretSlots;

        // Start is called before the first frame update
        void Start()
        {
            //Find Turret slots
            GetTurrets();
            //Get controls component
            controls = GameObject.FindObjectOfType<ControlsKeyMapping>();
        }

        private void GetTurrets()
        {
            turretSlots = FindObjectsOfType<TurretSlot>();
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
            Turret turret = slot.GetComponentInChildren<Turret>();
            if (turret != null)
            {
                turret.Fire();
            }
        }
    }
}
