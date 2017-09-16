using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DriverBehaviour : MonoBehaviour, IDestroyable
{
    public static float DistanceToWaypoint = 10.0f;
    public float MovementSpeed = 1.0f;
    public Waypoint CurrentWaypoint;

    private Waypoint LastWaypoint;
    private Waypoint PreLastWaypoint;
    private NavMeshAgent NavAgent;
    private Coroutine MoveCoroutine;

    public void OnStandingInExplosionRange(Bomb b) {
        Destroy(gameObject);
    }

    private void Awake() {
        NavAgent = GetComponent<NavMeshAgent>();
    }

    void Start() {
        MoveCoroutine = StartCoroutine(Move());
        StartCoroutine(CheckForHomeless());

        DriverHandler.Instance.AddDriver(this);
    }

    void Update() {

    }

    private void OnDestroy() {
        StopAllCoroutines();
    }

    private Waypoint GetCurrentWaypoint() {
        return CurrentWaypoint;
    }

    private void SetCurrentWaypoint(Waypoint w) {
        CurrentWaypoint = w;
    }

    private IEnumerator CheckForHomeless() {
        while (true) {
            if (Physics.Raycast(transform.position, CurrentWaypoint.transform.position - transform.position, 5.0f, 1 << Statics.HomelessLayer, QueryTriggerInteraction.Collide)) {
                Waypoint newWp = WaypointHolder.Instance.GetLinkedWaypointOtherRoad(LastWaypoint, CurrentWaypoint);
                NavAgent.SetDestination(newWp.transform.position);
                Waypoint tmp = CurrentWaypoint;
                SetCurrentWaypoint(newWp);
                PreLastWaypoint = LastWaypoint;
                LastWaypoint = tmp;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator Move() {
        NavAgent.SetDestination(CurrentWaypoint.transform.position);
        Debug.Log(NavAgent.destination.ToString());
        while (true) {
            Debug.Log(Vector3.Distance(transform.position, GetCurrentWaypoint().transform.position));
            if (Vector3.Distance(transform.position, GetCurrentWaypoint().transform.position) < DistanceToWaypoint) {
                Waypoint wp = WaypointHolder.Instance.GetLinkedWaypointFarthestFromBombRoad(CurrentWaypoint);
                LastWaypoint = CurrentWaypoint;
                if (wp != PreLastWaypoint) {
                    SetCurrentWaypoint(wp);
                } else {
                    Waypoint currentWaypoint = WaypointHolder.Instance.GetLinkedWaypointOtherRoad(CurrentWaypoint, wp);
                    if (currentWaypoint != null)
                        SetCurrentWaypoint(currentWaypoint);
                    else
                        SetCurrentWaypoint(wp);
                }

                PreLastWaypoint = LastWaypoint;
                NavAgent.SetDestination(GetCurrentWaypoint().transform.position);
            }

            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == Statics.MotherLayer) {
            collision.gameObject.GetComponent<MotherBehaviour>().OnDriverDeath();
        }
    }
}
