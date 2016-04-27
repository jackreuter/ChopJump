using UnityEngine;
using System.Collections;

public class Rope {

	public bool swinging;
	public int progress;
	public int framesElapsed;
	private int lagFrames;

	public Rope() {
		swinging = false;
		progress = 0;
		framesElapsed = 0;
		lagFrames = 10;
	}

	public void reset() {
		swinging = false;
		progress = 0;
		framesElapsed = 0;
	}

	void updateOnInput(int currentStage) {
		if (progress == currentStage && framesElapsed < lagFrames) {
			progress += 1;
			framesElapsed = 0;
		} else {
			this.reset ();
		}
	}

	public void update() {

		framesElapsed += 1;
		if (framesElapsed > lagFrames) {
			this.reset ();
		}

		if (Input.GetKeyDown (KeyCode.RightBracket)) {
			if (progress == 0) {
				swinging = true;
				progress += 1;
			} else {
				this.reset ();
			}
		}

		if (Input.GetKeyDown (KeyCode.LeftBracket)) {
			this.updateOnInput(1);
		}

		if (Input.GetKeyDown (KeyCode.P)) {
			this.updateOnInput(2);
		}

		if (Input.GetKeyDown (KeyCode.L)) {
			this.updateOnInput(3);
		}

		if (Input.GetKeyDown (KeyCode.Period)) {
			this.updateOnInput(4);
		}

	}


}
