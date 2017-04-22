using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeCube : MonoBehaviour {

    private float currentScale = 0.2f;
    private float scaleTo = 0.2f;
    private float scaleFactor = 1f;
    private Vector3 spawnPos;
    private float scaleTimer = 1f;
    private int Counter = 150;

    // Use this for initialization
    void Start() {
        spawnPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (Counter < 150) {
            currentScale += scaleFactor;
            transform.localScale = Vector3.one * currentScale;
            //moveObject();
            Counter++;
        }
    }


    void moveObject() {
        transform.position = spawnPos - new Vector3(currentScale, -currentScale, currentScale);
    }

    public void SetScaleTo(float s) {
        scaleTo = s;
        scaleFactor = (scaleTo - currentScale) / 150f;
        Counter = 0;
    }
}
