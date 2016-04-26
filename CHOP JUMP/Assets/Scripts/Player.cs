using UnityEngine;
using System.Collections;

public class Player : MovingObject {

	public float restartLevelDelay = 1f;        //Delay time in seconds to restart level.
	public int wallDamage = 1;                  //How much damage a player does to a wall when chopping it.
	public int ropeProgress = 0;
	public int ropeFrame = 0;
	public bool swinging = false;
	public int jumpFrame = 0;
	public bool jumping = false;

	public int jumpFrames = 10;
	public int ropeFrames = 10;

	private Animator animator;                  //Used to store a reference to the Player's animator component.
	
	// Use this for initialization
	protected override void Start () {
		animator = GetComponent<Animator> ();
		base.Start ();
	}

	private void onDisable() {}
	private void checkIfGameOver() {}

	protected override void AttemptMove<T> (int xDir, int yDir)
	{
		base.AttemptMove<T>(xDir, yDir);
		RaycastHit2D hit;
		checkIfGameOver();

	}
	
	// Update is called once per frame
	void Update () {

		//logistics of rope flourish motion for movement mechanic
		int horizontal = (int)(Input.GetAxisRaw ("Horizontal"));
		int vertical = (int)(Input.GetAxisRaw ("Vertical"));

		if (jumping) {
			if (jumpFrame == jumpFrames) {
				jumping = false;
				jumpFrame = 0;
			} else {
				jumpFrame += 1;
			}
		}

		if (swinging) {
			if (ropeFrame == ropeFrames) {
				swinging = false;
				ropeFrame = 0;
				ropeProgress = 0;
			} else {
				ropeFrame += 1;
			}
		}

		if (Input.GetKey (KeyCode.Space)) {
			jumpFrame = 0;
		} 

		if (Input.GetKeyUp (KeyCode.Space)) {
			jumping = true;
		}

		if (Input.GetKeyDown (KeyCode.RightBracket)) {
			if (ropeProgress == 0) {
				swinging = true;
				ropeProgress += 1;
			} else {
				ropeProgress = 0;
				swinging = false;
			}
		}
			
		if (Input.GetKeyDown (KeyCode.LeftBracket)) {
			if (ropeProgress == 1 && ropeFrame != 0) {
				ropeProgress += 1;
				ropeFrame = 0;
			} else {
				ropeProgress = 0;
				swinging = false;
			}
		}

		if (Input.GetKeyDown (KeyCode.P)) {
			if (ropeProgress == 2 && ropeFrame != 0) {
				ropeProgress += 1;
				ropeFrame = 0;
			} else {
				ropeProgress = 0;
				swinging = false;
			}
		}

		if (Input.GetKeyDown (KeyCode.L)) {
			if (ropeProgress == 3 && ropeFrame != 0) {
				ropeProgress += 1;
				ropeFrame = 0;
			} else {
				ropeProgress = 0;
				swinging = false;
			}
		}

		if (Input.GetKeyDown (KeyCode.Period)) {
			if (ropeProgress == 4 && ropeFrame != 0) {
				ropeProgress += 1;
				ropeFrame = 0;
			} else {
				ropeProgress = 0;
				swinging = false;
			}
		}

		if (ropeProgress == 5 && jumpFrame != 0 && (horizontal != 0 || vertical !=0)) {
			ropeProgress = 0;
			ropeFrame = 0;
			jumpFrame = 0;
			swinging = false;
			jumping = false;
			AttemptMove<Wall> (horizontal, vertical);
		}
	}

	protected override void OnCantMove<T> (T component) {
		Wall hitWall = component as Wall;
  		hitWall.DamageWall (wallDamage);
		animator.SetTrigger ("playerChop");
	}

	private void Restart() {
		Application.LoadLevel (Application.loadedLevel);
	}

	public void HitStun () {
		animator.SetTrigger ("playerHit");
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		//Check if the tag of the trigger collided with is Exit.
		if(other.tag == "Exit")
		{
			//Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
			Invoke ("Restart", restartLevelDelay);

			//Disable the player object since level is over.
			enabled = false;
		}
	}
}
