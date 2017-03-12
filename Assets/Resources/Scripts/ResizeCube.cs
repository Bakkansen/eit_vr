using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeCube : MonoBehaviour {

    private float currentScale = 1f;
    private float scaleTo = 1f;
    private Vector3 spawnPos;

	// Use this for initialization
	void Start () {
        spawnPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        currentScale = (scaleTo - currentScale) * 0.01F + currentScale;
        transform.localScale = Vector3.one * currentScale;
        moveObject();
    }

    void moveObject() {
        transform.position = spawnPos - new Vector3(currentScale, -currentScale + 1F, currentScale - 1F) / 2F;
    }

    public void SetScaleTo(float s) {
        scaleTo = s;
    }
}
