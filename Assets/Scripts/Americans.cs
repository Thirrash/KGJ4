using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Americans : MonoBehaviour
{
    public float DropTime = 3.0f;
    public GameObject Oil;
    public GameObject Marine1, Marine2, Marine3;

    void Start() {
        Oil.SetActive(true);
        Marine1.SetActive(true);
        Marine2.SetActive(true);
        Marine3.SetActive(true);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;

        StartCoroutine(DropMarines());
    }

    void Update() {

    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == Statics.MotherLayer)
            other.gameObject.GetComponent<MotherBehaviour>().bIsShielded = true;
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.layer == Statics.MotherLayer)
            other.gameObject.GetComponent<MotherBehaviour>().bIsShielded = false;
    }

    private IEnumerator DropMarines() {
        float yPos = 100.0f;
        float decValue = (100.0f - 1.5f) / DropTime;
        float timer = 0.0f;
        while (timer < DropTime) {
            Marine1.transform.localPosition = new Vector3(Marine1.transform.localPosition.x, yPos, Marine1.transform.localPosition.z);
            Marine2.transform.localPosition = new Vector3(Marine2.transform.localPosition.x, yPos, Marine2.transform.localPosition.z);
            Marine3.transform.localPosition = new Vector3(Marine3.transform.localPosition.x, yPos, Marine3.transform.localPosition.z);

            yPos -= decValue * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }

        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<SphereCollider>().enabled = true;
    }
}
