using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverHandler : MonoBehaviour
{
    public static float AmericanCooldown = 10.0f;
    public static DriverHandler Instance;
    public List<DriverBehaviour> DriverList = new List<DriverBehaviour>();

    public GameObject AmericanPrefab;

    public void RemoveDriver(DriverBehaviour db) {
        DriverList.Remove(db);
    } 

    public void AddDriver(DriverBehaviour db) {
        DriverList.Add(db);
    }

    void Start() {
        Instance = this;
        StartCoroutine(SpawnAmericans());
    }

    void Update() {

    }

    private IEnumerator SpawnAmericans() {
        yield return new WaitForSeconds(10.0f);
        while (true) {
            if (DriverList.Count == 0) {
                yield break;
            }

            int index = Random.Range(0, DriverList.Count - 1);
            GameObject americans = Instantiate<GameObject>(AmericanPrefab, DriverList[index].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(AmericanCooldown);
        }
    }
}
