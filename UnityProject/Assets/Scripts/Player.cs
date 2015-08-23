using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float playerNum;
	public float baseSpeed;
	public GameObject ball;

	Vector3 direction;
	public float speed;
	bool canGrab;


	CharacterController cont;
	// Use this for initialization
	void Start () {

		cont = GetComponent<CharacterController> ();

		speed = baseSpeed;

	
	}
	
	// Update is called once per frame
	void Update () {



	
		if (Input.GetButtonDown("GrabButton"+playerNum) && canGrab)
		{
			ball.SendMessage("Grabbed", this.gameObject);
			direction = new Vector3(Input.GetAxis("Xaxis"+playerNum),0,0);

		}
		else
		{
			direction = new Vector3(Input.GetAxis("Xaxis"+playerNum),Input.GetAxis("Yaxis"+playerNum),0);
		}
		if (Input.GetButtonUp("GrabButton"+playerNum) && canGrab)
		{
			ball.SendMessage("Ungrabbed", this.gameObject);
		}

		if (Input.GetAxis("Xaxis"+playerNum) != 0 || Input.GetAxis("Yaxis"+playerNum) != 0)
		{
			direction = direction.normalized;
			
			cont.Move((direction * speed) * Time.deltaTime);
		}


	}

	void OnTriggerEnter (Collider col)
	{
		canGrab = true;
	}
	void OnTriggerExit (Collider col)
	{
		canGrab = false;

		speed = baseSpeed;
	}

	void ModifySpeed (float newSpeed)
	{
		speed = baseSpeed / newSpeed;
	}
}
