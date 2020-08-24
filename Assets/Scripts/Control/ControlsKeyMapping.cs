using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AirShip.Control;

public class ControlsKeyMapping : MonoBehaviour
{
    //Movement Controls
    [SerializeField] public KeyCode defaultForwardPositive;
    [SerializeField] public KeyCode defaultForwardNegative;
    [SerializeField] public KeyCode defaultRotatePositive;
    [SerializeField] public KeyCode defaultRotateNegative;
    [SerializeField] public KeyCode defaultVerticalPositive;
    [SerializeField] public KeyCode defaultVerticalNegative;

    //Weapon and Ability Controls
    [SerializeField] public KeyCode defaultSpeedBurstKey;
    [SerializeField] public KeyCode defaultMainWeaponKey;
    [SerializeField] public KeyCode defaultLeftWeaponKey;
    [SerializeField] public KeyCode defaultRightWeaponKey;
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    // Start is called before the first frame update
    void Awake()
    {
        keys.Add("Forward", defaultForwardPositive);
        keys.Add("Reverse", defaultForwardNegative);
        keys.Add("Rotate Left", defaultRotateNegative);
        keys.Add("Rotate Right", defaultRotatePositive);
        keys.Add("Up", defaultVerticalPositive);
        keys.Add("Down", defaultVerticalNegative);
        keys.Add("Fire Main", defaultMainWeaponKey);
        keys.Add("Fire Left", defaultLeftWeaponKey);
        keys.Add("Fire Right", defaultRightWeaponKey);
        keys.Add("Speed Burst", defaultSpeedBurstKey);
    }
    public void SetKeyCode(string control, KeyCode key)
    {
        keys[control] = key;
    }

    public KeyCode GetKeyCode(string control)
    {
        return keys[control];
    }
    public void SetKeysToDefault()
    {
        SetKeyCode("Forward", KeyCode.W);
        SetKeyCode("Reverse", KeyCode.S);
        SetKeyCode("Rotate Left", KeyCode.A);
        SetKeyCode("Rotate Right", KeyCode.D);
        SetKeyCode("Up", KeyCode.UpArrow);
        SetKeyCode("Down", KeyCode.DownArrow);
        SetKeyCode("Fire Main", KeyCode.Space);
        SetKeyCode("Fire Left", KeyCode.LeftArrow);
        SetKeyCode("Fire Right", KeyCode.RightArrow);
        SetKeyCode("Speed Burst", KeyCode.LeftShift);
    }

    public void UpdateControls()
    {
        SetKeyCode("Forward", keys["Forward"]);
        SetKeyCode("Reverse", keys["Reverse"]);
        SetKeyCode("Rotate Left", keys["Rotate Left"]);
        SetKeyCode("Rotate Right", keys["Rotate Right"]);
        SetKeyCode("Up", keys["Up"]);
        SetKeyCode("Down", keys["Down"]);
        SetKeyCode("Fire Main", keys["Fire Main"]);
        SetKeyCode("Fire Left", keys["Fire Left"]);
        SetKeyCode("Fire Right", keys["Fire Right"]);
        SetKeyCode("Speed Burst", keys["Speed Burst"]);
    }
}
