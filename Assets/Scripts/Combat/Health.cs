using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float healthPoints = 100;
    [SerializeField] float timeBeforeDestroy = 1f;
    [SerializeField] GameObject deathFX;
    bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(healthPoints < 0)
        {
            DestroyTarget();
        }     
    }

    public void TakeDamage(float damage)
    {
        healthPoints -= damage;
    }

    public bool IsDead()
    {
        return dead;
    }

    private void DestroyTarget()
    {
        Instantiate(deathFX, transform.position, transform.rotation);
        dead = true;
        Destroy(gameObject, timeBeforeDestroy);
    }
}
