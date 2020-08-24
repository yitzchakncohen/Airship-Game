using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] float waypointTolerance = 1f;
    [SerializeField] float patrolSpeed = 100f;

    private Rigidbody rigidBody;
    Vector3 guardPosition;
    Vector3 nextPosition;
    int currentWaypointIndex = 0; 
    // Start is called before the first frame update
    void Start()
    {
        guardPosition = transform.position;
        nextPosition = guardPosition;
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Patrol();
    }

    private void Patrol()
    {
        if(patrolPath != null)
        {
            if (AtWaypoint())
            {
                CycleWaypoint();
            }
            nextPosition = GetCurrentWaypoint();
            rigidBody.transform.LookAt(nextPosition);
            float forwardMovementX = (rigidBody.transform.forward.x*patrolSpeed)*Time.fixedDeltaTime;
            float forwardMovementY = (rigidBody.transform.forward.y*patrolSpeed)*Time.fixedDeltaTime;
            float forwardMovementZ = (rigidBody.transform.forward.z*patrolSpeed)*Time.fixedDeltaTime;
            Vector3 movementVector = new Vector3(forwardMovementX, forwardMovementY, forwardMovementZ);
            rigidBody.velocity = movementVector*100;
        }

    }

    private Vector3 GetCurrentWaypoint()
    {
        return patrolPath.GetWaypoint(currentWaypointIndex);
    }

    private bool AtWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
        return distanceToWaypoint < waypointTolerance;
    }

    private void CycleWaypoint()
    {
        currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }
}
