using System.Collections;
using AirShip.Inventory;
using UnityEngine;

[RequireComponent(typeof(Turret))]
public class TurretAI : MonoBehaviour
{
    GameObject player;
    [SerializeField] float defenseRadius = 200f;
    [SerializeField] float reloadTime = 1f;
    public bool isReloading = false;
    bool lineOfSight = false;
    RaycastHit[] hits;
    
    Turret turret;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        turret = GetComponent<Turret>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        if(!player) return;
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer < defenseRadius)
        {
            transform.LookAt(player.transform);
            CheckLineOfSight();
            if(lineOfSight)
            {
                Fire();
            }
        }
    }

    private void CheckLineOfSight()
    {
        //Raycast to see if there is a clear line of fire to the player.
        hits = Physics.RaycastAll(transform.position, transform.forward, turret.range);
        lineOfSight = true;

        //Check result for obstacles    
        foreach (RaycastHit hit in hits)
        {
            //Ignore players
            if(hit.collider.tag != player.tag)
            {
                //Ignore self
                if(hit.transform != this.transform)
                {
                    //Check if hit is closer than the player
                    if(Vector3.Distance(hit.transform.position, this.transform.position) < Vector3.Distance(player.transform.position, this.transform.position))
                    {
                        lineOfSight = false;
                    }
                }
            }
        }
    }

    private void Fire()
    {
        if (isReloading == false)
        {
            turret.Fire();
            isReloading = true;
            StartCoroutine("Reload");
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
