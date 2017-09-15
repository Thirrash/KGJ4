using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointHolder : MonoBehaviour
{
    public static WaypointHolder Instance;
    public List<Waypoint> AllWaypoints = new List<Waypoint>();

    public void AddWaypoint(Waypoint W) {
        AllWaypoints.Add(W);
    }

    public Waypoint GetLinkedWaypointOther(Waypoint CurrentWaypoint, Waypoint WaypointWeDontWant) {
        List<Waypoint> list = CurrentWaypoint.Links;
        foreach (Waypoint w in list)
            if (w != WaypointWeDontWant)
                return w;

        return null;
    }

    public Waypoint GetLinkedWaypointFarthestFromBomb(Waypoint CurrentWaypoint) {
        Waypoint retWaypoint = null;
        List<Waypoint> list = CurrentWaypoint.Links;
        List<Bomb> bombsList = BombHolder.Instance.AtomicBombs;

        float maximumDistance = 0.0f;
        foreach (Waypoint w in list) {
            float waypointMaxDistance = 0.0f;
            foreach (Bomb b in bombsList) {
                if (Vector3.Distance(w.transform.position, b.transform.position) > waypointMaxDistance)
                    waypointMaxDistance = Vector3.Distance(w.transform.position, b.transform.position);
            }

            if (waypointMaxDistance > maximumDistance) {
                maximumDistance = waypointMaxDistance;
                retWaypoint = w;
            }
        }

        if (retWaypoint == null)
            retWaypoint = GetLinkedWaypointOther(CurrentWaypoint, null);

        return retWaypoint;
    }

    void Start() {
        Instance = this;
    }
}
