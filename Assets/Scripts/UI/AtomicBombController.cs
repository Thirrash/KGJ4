using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtomicBombController : MonoBehaviour
{
    public Image FillingImage;
    private float CooldownTime;

    void Start() {
        SpawnManager.Instance.OnAvailabilityChanged += OnAtomicCooldownChanged;
        CooldownTime = SpawnManager.Instance.WeaponCooldowns[1];
    }

    private void OnAtomicCooldownChanged(int WeaponNr) {
        if (WeaponNr != 1)
            return;

        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine() {
        FillingImage.fillAmount = 0.0f;
        float timer = 0.0f;
        while (timer < CooldownTime) {
            FillingImage.fillAmount = timer / CooldownTime;
            timer += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        FillingImage.fillAmount = 1.0f;
    }
}
