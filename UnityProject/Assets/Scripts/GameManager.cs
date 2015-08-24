using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	static GameManager instance;
	static public GameManager Instance
	{	
		get 
		{
			if (instance == null)
			{
				instance = FindObjectOfType<GameManager>();
				if (instance)
					Instantiate(Resources.Load<GameObject>("GameManager"));
			}
			return instance;
		}
	}

	public GameObject redPlayer;
	public GameObject bluePlayer;

	int bluePoints = 0;
	int redPoints = 0;

	public Text redScoreRend;
	public Text blueScoreRend;

	// Use this for initialization
	void Awake () 
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(this.gameObject);
			return;
		}

		DontDestroyOnLoad (gameObject);
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown("Restart")) 
		{
			Application.LoadLevel(Application.loadedLevel);	
		}

		blueScoreRend.text = bluePoints.ToString ();
		redScoreRend.text = redPoints.ToString ();
	
	}
	void Winner (GameObject winningPlayer)
	{

		if (winningPlayer == redPlayer)
			redPoints ++;

		if (winningPlayer == bluePlayer)
			bluePoints ++;

		Application.LoadLevel(Application.loadedLevel);	
	}
}
