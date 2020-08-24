using UnityEngine;
using UnityEngine.UI;
using AirShip.Control;

namespace AirShip.UI
{
    public class SpeedBurstUI : MonoBehaviour
    {
        [SerializeField] GameObject overlay;
        MovementController movementController;
        Image circle;
        float timer;
        float endTime;

        void Awake()
        {
            circle = overlay.GetComponent<Image>();
            movementController = GameObject.FindObjectOfType<MovementController>();
        }

        void OnEnable()
        {
            circle.fillAmount = 0;
            timer = 0f;
            UpdateCircle();
        }

        private void Update() 
        {
            timer += Time.deltaTime;
            circle.fillAmount = timer / endTime ;
        }

        void LateUpdate()
        {
            transform.forward = Camera.main.transform.forward;
        }

        public void UpdateCircle() 
        {
            endTime = movementController.GetBurthCoolDown();
        }
    }

}
