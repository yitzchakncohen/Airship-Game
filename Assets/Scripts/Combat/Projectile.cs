using UnityEngine;
using UnityEngine.Events;
public class Projectile : MonoBehaviour
{

    [SerializeField] float speed = 1;
    // [SerializeField] bool isHoming = false;
    [SerializeField] GameObject hitEffect = null;
    [SerializeField] float maxLifeTime = 10;
    [SerializeField] GameObject[] destroyOnHit = null;
    [SerializeField] float lifeAfterImpact = 10f;
    [SerializeField] UnityEvent onHit;
    Health target = null;
    [SerializeField] float damage = 0;

    private void Start()
    {
        // transform.LookAt(GetAimLocation());
    }

    void Update()
    {
        // if (target == null) return;

        // if (isHoming && !target.IsDead())
        // {
        //     transform.LookAt(GetAimLocation());
        // }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // public void SetTarget(Health target, GameObject instigator, float damage)
    // {
    //     this.target = target;
    //     this.damage = damage;

    //     Destroy(gameObject, maxLifeTime);
    // }

    private Vector3 GetAimLocation()
    {
        // CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        // if (targetCapsule == null)
        // {
        //     return target.transform.position;
        // }
        // return target.transform.position + Vector3.up * targetCapsule.height / 2;
        return Vector3.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Health>())
        {
            target = other.gameObject.GetComponent<Health>();
        }
        else
        {
            return;
        }

        if (target.IsDead()) return;

        target.GetComponent<Health>().TakeDamage(damage);

        speed = 0;

        onHit.Invoke();

        if (hitEffect != null)
        {
            Instantiate<GameObject>(hitEffect, gameObject.transform.position, transform.rotation);
        }

        foreach (GameObject toDestroy in destroyOnHit)
        {
            Destroy(toDestroy);
        }

        Destroy(gameObject, lifeAfterImpact);
    }
}

