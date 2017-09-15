using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Dictionary<int, string> WeaponResources = new Dictionary<int, string>();
    public int CurrentWeapon = 1;

    public bool bIsOnGlobalCooldown = false;
    public float GlobalCooldown = 0.5f;
    public Coroutine GlobalCooldownCoroutine;

    public void SpawnWeapon() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, 150.0f, 1 << Statics.FloorLayer | 1 << Statics.BuildingLayer))
            return;

        Vector3 spawnPoint = hit.point;
        GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(WeaponResources[CurrentWeapon]), spawnPoint, Quaternion.identity);

        bIsOnGlobalCooldown = true;
        if (GlobalCooldownCoroutine != null)
            StopCoroutine(GlobalCooldownCoroutine);

        GlobalCooldownCoroutine = StartCoroutine(ActivateGlobalCooldown());
    }

    void Start() {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        WeaponResources.Add(1, "Weapons/AtomicBomb");
    }

    void Update() {
        if (bIsOnGlobalCooldown)
            return;

        if (Input.GetMouseButtonDown(0)) {
            SpawnWeapon();
        }
    }

    private IEnumerator ActivateGlobalCooldown() {
        yield return new WaitForSeconds(GlobalCooldown);
        FinishGlobalCooldown();
    }

    private void FinishGlobalCooldown() {
        bIsOnGlobalCooldown = false;
    }
}
