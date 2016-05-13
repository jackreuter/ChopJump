using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public BoardManager boardScript;
	public int timeLeft = 100;
	private List<Enemy> enemies;							//List of all Enemy units, used to issue them move commands.
	public float turnDelay = 0.1f;							//Delay between each Player turn.
	private int level = 3;

	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		//Assign enemies to a new List of Enemy objects.
		enemies = new List<Enemy>();

		DontDestroyOnLoad (gameObject);
		boardScript = GetComponent<BoardManager> ();
		InitGame ();
	}

	void InitGame() {
		boardScript.setupScene (level);
		 
	}

	public void GameOver() {
		enabled = false;
	}

	//Call this to add the passed in Enemy to the List of Enemy objects.
	public void AddEnemyToList(Enemy script)
	{
		//Add Enemy to List enemies.
		enemies.Add(script);
	}

	//Coroutine to move enemies in sequence.
	IEnumerator MoveEnemies()
	{
		//Wait for turnDelay seconds, defaults to .1 (100 ms).
		yield return new WaitForSeconds(turnDelay);

		//If there are no enemies spawned (IE in first level):
		if (enemies.Count == 0) 
		{
			//Wait for turnDelay seconds between moves, replaces delay caused by enemies moving when there are none.
			yield return new WaitForSeconds(turnDelay);
		}

		//Loop through List of Enemy objects.
		for (int i = 0; i < enemies.Count; i++)
		{
			//Call the MoveEnemy function of Enemy at index i in the enemies List.
			enemies[i].MoveEnemy ();

			//Wait for Enemy's moveTime before moving next Enemy, 
			yield return new WaitForSeconds(enemies[i].moveTime);
		}
	}

	// Update is called once per frame
	void Update () {
		//Start moving enemies.
		StartCoroutine (MoveEnemies ());
	
	}
}
