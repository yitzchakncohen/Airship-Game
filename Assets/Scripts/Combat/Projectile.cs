using AirShip.Inventory;
using UnityEngine;
using UnityEngine.Events;

namespace AirShip.Combat
{
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
        [SerializeField] string originTeam;
        SphereCollider sphereCollider;
        MeshRenderer meshRenderer;
        float maxLifeTime;

        private void Start()
        {
            // transform.LookAt(GetAimLocation()); //add back in when I want homing misiles
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

        public void SetOrigin(string name, string team)
        {
            origin = name;
            originTeam = team;
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
            //Check for shield impact
            if (other.GetComponent<Shield>())
            {
                shieldOfImpact = other.gameObject.GetComponent<Shield>();
                //Check if shield is on the origin gameObject
                if(shieldOfImpact.shieldHost.name != origin)
                {
                    //Check if the shield is on the same team
                    if(shieldOfImpact.tag != originTeam)
                    {
                        shieldOfImpact.TakeDamage(damage);
                        //Destroy Projectile
                        HitEffect();
                        DestroyProjectile();
                    }
                    return;
                }
            }

            //Check if hit the Terrain
            if (other.name == "Terrain")
            {
                HitEffect();
                DestroyProjectile();
                return;
            }

            //Check for health impact
            if (other.GetComponent<Health>())
            {
                target = other.gameObject.GetComponent<Health>();
                if (target.IsDead()) return;
                //check if health is on the origin opbject
                if(origin != other.name)
                {
                    //check if health is on the same team.
                    if(target.tag != originTeam)
                    {
                        target.GetComponent<Health>().TakeDamage(damage);
                    }
                    HitEffect();
                    DestroyProjectile();
                            speed = 0;
                    //TODO onhit?
                    // onHit.Invoke();
                }

            }
            return;
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
}

