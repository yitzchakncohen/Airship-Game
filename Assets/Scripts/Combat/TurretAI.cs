using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Turret))]
public class TurretAI : MonoBehaviour
{
    GameObject player;
    [SerializeField] float defenseRadius = 200f;
    [SerializeField] float reloadTime = 1f;
    public bool isReloading = false;
    bool inRange = false;
    
    Turret turret;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        turret = GetComponent<Turret>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if(distanceToPlayer < defenseRadius)
        {
            transform.LookAt(player.transform);
            if(isReloading == false)
            {
                turret.Fire();
                isReloading = true;            
                StartCoroutine("Reload");   
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, defenseRadius);
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
        yield return null;
    }
}
