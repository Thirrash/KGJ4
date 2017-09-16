using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointHolder : MonoBehaviour
{
    public static WaypointHolder Instance;
    public List<Waypoint> AllWaypoints = new List<Waypoint>();
    public GameObject MotherGo;

    public void AddWaypoint(Waypoint W) {
        AllWaypoints.Add(W);
    }

    public Waypoint GetLinkedWaypointOther(Waypoint CurrentWaypoint, Waypoint WaypointWeDontWant) {
        List<Waypoint> list = CurrentWaypoint.Links;
        Waypoint w;
        while (w = list[Random.Range(0, list.Count)])
            if (w != WaypointWeDontWant)
                return w;

        return null;
    }

    public Waypoint GetLinkedWaypointOtherRoad(Waypoint CurrentWaypoint, Waypoint WaypointWeDontWant) {
        List<Waypoint> list = CurrentWaypoint.Links;
        Waypoint w;
        while (w = list[Random.Range(0, list.Count)])
            if (w != WaypointWeDontWant && w.bIsOnRoad)
                return w;

        return null;
    }

    public Waypoint GetLinkedWaypointFarthestFromBombRoad(Waypoint CurrentWaypoint) {
        Waypoint retWaypoint = null;
        List<Waypoint> list = CurrentWaypoint.Links;
        List<Bomb> bombsList = BombHolder.Instance.AtomicBombs;

        float maximumDistance = 0.0f;
        foreach (Waypoint w in list) {
            if (!w.bIsOnRoad)
                continue;

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

    public Waypoint GetLinkedWaypointNotOnRoad(Waypoint CurrentWaypoint) {
        Waypoint retWaypoint = null;
        List<Waypoint> list = CurrentWaypoint.Links;
        List<Bomb> bombsList = BombHolder.Instance.AtomicBombs;

        float maximumDistance = 0.0f;
        foreach (Waypoint w in list) {
            float waypointMaxDistance = 0.0f;

            if (waypointMaxDistance > maximumDistance && !w.bIsOnRoad) {
                maximumDistance = waypointMaxDistance;
                retWaypoint = w;
            }
        }

        if (retWaypoint == null)
            retWaypoint = GetLinkedWaypointOther(CurrentWaypoint, null);

        return retWaypoint;
    }

    public Waypoint GetLinkedWaypointClosestFromBomb(Waypoint CurrentWaypoint) {
        Waypoint retWaypoint = null;
        List<Waypoint> list = CurrentWaypoint.Links;
        List<Bomb> bombsList = BombHolder.Instance.AtomicBombs;

        float minimumDistance = 0.0f;
        foreach (Waypoint w in list) {
            float waypointMinDistance = 0.0f;
            foreach (Bomb b in bombsList) {
                if (Vector3.Distance(w.transform.position, b.transform.position) < waypointMinDistance)
                    waypointMinDistance = Vector3.Distance(w.transform.position, b.transform.position);
            }

            if (waypointMinDistance < minimumDistance) {
                minimumDistance = waypointMinDistance;
                retWaypoint = w;
            }
        }

        if (retWaypoint == null)
            retWaypoint = GetLinkedWaypointOther(CurrentWaypoint, null);

        return retWaypoint;
    }

    void Start() {
        Instance = this;
        Invoke("SpawnSpam", 3.0f);
    }

    private void SpawnSpam() {
        foreach (Waypoint w in AllWaypoints) {
            GameObject go = GameObject.Instantiate<GameObject>(MotherGo, w.transform.position, Quaternion.identity);
            go.GetComponent<MotherBehaviour>().CurrentWaypoint = w;
        }
    }
}
