using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour {

	public GameObject powerUpObject;

	public Transform[] spawnPoints;

	public float timer;


	GameObject activatePowerup;
	// Use this for initialization
	void Start () {

		//spawnPoints = GetComponentsInChildren<Transform> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (timer < 0 && activatePowerup == null)
		{
			SpawnPowerUp();
			//timer =  10f;
		}

		timer -= Time.deltaTime;
	
	}

	void SpawnPowerUp ()
	{
		if (activatePowerup == null)
		{
			int spawnNum = Random.Range (0, spawnPoints.Length);
			activatePowerup = Instantiate (powerUpObject, spawnPoints[spawnNum].position, spawnPoints[spawnNum].rotation) as GameObject;
		}
	}
	void Collected ()
	{
		activatePowerup = null;
		timer =  5f;
	}
}
