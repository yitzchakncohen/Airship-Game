using UnityEngine;

namespace AirShip.Inventory
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] float maxShieldPoints = 100f;
        [SerializeField] private float shieldPoints;
        [SerializeField] GameObject shieldDownFX;
        [SerializeField] float fXTime = 1f;
        [SerializeField] float recahrgeRate = 0.05f;
        public bool shieldDown;
        CapsuleCollider shieldCollider;
        MeshRenderer meshRenderer;
        public GameObject shieldHost;
        // Start is called before the first frame update
        void Start()
        {
            shieldPoints = 0;
            shieldCollider = GetComponent<CapsuleCollider>();
            meshRenderer = GetComponent<MeshRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if(shieldPoints < 0 && shieldDown == false)
            {
                ShieldDisabled();
            }else if(shieldPoints > 0 && shieldDown == true)
            {
                ShieldEnabled();
            }
            
            if(shieldPoints<maxShieldPoints)
            {
                shieldPoints += recahrgeRate;
            }
        }

        private void ShieldEnabled()
        {
            shieldDown = false;
            shieldCollider.enabled = true;
            meshRenderer.enabled = true;       
        }

        private void ShieldDisabled()
        {
            GameObject shieldFXObject = Instantiate(shieldDownFX, transform.position, transform.rotation);
            shieldDown = true;
            shieldCollider.enabled = false;
            meshRenderer.enabled = false;
            Destroy(shieldFXObject, fXTime);
        }

        public void TakeDamage(float damage)
        {
            shieldPoints -= damage;
        }

        public float getMaxShield()
        {
            return maxShieldPoints;
        }

        public float GetShieldPoints()
        {
            return shieldPoints;
        }

        public bool IsShieldEnabled()
        {
            return shieldDown;
        }

        public void SetShieldHost(GameObject host)
        {
            shieldHost = host;
        }

    }
}
