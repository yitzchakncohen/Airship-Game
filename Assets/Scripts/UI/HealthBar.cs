using System.Collections;
using System.Collections.Generic;
using AirShip.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace AirShip.UI
{
    public class HealthBar : MonoBehaviour
    {
        GameObject player;
        Health playerHealth;
        [SerializeField] GameObject fillerBar;
        Image fillerBarImage;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<Health>();
            fillerBarImage = fillerBar.GetComponent<Image>(); 
        }

        // Update is called once per frame
        void Update()
        {
            fillerBarImage.fillAmount = playerHealth.GetHealth() / playerHealth.GetMaxHealth(); 
        }
    }
}
