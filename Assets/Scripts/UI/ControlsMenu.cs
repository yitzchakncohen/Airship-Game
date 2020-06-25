using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ControlsMenu : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    public TextMeshProUGUI forward, reverse, rotateLeft, rotateRight, up, down, mainFire, leftFire, rightFire; 

    [SerializeField] GameObject controlsCanvas;
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

        forward.text = keys["Forward"].ToString();
        reverse.text = keys["Reverse"].ToString();
        rotateLeft.text = keys["Rotate Left"].ToString();
        rotateRight.text = keys["Rotate Right"].ToString();
        up.text = keys["Up"].ToString();
        down.text = keys["Down"].ToString();
        mainFire.text = keys["Fire Main"].ToString();
        leftFire.text = keys["Fire Left"].ToString();
        rightFire.text = keys["Fire Right"].ToString();

    }

    public void SetForwardKey()
    {
        
    }
}
