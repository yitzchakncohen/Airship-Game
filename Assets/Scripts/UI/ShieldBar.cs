using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{
    GameObject player;
    Shield playerShield;
    [SerializeField] GameObject fillerBar;
    Image fillerBarImage;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fillerBarImage = fillerBar.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        playerShield = player.GetComponentInChildren<Shield>();
        fillerBarImage.fillAmount = playerShield.GetShieldPoints() / playerShield.getMaxShield();
    }
}
