using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CarAIHandler : MonoBehaviour
{
    public enum AIMode { followPlayer, followWaypoints, followMouse };

    [Header("AI settings")]
    public AIMode aiMode;
    public float maxSpeed = 16;
    public bool isAvoidingCars = true;
    [Range(0.0f, 1.0f)]
    public float skillLevel = 1.0f;

    //Local variables
    Vector3 targetPosition = Vector3.zero;
    Transform targetTransform = null;
    float orignalMaximumSpeed = 0;

    //Avoidance
    Vector2 avoidanceVectorLerped = Vector3.zero;

    //Waypoints
    WaypointNode currentWaypoint = null;
    WaypointNode previousWaypoint = null;
    WaypointNode[] allWayPoints;

    //Colliders
    PolygonCollider2D polygonCollider2D;

    //Components
    TopDownCarController topDownCarController;

    //Awake is called when the script instance is being loaded.
    void Awake()
    {
        topDownCarController = GetComponent<TopDownCarController>();
        allWayPoints = FindObjectsOfType<WaypointNode>();

        polygonCollider2D = GetComponentInChildren<PolygonCollider2D>();

        orignalMaximumSpeed = maxSpeed;
    }

    
    void Start()
    {
        
    }

    // Update is called once per frame and is frame dependent
    void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;

        switch (aiMode)
        {
            case AIMode.followPlayer:
                FollowPlayer();
                break;

            case AIMode.followWaypoints:
                FollowWaypoints();
                break;

        }

        inputVector.x = TurnTowardTarget();
        inputVector.y = ApplyThrottleOrBrake(inputVector.x);

        //Send the input to the car controller.
        topDownCarController.SetInputVector(inputVector);
    }

    void FollowPlayer()
    {
        if (targetTransform == null)
            targetTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (targetTransform != null)
            targetPosition = targetTransform.position;
    }

 
    void FollowWaypoints()
    {
        //Pick the cloesest waypoint if we don't have a waypoint set.
        if (currentWaypoint == null)
        {
            currentWaypoint = FindClosestWayPoint();
            previousWaypoint = currentWaypoint;
        }

        //Set the target on the waypoints position
        if (currentWaypoint != null)
        {
            
            targetPosition = currentWaypoint.transform.position;

           
            float distanceToWayPoint = (targetPosition - transform.position).magnitude;

            
            if (distanceToWayPoint <= currentWaypoint.minDistanceToReachWaypoint)
            {
               
                
                previousWaypoint = currentWaypoint;

                currentWaypoint = currentWaypoint.nextWaypointNode[Random.Range(0, currentWaypoint.nextWaypointNode.Length)];
            }
        }
    }


    WaypointNode FindClosestWayPoint()
    {
        return allWayPoints
            .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
            .FirstOrDefault();

    }

    float TurnTowardTarget()
    {
        Vector2 vectorToTarget = targetPosition - transform.position;
        vectorToTarget.Normalize();
       
        float angleToTarget = Vector2.SignedAngle(transform.up, vectorToTarget);
        angleToTarget *= -1;

        float steerAmount = angleToTarget / 45.0f;

        steerAmount = Mathf.Clamp(steerAmount, -1.0f, 1.0f);

        return steerAmount;
    }

    float ApplyThrottleOrBrake(float inputX)
    {

        float reduceSpeedDueToCornering = Mathf.Abs(inputX) / 1.0f;

        return 1.05f - reduceSpeedDueToCornering * skillLevel;
    }

}