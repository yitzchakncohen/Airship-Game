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
    private Dictionary<Controls, KeyCode> keys = new Dictionary<Controls, KeyCode>();
    // Start is called before the first frame update
    void Awake()
    {
        keys.Add(Controls.Forward, defaultForwardPositive);
        keys.Add(Controls.Reverse, defaultForwardNegative);
        keys.Add(Controls.RotateLeft, defaultRotateNegative);
        keys.Add(Controls.RotateRight, defaultRotatePositive);
        keys.Add(Controls.Up, defaultVerticalPositive);
        keys.Add(Controls.Down, defaultVerticalNegative);
        keys.Add(Controls.FireMain, defaultMainWeaponKey);
        keys.Add(Controls.FireLeft, defaultLeftWeaponKey);
        keys.Add(Controls.FireRight, defaultRightWeaponKey);
        keys.Add(Controls.SpeedBurst, defaultSpeedBurstKey);
    }
    public void SetKeyCode(Controls control, KeyCode key)
    {
        keys[control] = key;
    }

    public KeyCode GetKeyCode(Controls control)
    {
        return keys[control];
    }
    public void SetKeysToDefault()
    {
        SetKeyCode(Controls.Forward, KeyCode.W);
        SetKeyCode(Controls.Reverse, KeyCode.S);
        SetKeyCode(Controls.RotateLeft, KeyCode.A);
        SetKeyCode(Controls.RotateRight, KeyCode.D);
        SetKeyCode(Controls.Up, KeyCode.UpArrow);
        SetKeyCode(Controls.Down, KeyCode.DownArrow);
        SetKeyCode(Controls.FireMain, KeyCode.Space);
        SetKeyCode(Controls.FireLeft, KeyCode.LeftArrow);
        SetKeyCode(Controls.FireRight, KeyCode.RightArrow);
        SetKeyCode(Controls.SpeedBurst, KeyCode.LeftShift);
    }

    public void UpdateControls()
    {
        SetKeyCode(Controls.Forward, keys[Controls.Forward]);
        SetKeyCode(Controls.Reverse, keys[Controls.Reverse]);
        SetKeyCode(Controls.RotateLeft, keys[Controls.RotateLeft]);
        SetKeyCode(Controls.RotateRight, keys[Controls.RotateRight]);
        SetKeyCode(Controls.Up, keys[Controls.Up]);
        SetKeyCode(Controls.Down, keys[Controls.Down]);
        SetKeyCode(Controls.FireMain, keys[Controls.FireMain]);
        SetKeyCode(Controls.FireLeft, keys[Controls.FireLeft]);
        SetKeyCode(Controls.FireRight, keys[Controls.FireRight]);
        SetKeyCode(Controls.SpeedBurst, keys[Controls.SpeedBurst]);
    }
}
