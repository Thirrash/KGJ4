using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public static float BlastRadius = 30.0f;
    public float TimeToExplosion = 4.0f;

    void Start() {
        BombHolder.Instance.AddAtomicBomb(this);
        StartCoroutine(Explode());
    }

    private void OnDestroy() {
        BombHolder.Instance.RemoveAtomicBomb(this);
    }

    void Update() {

    }

    private IEnumerator Explode() {
        yield return new WaitForSeconds(TimeToExplosion);
        Collider[] hitEntities = Physics.OverlapSphere(transform.position, BlastRadius, Statics.DestroyableLayers);
        foreach (Collider c in hitEntities) {
            IDestroyable des = c.gameObject.GetComponent<IDestroyable>();
            if (des != null)
                des.OnStandingInExplosionRange(this);
        }

        ParticleSystem pSystem = GetComponent<ParticleSystem>();
        pSystem.Play();
        yield return new WaitForSeconds(pSystem.duration + pSystem.startLifetime);
        Destroy(gameObject);
    }
}
