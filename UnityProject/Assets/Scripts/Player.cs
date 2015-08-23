using UnityEngine;
using System.Collections;
public class playerDetails : Player
{
	public GameObject playerObject;
	public bool gotPowerUp;

	public TeamColour teamCol;

}
public class Player : MonoBehaviour {

	public float playerNum;
	public float baseSpeed;
	public GameObject ball;
	public bool powerUp = false;
	public GameObject puManager;

	public enum TeamColour
	{
		Red,
		Blue
	};

	public TeamColour team;
	

	Vector3 direction;
	public float speed;
	bool canGrab;
	bool latched = false;
	float puTimer;
	

	CharacterController cont;
	// Use this for initialization
	void Start () 
	{

		cont = GetComponent<CharacterController> ();
		speed = baseSpeed;

	}
	
	// Update is called once per frame
	void Update () 
	{

		if (Input.GetButtonDown("GrabButton"+playerNum) && canGrab)
		{
			var sendDets = new playerDetails();
			sendDets.gotPowerUp = powerUp;
			sendDets.playerObject = this.gameObject;
			sendDets.teamCol = team;

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
		}
		else
		{
			direction = new Vector3(Input.GetAxis("Xaxis"+playerNum),Input.GetAxis("Yaxis"+playerNum),0);
			
			//print ("freedom");
			speed = baseSpeed;
		}

		if (puTimer > 0)
		{
			puTimer -= Time.deltaTime;
			speed =  baseSpeed * 2;
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


