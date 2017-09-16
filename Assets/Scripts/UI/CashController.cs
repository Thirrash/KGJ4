using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashController : MonoBehaviour
{
    public Text CashText;

    void Start() {
        CashText.text = "0.00 PLN";
        CostManager.Instance.OnTotalCostChanged += OnCostChanged;
    }

    private void OnCostChanged() {
        CashText.text = CostManager.Instance.TotalCost.ToString("F2") + " PLN";
    }
}
