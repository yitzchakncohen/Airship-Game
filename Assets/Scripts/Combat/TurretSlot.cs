using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSlot : MonoBehaviour
{
    [SerializeField] TurretSize turretSize;
    [SerializeField] GameObject turret;
    [SerializeField] TurretLocations turretLocation;
    // Start is called before the first frame update
    void Start()
    {
        GameObject localTurret = Instantiate(turret, this.transform.position, this.transform.rotation, this.transform);
    }

    // Update is called once per frame
    public TurretLocations GetLocation()
    {
        return turretLocation;
    }
}
