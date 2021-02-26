using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {
    public float speed;

    void Start () {
        if (transform.parent.localPosition.y > 0.2f)
            speed *= -1;
    }

    void FixedUpdate () {
        transform.Rotate (0, 0, speed);
    }
}