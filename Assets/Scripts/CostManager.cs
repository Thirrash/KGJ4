using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostManager : MonoBehaviour
{
    public static CostManager Instance;
    public float TotalCost;
    public event Action OnTotalCostChanged;

    void Awake() {
        TotalCost = 0;
        Instance = this;
    }

    private void Start() {
        OnTotalCostChanged += () => { };
    }

    void Update() {

    }

    public void AddCost(float cost) {
        if (cost > 0) {
            TotalCost += cost;
            OnTotalCostChanged();
        }
    }
}
