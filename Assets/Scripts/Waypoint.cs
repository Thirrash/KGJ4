using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public List<Waypoint> Links = new List<Waypoint>();
    public bool bIsOnRoad = false;

    void Start() {
        WaypointHolder.Instance.AddWaypoint(this);
    }
}
