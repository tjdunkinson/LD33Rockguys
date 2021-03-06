﻿using UnityEngine;
using System.Collections;
public class playerDetails
{
	public GameObject playerObject;
	public bool gotPowerUp;
}
public class Player : MonoBehaviour {

	public float playerNum;
	public float baseSpeed;
	public GameObject ball;
	public bool powerUp = false;
	public GameObject puManager;


	Vector3 direction;
	public float speed;
	bool canGrab;
	bool latched = false;
	float puTimer;
	Animator anim;

	CharacterController cont;
	// Use this for initialization
	void Start () 
	{

		cont = GetComponent<CharacterController> ();
		speed = baseSpeed;
		anim = GetComponentInChildren<Animator> ();

	}
	
	// Update is called once per frame
	void Update () 
	{

		if (Input.GetButtonDown("GrabButton"+playerNum) && canGrab)
		{
			var sendDets = new playerDetails();
			sendDets.gotPowerUp = powerUp;
			sendDets.playerObject = this.gameObject;

			ball.SendMessage("Grabbed", sendDets);
			//ball.SendMessage("Grabbed", powerUp);
			latched = true;

		}
		if (Input.GetButtonUp("GrabButton"+playerNum) && latched)
		{
			ball.SendMessage("Ungrabbed", this.gameObject);
			latched = false;
		}

		if (Input.GetAxis("Xaxis"+playerNum) != 0 || Input.GetAxis("Yaxis"+playerNum) != 0)
		{
			direction = direction.normalized;
			
			cont.Move((direction * speed) * Time.deltaTime);
		}
	


		if (latched)
		{
			direction = new Vector3(Input.GetAxis("Xaxis"+playerNum),0,0);
			transform.LookAt (ball.transform, Vector3.back);

			anim.SetBool("Pulling", true);
			anim.SetBool("Walking", true);
		}
		else
		{

			direction = new Vector3(Input.GetAxis("Xaxis"+playerNum),Input.GetAxis("Yaxis"+playerNum),0);
			transform.rotation = Quaternion.LookRotation(direction, Vector3.back);
			speed = baseSpeed;

			anim.SetBool("Walking", true);
			anim.SetBool("Pulling", false);
		}
		if (direction.normalized == Vector3.zero)
		{
			transform.LookAt (ball.transform, Vector3.back);

			anim.SetBool("Walking", false);
			anim.SetBool("Pulling", false);
		}
		

		if (puTimer > 0)
		{
			puTimer -= Time.deltaTime;
			speed =  baseSpeed * 3;
		}
		if (puTimer < 0)
		{
			DeactivatePower();
		}

	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "Ball") 
		{
			canGrab = true;
		}
		if (col.tag == "PowerUp")
		{
			ActivatePower(col.gameObject);
		}

	}
	void OnTriggerExit (Collider col)
	{
		if (col.tag == "Ball") 
		{
			canGrab = false;
			latched = false;
			//ball.SendMessage("Ungrabbed", this.gameObject);
		}
	}

	void ModifySpeed (float newSpeed)
	{
		speed = baseSpeed / newSpeed;
	}

	void ActivatePower (GameObject puObject)
	{
		powerUp = true;
		puManager.SendMessage("Collected");
		puTimer = 5f;
		Destroy (puObject, 0.2f);
	}
	void DeactivatePower ()
	{
		powerUp = false;
		speed = baseSpeed;
	}
}


