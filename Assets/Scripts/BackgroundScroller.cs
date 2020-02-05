using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    public float speed;
    public float tileSizeZ;

    private Vector3 startPos;

    void Start() {
        startPos = transform.position;
    }

    void Update () {
        float newPos = Mathf.Repeat(Time.time * speed, tileSizeZ);
        transform.position = startPos + Vector3.forward * newPos;
    }
}
