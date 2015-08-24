using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour {

	public GameObject powerUpObject;
	public Transform[] topSpawnPoints;
	public Transform[] botSpawnPoints;
	public float timer;
	public GameObject redPlayer;
	public GameObject bluePlayer;

	GameObject ball;
	Transform chosenSpawn;
	public float[] smallestFinder;
	GameObject activatePowerup;
	int spawnNum;
	// Use this for initialization
	void Start () {

		//spawnPoints = GetComponentsInChildren<Transform> ();

		ball = GameObject.FindGameObjectWithTag ("Ball");
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
		//int topOrBot = Random.Range (0, 2);

		for (int i = 0; i < topSpawnPoints.Length; i++) 
		{
			smallestFinder [i] = topSpawnPoints [i].position.x - ball.transform.position.x;
			if (smallestFinder [i] < 0) 
			{
				smallestFinder [i] = -smallestFinder [i];
			}
		}
		for (int i = 0; i < smallestFinder.Length; i++)
		{
			if (smallestFinder [i] == Mathf.Min(smallestFinder))
			{
				spawnNum = i;
			}
		}

		if (spawnNum == 0)
		{
			if (redPlayer.transform.position.y > 0)
			{
				activatePowerup = Instantiate (powerUpObject, topSpawnPoints[spawnNum].position, topSpawnPoints[spawnNum].rotation) as GameObject;
			}
			else
			{
				activatePowerup = Instantiate (powerUpObject, botSpawnPoints[spawnNum].position, botSpawnPoints[spawnNum].rotation) as GameObject;
			}
		}
		if (spawnNum == 1)
		{
			int topOrBot = Random.Range (0, 2);

			if (topOrBot == 0)
			{
				activatePowerup = Instantiate (powerUpObject, topSpawnPoints[spawnNum].position, topSpawnPoints[spawnNum].rotation) as GameObject;
			}
			else
			{
				activatePowerup = Instantiate (powerUpObject, botSpawnPoints[spawnNum].position, botSpawnPoints[spawnNum].rotation) as GameObject;
			}
			
		}
		if (spawnNum == 2)
		{
			if (bluePlayer.transform.position.y > 0)
			{
				activatePowerup = Instantiate (powerUpObject, topSpawnPoints[spawnNum].position, topSpawnPoints[spawnNum].rotation) as GameObject;
			}
			else
			{
				activatePowerup = Instantiate (powerUpObject, botSpawnPoints[spawnNum].position, botSpawnPoints[spawnNum].rotation) as GameObject;
			}
			
		}


		//activatePowerup = Instantiate (powerUpObject, topSpawnPoints[spawnNum].position, topSpawnPoints[spawnNum].rotation) as GameObject;
		/*if (activatePowerup == null)
		{
			//int spawnNum = Random.Range (0, botSpawnPoints.Length);
			activatePowerup = Instantiate (powerUpObject, topSpawnPoints[spawnNum].position, topSpawnPoints[spawnNum].rotation) as GameObject;
		}*/
	}
	void Collected ()
	{
		activatePowerup = null;
		timer =  5f;
	}
}
