using UnityEngine;
using GameDevTV.Inventories;
using TMPro;

public class ShipMenu : MonoBehaviour
{
    Equipment playerEquipment;
    [SerializeField] GameObject equipPointsTextObject;
    TextMeshProUGUI equipPointsText;

    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        playerEquipment = player.GetComponent<Equipment>();
        equipPointsText = equipPointsTextObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        equipPointsText.SetText("Equip Points: " 
                + playerEquipment.EquipPointsTotal() 
                + " / "
                + playerEquipment.GetEquipPoints());
    }
}
