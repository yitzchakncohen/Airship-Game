using UnityEngine;
using UnityEngine.Events;
public class Projectile : MonoBehaviour
{

    [SerializeField] float speed = 1;
    // [SerializeField] bool isHoming = false;
    [SerializeField] GameObject hitEffect = null;
    [SerializeField] GameObject[] destroyOnHit = null;
    [SerializeField] float lifeAfterImpact = 10f;
    [SerializeField] UnityEvent onHit;
    Health target = null;
    Shield shieldOfImpact = null;
    [SerializeField] float damage = 0;
    [SerializeField] string origin;
    SphereCollider sphereCollider;
    MeshRenderer meshRenderer;
    float maxLifeTime;

    private void Start()
    {
        // transform.LookAt(GetAimLocation());
        meshRenderer = GetComponent<MeshRenderer>();
        sphereCollider = GetComponent<SphereCollider>();
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

    public void SetRange(float range)
    {
        maxLifeTime = range / speed;
        Destroy(gameObject, maxLifeTime);
    }

    public void SetOrigin(string name)
    {
        origin = name;
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
        if (other.GetComponent<Shield>() && origin != "Player")
        {
            shieldOfImpact = other.gameObject.GetComponent<Shield>();
            shieldOfImpact.TakeDamage(damage);
            HitEffect();
            DestroyProjectile();
            return;
        }

        if (other.GetComponent<Health>() && origin != other.name)
        {
            target = other.gameObject.GetComponent<Health>();
        }
        else if (other.name == "Terrain")
        {
            HitEffect();
            DestroyProjectile();
            return;
        }
        else
        {
            return;
        }

        if (target.IsDead()) return;
        
        if(origin != other.name)
        {
            target.GetComponent<Health>().TakeDamage(damage);
        }

        speed = 0;

        //TODO onhit?
        onHit.Invoke();
        HitEffect();
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        foreach (GameObject toDestroy in destroyOnHit)
        {
            Destroy(toDestroy);
        }
        meshRenderer.enabled = false;
        sphereCollider.enabled = false;
        Destroy(gameObject, lifeAfterImpact);
    }

    private void HitEffect()
    {
        if (hitEffect != null)
        {
            GameObject hitEffectObject = Instantiate<GameObject>(hitEffect, gameObject.transform.position, transform.rotation);
            Destroy(hitEffectObject, lifeAfterImpact);
        }
    }
}

