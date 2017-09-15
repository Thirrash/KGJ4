using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Waypoint : MonoBehaviour
{
    public List<Waypoint> Links = new List<Waypoint>();

    void Start() {
        WaypointHolder.Instance.AddWaypoint(this);
    }

    private void OnDrawGizmos() {
        GUIContent con = new GUIContent();
        con.text = gameObject.name;
        Handles.Label(transform.position, con);
    }
}
