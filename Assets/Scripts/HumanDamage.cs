using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanDamage : MonoBehaviour, IDamageable {

    public float cost;

    public void OnHit(Bomb bomb)
    {
        CostManager.Instance.AddCost(cost);
        Destroy(gameObject);
    }
}
