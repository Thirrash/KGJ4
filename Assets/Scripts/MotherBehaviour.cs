﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MotherBehaviour : MonoBehaviour, IDestroyable
{
    public static float DistanceToWaypoint = 4f;
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
            if (Physics.Raycast(transform.position, CurrentWaypoint.transform.position - transform.position, 5.0f, 1 << Statics.HomelessLayer, QueryTriggerInteraction.Collide)) {
                Waypoint newWp = WaypointHolder.Instance.GetLinkedWaypointOther(LastWaypoint, CurrentWaypoint);
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
