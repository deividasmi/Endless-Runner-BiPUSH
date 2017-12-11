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


	private float minHeight;
	public Transform maxHeightPoint;
	private float maxHeight;
	public float maxHeightChange;
	private float heightChange;

	private CoinGenerator theCoinGenerator; 
	public float randomCoinTreshold;

	public float randomSpikeTreshold;
	public ObjectPooler spikePool;

	public float powerUpHeight;
	public ObjectPooler powerUpPool;
	public float powerUpTreshold;


	// Use this for initialization
	void Start () {
		//platformWidth = thePlatform.GetComponent<BoxCollider2D> ().size.x;
		platformWidths = new float[theObjectPools.Length];
		for (int i = 0; i < theObjectPools.Length; i++) {
			platformWidths [i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D> ().size.x;
			//platformWidths [i] = theObjectPools [i].pooledObject.gameObject.GetComponentInChildren<BoxCollider2D> ().size.x;
		}
		minHeight = transform.position.y;
		maxHeight = maxHeightPoint.transform.position.y;
		theCoinGenerator = FindObjectOfType<CoinGenerator> ();
	}

	// Update is called once per frame
	void Update () {

		if (transform.position.x < generationPoint.position.x) {

			distanceBetween = Random.Range (distanceBtwMin, distanceBtwMax);
			platformSelector = Random.Range (0, theObjectPools.Length);

			heightChange = transform.position.y + Random.Range (-maxHeightChange, maxHeightChange);


			if (heightChange > maxHeight) {
				heightChange = maxHeight;
			}else if(heightChange<minHeight){
				heightChange = minHeight;
			}

			//powerUpGeneration
			if(Random.Range (0f,100f) < powerUpTreshold){
				GameObject newPowerUp = powerUpPool.GetPooledObject ();
				newPowerUp.transform.position = transform.position + new Vector3 (distanceBetween / 2f, Random.Range (1f, powerUpHeight), 0f);
				newPowerUp.SetActive (true);
			}

			//Debug.Log(heightChange);
			transform.position = new Vector3 (transform.position.x + (platformWidths[platformSelector]/2) + distanceBetween, heightChange, transform.position.z);
			//Debug.Log (transform.position.x + (platformWidths [platformSelector] / 2) + distanceBetween);



			//Instantiate (thePlatforms[platformSelector], transform.position, transform.rotation);
			GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject ();
			newPlatform.transform.position = transform.position;
			newPlatform.transform.rotation = transform.rotation;
			newPlatform.SetActive (true);

			if (Random.Range (0f, 100) < randomCoinTreshold) {
				theCoinGenerator.SpawnsCoins (new Vector3 (transform.position.x , transform.position.y + 3f, transform.position.z));
			}

			if (Random.Range (0f, 100) < randomSpikeTreshold) {
				if (platformWidths [platformSelector] > 3f) {
					GameObject newSpike = spikePool.GetPooledObject ();
					float spikeXPosition = Random.Range (-platformWidths [platformSelector] / 2f + 1.25f, platformWidths [platformSelector] / 2f - 1.25f);
					Vector3 spikePosition = new Vector3 (spikeXPosition, 0.5f, 0f);

					newSpike.transform.position = transform.position + spikePosition;
					Debug.Log ("Spike y: " + (newSpike.transform.position.y - transform.position.y));
					newSpike.transform.rotation = transform.rotation;
					newSpike.SetActive (true);
				}

			}
			transform.position = new Vector3 (transform.position.x + (platformWidths[platformSelector]/2), transform.position.y, transform.position.z);

		}
		
	}
}
