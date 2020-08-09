using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float range;
    [SerializeField] TurretSize turretSize;
    public Health turretParent;

    // Vector3[] positions = new Vector3[2];

    //Aim Assist
    //LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        turretParent = GetComponentInParent<Health>();
        // lineRenderer = GetComponent<LineRenderer>();
        // for (int i = 0; i < positions.Length; i++)
        // {
        //     positions[i] = new Vector3(0,0,0);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        // positions[0] = transform.position;
        // positions[1] = transform.position + transform.forward*1000;
        // lineRenderer.SetPositions(positions);
    }

    public void Fire()
    {
        GameObject shot = Instantiate(projectile, this.transform.position, this.transform.rotation);
        Projectile projectileComponent = shot.GetComponent<Projectile>();
        projectileComponent.SetRange(range);
        projectileComponent.SetOrigin(turretParent.name, turretParent.tag);
    }
}
