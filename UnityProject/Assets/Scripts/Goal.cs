using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	public GameManager gameManager;
	public GameObject homePlayer;

	// Use this for initialization
	void Start () 
	{
		gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "Ball")
		{
			gameManager.SendMessage("Winner", homePlayer);
		}
	}
}
