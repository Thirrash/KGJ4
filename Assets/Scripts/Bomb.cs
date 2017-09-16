using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public bool bIsAirRaid = false;
    public float Damage = 10.0f;
    public float BlastRadius = 10.0f;
    public float TimeToExplosion = 4.0f;
    public float InitialYPos = 100.0f;

    private LineRenderer LineR;
    private ParticleSystem ParticleS;

    private void Awake() {
        LineR = GetComponent<LineRenderer>();
        ParticleS = GetComponent<ParticleSystem>();
    }

    void Start() {
        BombHolder.Instance.AddAtomicBomb(this);
        ParticleSystem.ShapeModule shape = ParticleS.shape;
        shape.radius = BlastRadius;

        int lineCount = 300;
        LineR.positionCount = lineCount;
        float incrValue = 2.0f * Mathf.PI / (float)lineCount;
        float blast2 = BlastRadius * BlastRadius;
        float currAngle = 0.0f;
        for (int i = 0; i < lineCount; i++) {
            float x = Mathf.Sin(currAngle) * BlastRadius + transform.position.x;
            float y;
            float z = Mathf.Cos(currAngle) * BlastRadius + transform.position.z;

            RaycastHit hit;
            if (Physics.Raycast(new Vector3(x, 100.0f, z), new Vector3(0.0f, -150.0f, 0.0f), out hit, 150.0f, 1 << Statics.BuildingLayer))
                y = hit.point.y + 0.3f;
            else
                y = 0.3f;

            LineR.SetPosition(i, new Vector3(x, y, z));
            currAngle += incrValue;
        }

        StartCoroutine(FallDown());
        StartCoroutine(Explode());
    }

    private void OnDestroy() {
        BombHolder.Instance.RemoveAtomicBomb(this);
    }

    void Update() {

    }

    private IEnumerator FallDown() {
        transform.position = new Vector3(transform.position.x, InitialYPos, transform.position.z);

        float positionDecrement = InitialYPos / TimeToExplosion;
        float timer = 0.0f;
        while (timer < TimeToExplosion) {
            transform.position -= new Vector3(0.0f, positionDecrement * Time.deltaTime, 0.0f);
            yield return null;
        }
    }

    private IEnumerator Explode() {
        yield return new WaitForSeconds(TimeToExplosion);
        Collider[] hitEntities = Physics.OverlapSphere(new Vector3(transform.position.x, 0.0f, transform.position.z), BlastRadius, Statics.DestroyableLayers);
        foreach (Collider c in hitEntities) {
            if (c.gameObject.layer == Statics.ONRLayer) {
                c.gameObject.GetComponent<IDestroyable>().OnStandingInExplosionRange(this);
                Destroy(gameObject);
            }
        }

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
