using UnityEngine;
using System.Collections;

public class resizeCube : MonoBehaviour {


	private GameObject cube; 
	private GameObject cube1; 
	private float currentScale;
	private float scaleTo;
	private float deltaScale;
	public Texture myTexture; 
	public Texture myTexture1; 

	private Vector3 spawnPos = new Vector3(-56F, 31F, 508F);

	void Update () {
		if (Mathf.Abs(scaleTo-currentScale) > 0.01F)
		{
			currentScale = scaleObjectToCurrentScale (currentScale, scaleTo, deltaScale);
		}
		moveObject (cube, currentScale);
		scaleObject(cube, new Vector3(currentScale, currentScale, currentScale));

	}

	float getDeltaScale(float scaleToX, float currentScaleX)
	{
		deltaScale = scaleToX - currentScaleX;
		return deltaScale;
	}

	float scaleObjectToCurrentScale(float currentScaleX, float scaleToX, float deltaScaleX)
	{
		currentScaleX = deltaScaleX*0.01F + currentScaleX;
		return currentScaleX;
	}

	void moveObject(GameObject cube, float currentScaleX)
	{
		cube.transform.position = spawnPos - new Vector3 (currentScaleX , -currentScaleX + 1F, currentScaleX - 1F) / 2F;
	}
	void scaleObject(GameObject cube, Vector3 scaleCube)
	{
		cube.transform.localScale = scaleCube;
	}

	GameObject spawnCube (Vector3 cubePosition)
	{
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.position = cubePosition;
		cube.GetComponent<Renderer>().material.mainTexture = myTexture;

		return cube;
	}


}
