using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

	public GameObject thePlatform;
	//public GameObject[] thePlatforms;
	public Transform generationPoint;
	public float distanceBetween;
	public float distanceBtwMin;
	public float distanceBtwMax;

	public ObjectPooler[] theObjectPools;

	private float platformWidth;
	private int platformSelector;
	private float[] platformWidths;




	// Use this for initialization
	void Start () {
		platformWidth = thePlatform.GetComponent<BoxCollider2D> ().size.x;
		platformWidths = new float[theObjectPools.Length];
		for (int i = 0; i < theObjectPools.Length; i++) {
			platformWidths [i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D> ().size.x;
			//platformWidths [i] = theObjectPools [i].pooledObject.gameObject.GetComponentInChildren<BoxCollider2D> ().size.x;
		}
	}

	// Update is called once per frame
	void Update () {

		if (transform.position.x < generationPoint.position.x) {

			distanceBetween = Random.Range (distanceBtwMin, distanceBtwMax);
			platformSelector = Random.Range (0, theObjectPools.Length);

			transform.position = new Vector3 (transform.position.x + (platformWidths[platformSelector]/2) + distanceBetween, transform.position.y, transform.position.z);




			//Instantiate (thePlatforms[platformSelector], transform.position, transform.rotation);
			GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject ();
			newPlatform.transform.position = transform.position;
			newPlatform.transform.rotation = transform.rotation;
			newPlatform.SetActive (true);

			transform.position = new Vector3 (transform.position.x + (platformWidths[platformSelector]/2), transform.position.y, transform.position.z);

		}
		
	}
}
