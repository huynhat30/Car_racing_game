using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNode : MonoBehaviour
{
    [Header("This is the waypoint we are going towards, bot yet reached")]

    public float minDistanceToReachWaypoint = 5;

    public WaypointNode[] nextWaypointNode;
}
