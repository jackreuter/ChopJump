﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public BoardManager boardScript;
	public int timeLeft = 100;

	private int level = 3;

	void Awake () {
		if (instance = null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

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
	
	// Update is called once per frame
	void Update () {
	
	}
}