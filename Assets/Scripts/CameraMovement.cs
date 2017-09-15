using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Boundaries
{
    public float xMin, xMax, yMin, yMax, zMin, zMax;

    public Vector3 Clamp(Vector3 vec) {
        return new Vector3(Mathf.Clamp(vec.x, xMin, xMax), Mathf.Clamp(vec.y, yMin, yMax), Mathf.Clamp(vec.z, zMin, zMax));
    }
}

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed;
    public float scrollSpeed;
    public Boundaries boundaries;

    void Start() {
        transform.position = boundaries.Clamp(transform.position);
    }

    void Update() {
        transform.position += new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed, -Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * scrollSpeed, Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed);
        transform.position = boundaries.Clamp(transform.position);
    }
}
