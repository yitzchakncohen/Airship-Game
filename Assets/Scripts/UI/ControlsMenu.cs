using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// using AirShip.Combat;
using AirShip.Control;

namespace AirShip.UI
{
    public class ControlsMenu : MonoBehaviour
    {
        // private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
        private KeyMapButton currentKey;

        public TextMeshProUGUI forward, reverse, rotateLeft, rotateRight, up, down, mainFire, leftFire, rightFire, speedBurst; 
        [SerializeField] Color32 normalColour;
        [SerializeField] Color32 selectedColour;
        ControlsKeyMapping controls;

        void Start()
        {
            controls = GameObject.FindObjectOfType<ControlsKeyMapping>();
            UpdateButtonText();
        }

        private void UpdateButtonText()
        {
            forward.text = controls.GetKeyCode(Controls.Forward).ToString();
            reverse.text = controls.GetKeyCode(Controls.Reverse).ToString();
            rotateLeft.text = controls.GetKeyCode(Controls.RotateLeft).ToString();
            rotateRight.text = controls.GetKeyCode(Controls.RotateRight).ToString();
            up.text = controls.GetKeyCode(Controls.Up).ToString();
            down.text = controls.GetKeyCode(Controls.Down).ToString();
            mainFire.text = controls.GetKeyCode(Controls.FireMain).ToString();
            leftFire.text = controls.GetKeyCode(Controls.FireLeft).ToString();
            rightFire.text = controls.GetKeyCode(Controls.FireRight).ToString();
            speedBurst.text = controls.GetKeyCode(Controls.SpeedBurst).ToString();
        }

        /// <summary>
        /// OnGUI is called for rendering and handling GUI events.
        /// This function can be called multiple times per frame (one call per event).
        /// </summary>
        void OnGUI()
        {
            if(currentKey != null)
            {
                Event e =Event.current;
                if(e.isKey)
                {
                    controls.SetKeyCode(currentKey.controlReference, e.keyCode); 
                    currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = e.keyCode.ToString();
                    currentKey.GetComponent<Image>().color = normalColour;
                    UpdateControls();
                    currentKey = null; 
                }
            }
        }

        public void ChangeKey(KeyMapButton clicked)
        {
            if(currentKey != null)
            {
                currentKey.GetComponent<Image>().color = normalColour;
            }
            currentKey = clicked;
            currentKey.GetComponent<Image>().color = selectedColour;
        }

        public void SetKeysToDefault()
        {
            controls.SetKeysToDefault();
            UpdateButtonText();
        }

        private void UpdateControls()
        {
            controls.UpdateControls();
        }
    }
}
