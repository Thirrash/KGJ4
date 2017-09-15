using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IDestroyable
{
    public float cost;

    private const float EPSILON = 0.001f;
    private bool bIsDestroyed = false;

    public void OnStandingInExplosionRange(Bomb b) {
        if (bIsDestroyed) { return; }

        if (cost > 0) {
            float impactDistance = Vector3.Magnitude(b.transform.position - transform.position);
            float loss = b.Damage * (b.BlastRadius - impactDistance) / b.BlastRadius;
            loss = loss > cost ? cost : loss;
            cost -= loss;

            CostManager.Instance.AddCost(loss);
        }

        if (cost <= EPSILON) {
            DestroyBuilding();
            bIsDestroyed = true;
        }
    }

    void DestroyBuilding() {

    }

    void Start() {

    }

    void Update() {

    }
}
