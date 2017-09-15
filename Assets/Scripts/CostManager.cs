using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostManager : MonoBehaviour
{
    public static CostManager Instance;
    public float TotalCost;

    void Awake() {
        TotalCost = 0;
        Instance = this;
    }

    void Update() {

    }

    public void AddCost(float cost) {
        if (cost > 0) {
            TotalCost += cost;
        }
    }
}
