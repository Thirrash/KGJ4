using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Dictionary<int, string> WeaponResources = new Dictionary<int, string>();
    public Dictionary<int, string> WeaponShadows = new Dictionary<int, string>();
    public int CurrentWeapon = 1;

    public bool bIsOnGlobalCooldown = false;
    public float GlobalCooldown = 0.5f;
    public Coroutine GlobalCooldownCoroutine;

    private Vector3 CurrentSpawnPoint = Vector3.zero;
    private GameObject CurrentShadow;

    public void SpawnWeapon() {
        if (CurrentSpawnPoint == Vector3.zero)
            return;

        GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(WeaponResources[CurrentWeapon]), CurrentSpawnPoint, Quaternion.identity);

        bIsOnGlobalCooldown = true;
        if (GlobalCooldownCoroutine != null)
            StopCoroutine(GlobalCooldownCoroutine);

        GlobalCooldownCoroutine = StartCoroutine(ActivateGlobalCooldown());
    }

    void Start() {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        WeaponResources.Add(1, "Weapons/AtomicBomb");
        WeaponResources.Add(3, "Weapons/Fresher");

        WeaponShadows.Add(1, "Shadows/AtomicBomb");
        WeaponShadows.Add(3, "Shadows/Fresher");

        ChangeCurrentShadow();
    }

    void Update() {
        if (bIsOnGlobalCooldown)
            return;

        if (Input.GetMouseButtonDown(0)) {
            SpawnWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            CurrentWeapon = 1;
            ChangeCurrentShadow();
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            CurrentWeapon = 2;
            ChangeCurrentShadow();
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            CurrentWeapon = 3;
            ChangeCurrentShadow();
        }

        CurrentSpawnPoint = GetSpawnPosition();

        if (CurrentSpawnPoint != Vector3.zero)
            CurrentShadow.transform.position = CurrentSpawnPoint;
        else
            CurrentShadow.transform.position = new Vector3(0.0f, -50.0f, 0.0f);
    }

    private IEnumerator ActivateGlobalCooldown() {
        yield return new WaitForSeconds(GlobalCooldown);
        FinishGlobalCooldown();
    }

    private void FinishGlobalCooldown() {
        bIsOnGlobalCooldown = false;
    }

    private void ChangeCurrentShadow() {
        if (CurrentShadow != null)
            Destroy(CurrentShadow);

        CurrentShadow = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(WeaponShadows[CurrentWeapon]), CurrentSpawnPoint, Quaternion.identity);
    }

    private Vector3 GetSpawnPosition() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, 150.0f, 1 << Statics.FloorLayer | 1 << Statics.BuildingLayer))
            return Vector3.zero;

        if (hit.collider.gameObject.layer == Statics.BuildingLayer && CurrentWeapon != 1 && CurrentWeapon != 2)
            return Vector3.zero;

        return hit.point;
    }
}
