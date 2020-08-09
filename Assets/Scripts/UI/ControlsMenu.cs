using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ControlsMenu : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    private GameObject currentKey;

    public TextMeshProUGUI forward, reverse, rotateLeft, rotateRight, up, down, mainFire, leftFire, rightFire, speedBurst; 

    [SerializeField] GameObject controlsCanvas;
    [SerializeField] Color32 normalColour;
    [SerializeField] Color32 selectedColour;
    MovementController playerMovementController;
    CombatController playerCombatController;

    void Start()
    {
        keys.Add("Forward", KeyCode.W);
        keys.Add("Reverse", KeyCode.S);
        keys.Add("Rotate Left", KeyCode.A);
        keys.Add("Rotate Right", KeyCode.D);
        keys.Add("Up", KeyCode.UpArrow);
        keys.Add("Down", KeyCode.DownArrow);
        keys.Add("Fire Main", KeyCode.Space);
        keys.Add("Fire Left", KeyCode.LeftArrow);
        keys.Add("Fire Right", KeyCode.RightArrow);
        keys.Add("Speed Burst", KeyCode.LeftShift);
        UpdateButtonText();

        playerMovementController = GameObject.FindObjectOfType<MovementController>();
        playerCombatController = GameObject.FindObjectOfType<CombatController>();
    }

    private void UpdateButtonText()
    {
        forward.text = keys["Forward"].ToString();
        reverse.text = keys["Reverse"].ToString();
        rotateLeft.text = keys["Rotate Left"].ToString();
        rotateRight.text = keys["Rotate Right"].ToString();
        up.text = keys["Up"].ToString();
        down.text = keys["Down"].ToString();
        mainFire.text = keys["Fire Main"].ToString();
        leftFire.text = keys["Fire Left"].ToString();
        rightFire.text = keys["Fire Right"].ToString();
        speedBurst.text = keys["Speed Burst"].ToString();
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
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = e.keyCode.ToString();
                currentKey.GetComponent<Image>().color = normalColour;
                updateControls();
                currentKey = null; 
            }
        }
    }

    public void changeKey(GameObject clicked)
    {
        if(currentKey != null)
        {
            currentKey.GetComponent<Image>().color = normalColour;
        }
        currentKey = clicked;
        currentKey.GetComponent<Image>().color = selectedColour;
    }

    public void setKeysToDefauSlt()
    {
        keys["Forward"] = KeyCode.W;
        keys["Reverse"] = KeyCode.S;
        keys["Rotate Left"] = KeyCode.A;
        keys["Rotate Right"] = KeyCode.D;
        keys["Up"] = KeyCode.UpArrow;
        keys["Down"] = KeyCode.DownArrow;
        keys["Fire Main"] = KeyCode.Space;
        keys["Fire Left"] = KeyCode.LeftArrow;
        keys["Fire Right"] = KeyCode.RightArrow;
        keys["Speed Burst"] = KeyCode.LeftShift;
        UpdateButtonText();
        updateControls();
    }

    private void updateControls()
    {
        playerMovementController.forwardPositive = keys["Forward"];
        playerMovementController.forwardNegative = keys["Reverse"];
        playerMovementController.rotateNegative = keys["Rotate Left"];
        playerMovementController.rotatePositive = keys["Rotate Right"];
        playerMovementController.verticalPositive = keys["Up"];
        playerMovementController.verticalNegative = keys["Down"];
        playerCombatController.mainWeaponKey = keys["Fire Main"];
        playerCombatController.leftWeaponKey = keys["Fire Left"];
        playerCombatController.rightWeaponKey = keys["Fire Right"];
        playerMovementController.speedBurstKey = keys["Speed Burst"];
    }
}
