using UnityEngine;
using AirShip.Control;

namespace AirShip.UI
{
    //This class just makes the compass arrow always point north in the UI.
    public class FaceNorth : MonoBehaviour
    {
        MovementController movementController;

        void Start()
        {
            movementController = GameObject.FindObjectOfType<MovementController>();
        }
        void Update()
        {
            ApplyRotation();
        }

        public void ApplyRotation()
        {
            transform.rotation = Quaternion.Euler(0,0,movementController.GetCompassRotation());
        }
    }
}
