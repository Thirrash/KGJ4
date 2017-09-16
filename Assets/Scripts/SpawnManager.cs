using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    public event Action<int> OnAvailabilityChanged;
    public int NoMothers;

    public Dictionary<int, float> WeaponCooldowns = new Dictionary<int, float>();
    public Dictionary<int, bool> WeaponAvailability = new Dictionary<int, bool>();
    public Dictionary<int, string> WeaponResources = new Dictionary<int, string>();
    public Dictionary<int, string> WeaponShadows = new Dictionary<int, string>();
    public int CurrentWeapon = 1;

    public bool bIsToRotate = false;

    public bool bIsOnGlobalCooldown = false;
    public float GlobalCooldown = 0.3f;
    public Coroutine GlobalCooldownCoroutine;

    private Vector3 CurrentSpawnPoint = Vector3.zero;
    private GameObject CurrentShadow;

    public void RemoveMother() {
        WaypointHolder.Instance.SpawnMother();
    }

    public void SpawnWeapon() {
        if (CurrentSpawnPoint == Vector3.zero)
            return;

        if (!WeaponAvailability[CurrentWeapon] || bIsOnGlobalCooldown)
            return;

        GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(WeaponResources[CurrentWeapon]), CurrentSpawnPoint, Quaternion.Euler(new Vector3(0.0f, (bIsToRotate) ? 90.0f : 0.0f, 0.0f)));

        bIsOnGlobalCooldown = true;
        if (GlobalCooldownCoroutine != null)
            StopCoroutine(GlobalCooldownCoroutine);

        GlobalCooldownCoroutine = StartCoroutine(ActivateGlobalCooldown());
        StartCoroutine(TickWeaponCooldown(CurrentWeapon));
        OnAvailabilityChanged(CurrentWeapon);
    }

    void Start() {
        Instance = this;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        WeaponResources.Add(1, "Weapons/AtomicBomb");
        WeaponResources.Add(2, "Weapons/AirRaid");
        WeaponResources.Add(3, "Weapons/Fresher");
        WeaponResources.Add(4, "Weapons/Homeless");

        WeaponShadows.Add(1, "Shadows/AtomicBomb");
        WeaponShadows.Add(2, "Shadows/AirRaid");
        WeaponShadows.Add(3, "Shadows/Fresher");
        WeaponShadows.Add(4, "Shadows/Homeless");

        WeaponCooldowns.Add(1, 6.0f);
        WeaponCooldowns.Add(2, 8.0f);
        WeaponCooldowns.Add(3, 6.0f);
        WeaponCooldowns.Add(4, 5.0f);

        WeaponAvailability.Add(1, true);
        WeaponAvailability.Add(2, true);
        WeaponAvailability.Add(3, true);
        WeaponAvailability.Add(4, true);

        ChangeCurrentShadow();
        OnAvailabilityChanged += (x) => { };
    }

    void Update() {
        if (bIsOnGlobalCooldown)
            return;

        if (Input.GetMouseButtonDown(0)) {
            SpawnWeapon();
        }

        if (Input.GetMouseButtonDown(1)) {
            bIsToRotate = !bIsToRotate;
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
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            CurrentWeapon = 4;
            ChangeCurrentShadow();
        }

        CurrentSpawnPoint = GetSpawnPosition();

        if (CurrentSpawnPoint != Vector3.zero)
            CurrentShadow.transform.position = CurrentSpawnPoint;
        else
            CurrentShadow.transform.position = new Vector3(0.0f, -50.0f, 0.0f);

        CurrentShadow.transform.rotation = Quaternion.Euler(new Vector3(0.0f, (bIsToRotate) ? 90.0f : 0.0f, 0.0f));

        if (!WeaponAvailability[CurrentWeapon]) {
            MeshRenderer[] renderers = CurrentShadow.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer m in renderers)
                m.enabled = false;
        } else {
            MeshRenderer[] renderers = CurrentShadow.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer m in renderers)
                m.enabled = true;
        }
    }

    private IEnumerator ActivateGlobalCooldown() {
        yield return new WaitForSeconds(GlobalCooldown);
        FinishGlobalCooldown();
    }

    private IEnumerator TickWeaponCooldown(int WeaponNr) {
        WeaponAvailability[WeaponNr] = false;
        yield return new WaitForSeconds(WeaponCooldowns[WeaponNr]);
        WeaponAvailability[WeaponNr] = true;
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
        if (!Physics.Raycast(ray, out hit, 150.0f, 1 << Statics.FloorLayer | 1 << Statics.BuildingLayer | 1 << Statics.AmericanLayer))
            return Vector3.zero;

        if (hit.collider.gameObject.layer == Statics.BuildingLayer && CurrentWeapon != 1 && CurrentWeapon != 2)
            return Vector3.zero;

        if (hit.collider.gameObject.layer == Statics.AmericanLayer)
            return Vector3.zero;

        return hit.point;
    }
}
