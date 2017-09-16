using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ONRBehaviour : MonoBehaviour, IDestroyable
{
    public static float DistanceToWaypoint = 4f;
    public float MovementSpeed = 1.0f;
    public Waypoint CurrentWaypoint;

    private Waypoint LastWaypoint;
    private Waypoint PreLastWaypoint;
    private NavMeshAgent NavAgent;
    private Coroutine MoveCoroutine;

    public void OnStandingInExplosionRange(Bomb b) {
        
    }

    private void Awake() {
        NavAgent = GetComponent<NavMeshAgent>();
    }

    void Start() {
        MoveCoroutine = StartCoroutine(Move());
        StartCoroutine(CheckForHomeless());
    }

    void Update() {

    }

    private Waypoint GetCurrentWaypoint() {
        return CurrentWaypoint;
    }

    private void SetCurrentWaypoint(Waypoint w) {
        CurrentWaypoint = w;
    }

    private IEnumerator CheckForHomeless() {
        while (true) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, CurrentWaypoint.transform.position - transform.position, out hit, 1.5f, 1 << Statics.HomelessLayer, QueryTriggerInteraction.Collide)) {
                yield return new WaitForSeconds(1.5f);
                Destroy(hit.collider.gameObject);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator Move() {
        NavAgent.SetDestination(CurrentWaypoint.transform.position);
        while (true) {
            if (Vector3.Distance(transform.position, GetCurrentWaypoint().transform.position) < DistanceToWaypoint) {
                Waypoint wp;
                if (LastWaypoint.bIsOnRoad)
                    wp = WaypointHolder.Instance.GetLinkedWaypointNotOnRoad(CurrentWaypoint);
                else
                    wp = WaypointHolder.Instance.GetLinkedWaypointFarthestFromBomb(CurrentWaypoint);

                LastWaypoint = CurrentWaypoint;
                if (wp != PreLastWaypoint) {
                    SetCurrentWaypoint(wp);
                } else {
                    SetCurrentWaypoint(WaypointHolder.Instance.GetLinkedWaypointOther(CurrentWaypoint, wp));
                }

                PreLastWaypoint = LastWaypoint;
                NavAgent.SetDestination(GetCurrentWaypoint().transform.position);
            }

            yield return null;
        }
    }
}
