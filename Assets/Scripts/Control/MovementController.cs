using System.Collections;
using UnityEngine;
using GameDevTV.Inventories;
using AirShip.Inventory;

namespace AirShip.Control
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        [SerializeField] float verticalSpeed = 1f;
        [SerializeField] float rotationSpeed = 1f;
        [SerializeField] float burstMulitplier = 2f;
        [SerializeField] float defaultBurstMultiplier = 2f;
        [SerializeField] float burstLength = 3f;
        [SerializeField] float defaultBurstlength = 3f;
        [SerializeField] float burstCoolDownLength = 10f;
        [SerializeField] float defaultBurstCoolDownLength = 10f;
        [SerializeField] float climbAngle = 15f;
        string burstFXName = "SpeedBurstEffect";
        float climbAngleGrowing = 0f;
        float axisCalibration = 0.05f;
        [SerializeField] float climbAngleSpeed = 0.5f;
        [SerializeField] GameObject playerModel;
        [SerializeField] GameObject burstEffectObject;
        [SerializeField] GameObject defaultBurstEffect;
        // [SerializeField] Animator animator;

        [SerializeField] public bool controllerEnabled = true;
        ControlsKeyMapping controls;
        [SerializeField] GameObject speedBurstUI;
        
        Equipment equipment;
        BoostItem boostItem;
        private Rigidbody rigidBody;
        private Vector3 inputs = Vector3.zero;
        private float compassRotation;
        // private bool isGrounded = true;
        private bool burstActive = false;
        // float height = 0;
        float burstSpeed;

        void Start()
        {
            //Get controls component
            controls = GameObject.FindObjectOfType<ControlsKeyMapping>();
            //Set rigidBody componenet
            rigidBody = GetComponent<Rigidbody>();
            //Connect equipment to speed burst.
            equipment = GetComponent<Equipment>();
            equipment.equipmentUpdated += speedBurstUpdate;
            
            //Speed burst setup
            burstEffectObject.SetActive(false);
            speedBurstUI.SetActive(false);
            GameObject speedBurstEffect = Instantiate(defaultBurstEffect, burstEffectObject.transform);
            speedBurstEffect.name = burstFXName; 
        }

        void Update()
        {
            // Control Inputs
            inputs.x = CalculateInputAxis(Input.GetKey(controls.GetKeyCode("Rotate Right")), Input.GetKey(controls.GetKeyCode("Rotate Left")), inputs.x);
            inputs.z = CalculateInputAxis(Input.GetKey(controls.GetKeyCode("Forward")), Input.GetKey(controls.GetKeyCode("Reverse")), inputs.z);
            inputs.y = CalculateInputAxis(Input.GetKey(controls.GetKeyCode("Up")), Input.GetKey(controls.GetKeyCode("Down")), inputs.y); 
            
            if(Input.GetKeyDown(controls.GetKeyCode("Speed Burst")) && !burstActive)
            {
                StartCoroutine(SpeedBurst());
            }
            compassRotation = transform.eulerAngles.y;
        }

        void FixedUpdate()
        {
            //Create movement vector
            float forwardMovement = inputs.z * speed * Time.fixedDeltaTime;
            float verticalMovement = inputs.y * verticalSpeed *Time.fixedDeltaTime;
            Vector3 movementVector = new Vector3(rigidBody.transform.forward.x*forwardMovement, verticalMovement, rigidBody.transform.forward.z*forwardMovement);
            float rotationAmount = inputs.x * rotationSpeed * Time.fixedDeltaTime;

            //Make ship planar, using freeze rotation right now
            if (controllerEnabled && inputs != new Vector3(0,0,0))
            {
                //If controller is enabled, move player with MovePosition or Velocity or addForce? TODO
                // rigidBody.MovePosition(rigidBody.position + movementVector);
                rigidBody.velocity = movementVector * 100;
                // rigidBody.AddForce(movementVector*100);

                //Add rotation with RotateAround or Torue? TODO
                rigidBody.transform.RotateAround(rigidBody.transform.position, Vector3.up, rotationAmount);
                // rigidBody.AddTorque(transform.up * rotationAmount * 100);
                float movementSpeed = movementVector.magnitude;
                // animator.SetFloat("MoveSpeed", movementVector.magnitude*1.66f);

            }
            else
            {
                // animator.SetFloat("MoveSpeed", 0);
            }
            TiltShip(verticalMovement);
        }

        private void TiltShip(float verticalMovement)
        {
            //Tilt the ship while climbing
            if (verticalMovement > 0)
            {
                if (climbAngleGrowing < climbAngle)
                {
                    climbAngleGrowing += climbAngleSpeed;
                }
                playerModel.transform.eulerAngles = new Vector3(playerModel.transform.eulerAngles.x, playerModel.transform.eulerAngles.y, climbAngleGrowing);
            }
            else if (verticalMovement < 0)
            {
                if (climbAngleGrowing > -climbAngle)
                {
                    climbAngleGrowing -= climbAngleSpeed;
                }
                playerModel.transform.eulerAngles = new Vector3(playerModel.transform.eulerAngles.x, playerModel.transform.eulerAngles.y, climbAngleGrowing);
            }
            else
            {
                if (climbAngleGrowing > 0)
                {
                    climbAngleGrowing -= climbAngleSpeed;
                }
                else if (climbAngleGrowing < 0)
                {
                    climbAngleGrowing += climbAngleSpeed;
                }
                playerModel.transform.eulerAngles = new Vector3(playerModel.transform.eulerAngles.x, playerModel.transform.eulerAngles.y, climbAngleGrowing);
            }
        }


        //Check if player is on the ground, do I need this for this game?
        // private void isPlayerGrounded()
        // {
        //     LayerMask mask = LayerMask.GetMask("Terrain");
        //     if(Physics.CheckSphere(transform.position, 2, mask))
        //     {
        //         isGrounded = true;
        //         // animator.SetBool("Grounded", true);
        //     } 
        //     else
        //     {
        //         isGrounded = false;
        //         // animator.SetBool("Grounded", false);
        //     }
        //     // print(isGrounded);
        // }

        //Update player speed
        public void UpdateSpeed(float newspeed){
            speed = newspeed;
        }
        
        private void speedBurstUpdate()
        {
            boostItem = equipment.GetItemInSlot(EquipLocation.Boost) as BoostItem;
            if(boostItem)
            {
                burstMulitplier = boostItem.GetBurst();
                burstLength = boostItem.GetBurstLength();
                burstCoolDownLength = boostItem.GetBurstCoolDown();
                DestroyOldBurstEffect();
                GameObject speedBurstEffect = Instantiate(boostItem.GetBurstFX(), burstEffectObject.transform);
                speedBurstEffect.name = burstFXName;
            }
            else
            {
                burstMulitplier = defaultBurstMultiplier;
                burstLength = defaultBurstlength;
                burstCoolDownLength = defaultBurstCoolDownLength;
                DestroyOldBurstEffect();
                GameObject speedBurstEffect = Instantiate(defaultBurstEffect, burstEffectObject.transform);
                speedBurstEffect.name = burstFXName; 
            }
        }
        private void DestroyOldBurstEffect()
        {
            burstEffectObject.SetActive(true);
            GameObject oldFX = GameObject.FindWithTag(burstFXName);
            if(oldFX != null)
            {
                Destroy(oldFX);
            }
            burstEffectObject.SetActive(false);
        }

        //Disable controller
        public void ToggleControl(bool onOrOff)
        {
            controllerEnabled = onOrOff;
        }

        public float GetCompassRotation()
        {
            return compassRotation;
        }

        IEnumerator SpeedBurst()
        {
            burstActive = true;
            burstSpeed = speed*burstMulitplier;
            float baseSpeed = speed;
            speed = burstSpeed;
            burstEffectObject.SetActive(true);
            yield return StartCoroutine(BurstWait());
            speed = baseSpeed;
            burstEffectObject.SetActive(false);
            speedBurstUI.SetActive(true);
            yield return StartCoroutine(BurstCoolDown());
            speedBurstUI.SetActive(false);
            burstActive = false;
        }

        public float GetBurthCoolDown()
        {
            return burstCoolDownLength;
        }

        IEnumerator BurstWait()
        {
            yield return new WaitForSeconds(burstLength);
        }

        IEnumerator BurstCoolDown()
        {
            yield return new WaitForSeconds(burstCoolDownLength);
        }

        private float CalculateInputAxis(bool positiveValue, bool negativeValue, float axisValue)
        {
            if(positiveValue)
            {
                if(axisValue <= 1)
                {
                    axisValue += axisCalibration;
                }
            }

            if(negativeValue)
            {
                if(axisValue >= -1)
                {
                    axisValue -= axisCalibration;
                }
            }

            if(!positiveValue && axisValue > 0)
            {
                axisValue -= axisCalibration;
            }

            if(!negativeValue && axisValue < 0)
            {
                axisValue += axisCalibration;
            }

            if(axisValue < axisCalibration && axisValue > -axisCalibration)
            {
                axisValue = 0;
            }

            return axisValue;
        }
    }
}
