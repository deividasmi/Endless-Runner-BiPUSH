using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

	public float backgroundSize;
	private Transform cameraTransform;
	private Transform[] layers;
	private float viewZone = 30;
	private int leftIndex;
	private int rightIndex;
	private float lastCameraX;
	private float startingXposition;
	private GameManager theGameManager;
	public float fixedY;

	public float paralaxSpeed;
	public bool scrolling;
	public bool paralax;

	// Use this for initialization
	void Start () {
		cameraTransform = Camera.main.transform;
		lastCameraX = cameraTransform.position.x;
		layers = new Transform[transform.childCount];
		theGameManager = FindObjectOfType<GameManager> ();
		for (int i = 0; i < transform.childCount; i++) {
			layers [i] = transform.GetChild (i);
			layers [i].position = new Vector3 (transform.position.x, fixedY, transform.position.z);
			//Debug.Log("layers pos y " + layers[i].position.y + " camera :" + cameraTransform.position.y);
			leftIndex = 0;
			rightIndex = layers.Length - 1;
		}
		startingXposition = layers [0].position.x;
	}

	private void Update(){

		float deltaX = cameraTransform.position.x - lastCameraX;
		transform.position += Vector3.right * (deltaX * paralaxSpeed);
		lastCameraX = cameraTransform.position.x;

		if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
			ScrollRight ();
		if (theGameManager.backroundReset) {
			ResetBackground();
		}
	}
	
	private void ScrollRight(){
		//int lastLeft = leftIndex;
		//layers [leftIndex].position = Vector3.right * (layers [rightIndex].position.x + backgroundSize);
		layers [leftIndex].position = new Vector3 (layers [rightIndex].position.x + backgroundSize, fixedY, transform.position.z);
		//Debug.Log("layers pos y " + layers[leftIndex].position.y + " camera :" + cameraTransform.position.y);
		rightIndex = leftIndex;
		leftIndex++;
		if (leftIndex == layers.Length)
			leftIndex = 0;
	}

	public void ResetBackground(){
		//int lastRight = rightIndex;
		//layers [leftIndex].position = Vector3.right * (layers [rightIndex].position.x + backgroundSize);
		//layers [rightIndex].position = new Vector3 (layers [leftIndex].position.x + backgroundSize, cameraTransform.position.y, transform.position.z);
		//Debug.Log("layers pos y " + layers[leftIndex].position.y + " camera :" + cameraTransform.position.y);
		//leftIndex = rightIndex;
		//rightIndex--;
	    //if (rightIndex < 0)
		//	rightIndex = layers.Length - 1;
		layers [0].position = new Vector3(startingXposition, fixedY, transform.position.z);

		float addedXpostion = startingXposition;
		for(int i = 1; i < layers.Length; i++){
			addedXpostion +=  backgroundSize;
			layers [i].position = new Vector3(addedXpostion, fixedY, transform.position.z);
			//Debug.Log ("add: " + addedXpostion + " x:" + layers [i].position.x);
		}
		theGameManager.backroundReset = false;
	}
}
