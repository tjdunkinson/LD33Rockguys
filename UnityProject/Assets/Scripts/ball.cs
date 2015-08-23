using UnityEngine;
using System.Collections;

public class ball : MonoBehaviour {

	public int holdignMe;

	//public GameObject[] players; // 0 is prime player

	public GameObject pullingPlayer;
	public GameObject secondaryplayer;

	private float pullingHeldDist;
	private float secondaryHeldDist;
	private Vector3 slide;
	private Vector3 playerDrag;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {




		if (pullingPlayer != null)
		{
			holdignMe = 1;
			//pullingHeldDist = pullingPlayer.transform.position.x - transform.position.x;
		}
		else
			holdignMe = 0;

		if (secondaryplayer != null)
		{
			holdignMe = 2;
			//secondaryHeldDist = secondaryplayer.transform.position.x - transform.position.x;
		}

		if (holdignMe > 0)
		{
			pullingPlayer.SendMessage("ModifySpeed", 2 * holdignMe);

			slide = transform.position;
			slide.x = pullingPlayer.transform.position.x - (pullingHeldDist);
			transform.position = slide;

		}
		if (holdignMe > 1)
		{
			playerDrag = secondaryplayer.transform.position;
			playerDrag.x = transform.position.x + secondaryHeldDist;
			secondaryplayer.transform.position = playerDrag;
		}
	
	}

	void Grabbed (playerDetails playerDets)
	{
		if (holdignMe == 0)
		{
			pullingPlayer = playerDets.playerObject;

			pullingHeldDist = pullingPlayer.transform.position.x - transform.position.x;
		}
		else if (holdignMe == 1)
		{
			if (playerDets.gotPowerUp)
			{
				secondaryplayer = pullingPlayer;
				pullingPlayer = playerDets.playerObject;

				pullingHeldDist = pullingPlayer.transform.position.x - transform.position.x;
				secondaryHeldDist = secondaryplayer.transform.position.x - transform.position.x;
			}
			else
			{
				secondaryplayer = playerDets.playerObject;

				pullingHeldDist = pullingPlayer.transform.position.x - transform.position.x;
				secondaryHeldDist = secondaryplayer.transform.position.x - transform.position.x;
			}
		}

	}
	void Ungrabbed (GameObject playerGrab)
	{

		if (holdignMe == 1)
		{
			pullingPlayer = null;
			secondaryplayer = null;

			pullingHeldDist = 0;
			secondaryHeldDist = 0;
		}
		if (holdignMe > 1)
		{
			if (secondaryplayer == playerGrab)
			{
				secondaryplayer = null;
				secondaryHeldDist = 0;
			}
			else if (pullingPlayer == playerGrab)
			{
				pullingPlayer = secondaryplayer;
				secondaryplayer = null;

				pullingHeldDist = pullingPlayer.transform.position.x - transform.position.x;
				secondaryHeldDist = 0;
			}
		}

	}
}
