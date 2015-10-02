using UnityEngine;
using System.Collections;
public class kickInstruction
{
	public Vector3 kickDir;
	public float kickPow;
}
public class Player : MonoBehaviour {

	public float playerNum;
	public float baseSpeed;
	public GameObject ball;
	Vector3 direction;
	public float speed;
	public float kickRange;
	public float chargeTime;
	public float maxKickPower;

	Animator anim;
	CharacterController cont;

	public float kickPower;


	//bool canGrab;

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

		if (Input.GetButtonDown("Kick"+playerNum))
		{
			kickPower = 0f;
			
			
		}
		if (Input.GetButton("Kick"+playerNum))
		{
			kickPower = Mathf.Lerp (0f, maxKickPower, Time.time * chargeTime);
			
			
		}
		if (Input.GetButtonUp("Kick"+playerNum))
		{
			float dist = Vector3.Distance(transform.position, ball.transform.position);
			if (dist < kickRange)
			{
				var sendDets = new kickInstruction();
				sendDets.kickDir = Vector3.zero; //put kick vector here
				sendDets.kickPow = kickPower; //put kick pwoer here
				
				ball.SendMessage("Kicked", sendDets);

				kickPower = 0f;

			}

			/*ball.SendMessage("Ungrabbed", this.gameObject);
			latched = false;*/
		}

		if (Input.GetAxis("Xaxis"+playerNum) != 0 || Input.GetAxis("Yaxis"+playerNum) != 0)
		{
			direction = direction.normalized;
			
			cont.Move((direction * speed) * Time.deltaTime);
		}
	


		/*if (latched)
		{
			direction = new Vector3(Input.GetAxis("Xaxis"+playerNum),0,0);
			transform.LookAt (ball.transform, Vector3.back);

			anim.SetBool("Pulling", true);
			anim.SetBool("Walking", true);
		}*/
		//else
		//{

			direction = new Vector3(Input.GetAxis("Xaxis"+playerNum),Input.GetAxis("Yaxis"+playerNum),0);
			transform.rotation = Quaternion.LookRotation(direction, Vector3.back);
			speed = baseSpeed;

			anim.SetBool("Walking", true);
			//anim.SetBool("Pulling", false);
		//}
		if (direction.normalized == Vector3.zero)
		{
			transform.LookAt (ball.transform, Vector3.back);

			anim.SetBool("Walking", false);
			anim.SetBool("Pulling", false);
		}
		

		/*if (puTimer > 0)
		{
			puTimer -= Time.deltaTime;
			speed =  baseSpeed * 3;
		}
		if (puTimer < 0)
		{
			DeactivatePower();
		}*/

	}


}


