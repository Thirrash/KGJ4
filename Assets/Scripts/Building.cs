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
        Transform[] destroyed = GetComponentsInChildren<Transform>(true);
        foreach (Transform t in destroyed) {
            if (t.gameObject != gameObject) {
                t.gameObject.SetActive(true);
                Debug.Log(t.gameObject.name);
            }
        }

        GetComponent<MeshRenderer>().enabled = false;
    }

    void Start() {
        Transform[] destroyed = GetComponentsInChildren<Transform>();
        foreach (Transform t in destroyed)
            if (t.gameObject != gameObject)
                t.gameObject.SetActive(false);
    }

    void Update() {

    }
}
