using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float verticalSpeed = 1f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float burstMulitplier = 2f;
    [SerializeField] float burstLength = 3f;
    [SerializeField] float burstCoolDownLength = 10f;
    [SerializeField] float climbAngle = 15f;
    float climbAngleGrowing = 0f;
    [SerializeField] float climbAngleSpeed = 0.5f;
    [SerializeField] GameObject playerModel;
    [SerializeField] GameObject burstEffect;
    // [SerializeField] Animator animator;

    [SerializeField] public bool controllerEnabled = true;
    [SerializeField] public KeyCode speedBurstKey;
    [SerializeField] GameObject speedBurstUI;

    private Rigidbody rigidBody;
    private Vector3 inputs = Vector3.zero;
    private FaceNorth compassUI;
    // private bool isGrounded = true;
    private bool burstActive = false;
    // float height = 0;
    float burstSpeed;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        compassUI = FindObjectOfType<FaceNorth>();
        burstEffect.SetActive(false);
        speedBurstUI.SetActive(false);
    }

    void Update()
    {
        // Control Inputs
        inputs = Vector3.zero;
        inputs.x = Input.GetAxis("Horizontal");
        inputs.z = Input.GetAxis("Vertical");
        inputs.y = Input.GetAxis("Height");
        if(Input.GetKeyDown(speedBurstKey) && !burstActive)
        {
            StartCoroutine(SpeedBurst());
        }
        compassUI.ApplyRotation(transform.eulerAngles.y);
    }

    void FixedUpdate()
    {
        //Create movement vector
        float forwardMovement = inputs.z * speed * Time.fixedDeltaTime;
        float verticalMovement = inputs.y * verticalSpeed *Time.fixedDeltaTime;
        Vector3 movementVector = new Vector3(rigidBody.transform.forward.x*forwardMovement, verticalMovement, rigidBody.transform.forward.z*forwardMovement);
        float rotationAmount = inputs.x * rotationSpeed * Time.fixedDeltaTime;

        //Make ship planar, using freeze rotation right now
        // rigidBody.MoveRotation(Quaternion.Euler(0, transform.eulerAngles.y, 0));
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

    //Disable controller
    public void ToggleControl(bool onOrOff)
    {
        controllerEnabled = onOrOff;
    }

    IEnumerator SpeedBurst()
    {
        burstActive = true;
        burstSpeed = speed*burstMulitplier;
        float baseSpeed = speed;
        speed = burstSpeed;
        burstEffect.SetActive(true);
        yield return StartCoroutine(burstWait());
        speed = baseSpeed;
        burstEffect.SetActive(false);
        speedBurstUI.SetActive(true);
        speedBurstUI.GetComponent<SpeedBurstUI>().UpdateCircle(burstCoolDownLength);
        yield return StartCoroutine(burstCoolDown());
        speedBurstUI.SetActive(false);
        burstActive = false;
    }

    IEnumerator burstWait()
    {
        yield return new WaitForSeconds(burstLength);
    }

    IEnumerator burstCoolDown()
    {
        yield return new WaitForSeconds(burstCoolDownLength);
    }
    
}
