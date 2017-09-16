using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fresher : MonoBehaviour
{
    public float PullRadius = 60.0f;
    public float LiveTime = 6.0f;
    public Waypoint ClosestWaypoint;

    private List<MotherBehaviour> MothersInRange = new List<MotherBehaviour>();

    void Start() {
        StartCoroutine(DestroyAfterTime());
        Collider[] mothers = Physics.OverlapSphere(transform.position, PullRadius, 1 << Statics.MotherLayer);
        foreach (Collider c in mothers) {
            MotherBehaviour mb = c.GetComponent<MotherBehaviour>();
            if (mb != null) {
                mb.OnFresherSpawnedInRange(this);
                MothersInRange.Add(mb);
            }
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == Statics.MotherLayer) {
            MotherBehaviour winnerMother = collision.gameObject.GetComponent<MotherBehaviour>();
            foreach (MotherBehaviour m in MothersInRange) {
                if (m != winnerMother)
                    m.ChaseAnotherMother(winnerMother);
            }

            winnerMother.ReturnToNormal();
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyAfterTime() {
        yield return new WaitForSeconds(LiveTime);
        Destroy(gameObject);
    }
}
