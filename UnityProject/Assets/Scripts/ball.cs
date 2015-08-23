using UnityEngine;
using System.Collections;

public class ball : MonoBehaviour {

	public int holdignMe;

	public GameObject[] players; // 0 is prime player

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (holdignMe > 0)
		{
			players[0].SendMessage("ModifySpeed", 2 * holdignMe);
			Vector3 slide = Vector3.zero;
			slide.x = players[0].transform.position.x;
			transform.position = slide;

			//print (players[0].name);
		
		}
	
	}

	void Grabbed (GameObject playerGrab)
	{
		holdignMe ++;
		players [holdignMe - 1] = playerGrab;
	}
	void Ungrabbed (GameObject playerGrab)
	{
		holdignMe --;
	}
}
