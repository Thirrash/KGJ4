using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MotherBehaviour : MonoBehaviour, IDestroyable
{
    public static float DistanceToWaypoint = 1.0f;
    public float MovementSpeed = 1.0f;
    public Waypoint CurrentWaypoint;

    private Waypoint LastWaypoint;
    private Waypoint PreLastWaypoint;
    private NavMeshAgent NavAgent;
    private Coroutine MoveCoroutine;

    public void ReturnToNormal() {
        MoveCoroutine = StartCoroutine(Move());
    }

    public void ChaseAnotherMother(MotherBehaviour m) {
        StartCoroutine(ChaseMotherCoroutine(m));
    }

    public void OnFresherSpawnedInRange(Fresher f) {
        if (MoveCoroutine != null)
            StopCoroutine(MoveCoroutine);

        NavAgent.SetDestination(f.transform.position);
    }

    public void OnStandingInExplosionRange(Bomb b) {
        Destroy(gameObject);
    }

    private void Awake() {
        NavAgent = GetComponent<NavMeshAgent>();
    }

    void Start() {
        MoveCoroutine = StartCoroutine(Move());
    }

    void Update() {

    }

    private Waypoint GetCurrentWaypoint() {
        return CurrentWaypoint;
    }

    private void SetCurrentWaypoint(Waypoint w) {
        CurrentWaypoint = w;
    }

    private IEnumerator Move() {
        NavAgent.SetDestination(CurrentWaypoint.transform.position);
        while (true) {
            if (Vector3.Distance(transform.position, GetCurrentWaypoint().transform.position) < DistanceToWaypoint) {
                Waypoint wp = WaypointHolder.Instance.GetLinkedWaypointFarthestFromBomb(CurrentWaypoint);
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

    private IEnumerator ChaseMotherCoroutine(MotherBehaviour m) {
        float timer = 0.0f;
        while (timer < 5.0f) {
            NavAgent.SetDestination(m.transform.position);
            timer += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        ReturnToNormal();
    }
}
