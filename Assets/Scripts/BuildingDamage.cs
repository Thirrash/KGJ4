using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDamage : MonoBehaviour, IDamageable {

    public float cost;

    private const float EPSILON = 0.001f;
    private bool bIsDestroyed = false;

    public void OnHit(Bomb bomb)
    {
        if (bIsDestroyed) { return; }

        if (cost > 0)
        {
            float impactDistance = Vector3.Magnitude(bomb.transform.position - transform.position);
            float loss = bomb.damage * (bomb.radius - impactDistance) / bomb.radius;
            loss = loss > cost ? cost : loss;
            cost -= loss;

            CostManager.Instance.AddCost(loss);
        }

        if (cost <= EPSILON)
        {
            DestroyBuilding();
            bIsDestroyed = true;
        }
    }

    void DestroyBuilding()
    {

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
