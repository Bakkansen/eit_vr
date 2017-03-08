using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleController : MonoBehaviour {

    private float currentScale = 1.0f;
    private float scaleTo = 1.0f;
    private Vector3 spawnPos;

    public void Start() {
        spawnPos = this.transform.position;
    }

    private void Update() {
        if (Mathf.Abs(scaleTo - currentScale) > 0.01F) {
            currentScale = scaleObjectToCurrentScale();
        }
        moveObject(currentScale);
        scaleObject(Vector3.one * currentScale);
    }

    private float getDeltaScale() {        
        return scaleTo - currentScale;
    }

    private float scaleObjectToCurrentScale() {
        return getDeltaScale() * 0.01F + currentScale;
    }

    private void moveObject(float currentScaleX) {
        this.transform.position = spawnPos - new Vector3(currentScaleX, -currentScaleX + 1F, currentScaleX - 1F) / 2F;
    }
    private void scaleObject(Vector3 scaleCube) {
        this.transform.localScale = scaleCube;
    }

    public void setScaleTo(float s) {
        this.scaleTo = s;
//         Debug.Log("Scale: " + this.scaleTo);
    }


}
