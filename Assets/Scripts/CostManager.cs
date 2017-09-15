using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostManager : MonoBehaviour {

    public static CostManager Instance;
    public float totalCost { get; private set; }

	void Awake () {
        totalCost = 0;
        Instance = this;
	}
	
	void Update () {
		
	}

    public void AddCost(float cost)
    {
        if (cost > 0)
        {
            totalCost += cost;
        }
    }
}
