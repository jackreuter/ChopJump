﻿using UnityEngine;
using System.Collections;

public class Player : MovingObject {

	public float restartLevelDelay = 1f;        //Delay time in seconds to restart level.
	public int wallDamage = 1;                  //How much damage a player does to a wall when chopping it.
	public Rope rope;

	public int jumpFrame = 0;
	public bool jumping = false;
	public int jumpFrames = 20;
	public float minMove = 0.5f;
	public float maxMove = 2f;

	private Animator animator;                  //Used to store a reference to the Player's animator component.
	
	// Use this for initialization
	protected override void Start () {
		animator = GetComponent<Animator> ();
		rope = new Rope (animator);
		base.Start ();
	}

	private void onDisable() {}
	private void checkIfGameOver() {}

	protected override void AttemptMove<T> (float xDir, float yDir)
	{
		base.AttemptMove<T>(xDir, yDir);
		RaycastHit2D hit;
		checkIfGameOver();

	}
	
	// Update is called once per frame
	void Update () {

		//logistics of rope flourish motion for movement mechanic
		float horizontal = (float) ((Input.GetAxisRaw ("Horizontal")));
		float vertical = (float) (Input.GetAxisRaw ("Vertical"));

		rope.update ();
		//print (rope.printString);

		if (jumping) {
			if (jumpFrame == jumpFrames) {
				jumping = false;
				jumpFrame = 0;
			} else {
				jumpFrame += 1;
			}
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			jumpFrame = 0;
			animator.SetTrigger ("bend");
		} 

		if (Input.GetKeyUp (KeyCode.Space)) {
			jumping = true;
			animator.SetTrigger ("unbend");
		}

		if (rope.progress == 5 && jumpFrame != 0 && (horizontal != 0 || vertical !=0)) {

			float jumpPercent = (float)jumpFrame / (float)jumpFrames;

			if (horizontal > 0 && vertical > 0) {
				float dist = Mathf.Sqrt (maxMove * maxMove * 2);
				horizontal = dist * jumpPercent;
				vertical = dist * jumpPercent;
			} else {
				if (horizontal > 0)
					horizontal = horizontal * jumpPercent * maxMove + minMove;
				if (vertical > 0)
					vertical = vertical * jumpPercent * maxMove + minMove;
			}

			jumping = false;
			rope.reset ();
			AttemptMove<Wall> (horizontal, vertical);
		}

		if (rope.attacking) {
			animator.SetTrigger ("playerChop");
		}
	}

	protected override void OnCantMove<T> (T component) {
//		Wall hitWall = component as Wall;
//  		hitWall.DamageWall (wallDamage);
		animator.SetTrigger ("playerHit");
	}

	private void Restart() {
		Application.LoadLevel (Application.loadedLevel);
	}

	public void HitStun (int damage) {
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
