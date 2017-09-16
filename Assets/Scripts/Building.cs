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
            float loss = b.Damage > cost ? cost : b.Damage;
            cost -= loss;

            CostManager.Instance.AddCost(loss);
        }

        if (cost <= EPSILON) {
            DestroyBuilding();
            bIsDestroyed = true;
        }
    }

    void DestroyBuilding() {
        GameObject destroyed = GetComponentInChildren<Transform>().gameObject;
        destroyed.SetActive(true);
        GetComponent<MeshRenderer>().enabled = false;
    }

    void Start() {

    }

    void Update() {

    }
}
