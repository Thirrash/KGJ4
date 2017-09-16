using System;
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

    private bool bIsFighting = false;

    public void OnStandingInExplosionRange(Bomb b) {
        Destroy(gameObject);
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

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == Statics.MotherLayer) {
            if (!bIsFighting)
                StartCoroutine(FightMother(collision.gameObject.GetComponent<MotherBehaviour>(), 1.5f, (b) => { bIsFighting = b; }));
        }
    }

    private IEnumerator FightMother(MotherBehaviour mb, float Time, Action<bool> bIsFIghtingWrapper) {
        bIsFIghtingWrapper(true);
        float prevSpeed = NavAgent.speed;
        NavAgent.speed = 0.0f;
        mb.GetComponent<NavMeshAgent>().speed = 0.0f;
        yield return new WaitForSeconds(Time);
        mb.OnONRDeath();
        NavAgent.speed = prevSpeed;
        bIsFIghtingWrapper(false);
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
                if (LastWaypoint && LastWaypoint.bIsOnRoad)
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
