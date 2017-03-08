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

	void Start () {

		cube = spawnCube (spawnPos);
		cube1 = spawnCube (new Vector3(-56F, 31F, 508F));
		scaleObject(cube, new Vector3(1F, 1F, 1F));
		scaleObject(cube1, new Vector3(1F, 1F, 1F));
	}

	void Update () {
	
		if(Input.GetKeyDown("1")){
			scaleTo = quizOptionsNr1(1);
			getDeltaScale(scaleTo, currentScale);
		}
		if(Input.GetKeyDown("2")){
			scaleTo = quizOptionsNr1(2);
			getDeltaScale(scaleTo, currentScale);
		}
		if(Input.GetKeyDown("3")){
			scaleTo = quizOptionsNr1(3);
			getDeltaScale(scaleTo, currentScale);
		}
		if(Input.GetKeyDown("4")){
			scaleTo = quizOptionsNr1(4);
			getDeltaScale(scaleTo, currentScale);
		}
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

	float quizOptionsNr1(int option)            // First quiz "Hva blir volumet i dm³?"
	{											// 1m³ = 1 * 10³ dm³ 
		float[] myOptions;
		myOptions = new float[5]; 
		myOptions [1] = 10F;
		myOptions [2] = 1.0F;
		myOptions [3] = 0.1F;
		myOptions [4] = 0.01F;
		
		return 100F * myOptions [option];
	}

	float quizOptionsNr2(int option) 			// Second quiz "Hva blir volumet i cm³?"
	{											// 1m³ = 1 * 10⁶ m³ 
		float[] myOptions;
		myOptions = new float[5]; 
		myOptions [1] = 1F;
		myOptions [2] = 0.1F;
		myOptions [3] = 0.01F;
		myOptions [4] = 0.001F;
		
		return 100F * myOptions [option];
	}

	float quizOptionsNr3(int option) 			// Third quiz "Hvor mange liter støpemasse brukte egypterene til å støpe en stein?"
	{											// 1l = 1dm³
		float[] myOptions;					
		myOptions = new float[5]; 
		myOptions [1] = 1F;
		myOptions [2] = 0.1F;
		myOptions [3] = 0.01F;
		myOptions [4] = 0.001F;
		
		return 100F * myOptions [option];
	}


}
