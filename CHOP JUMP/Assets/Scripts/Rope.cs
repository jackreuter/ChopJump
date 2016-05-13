using UnityEngine;
using System.Collections;

public class Rope {

	public bool swinging;
	public bool attacking;
	public int progress;
	public int framesElapsed;
	private int lagFrames;

	public string printString;

	Animator animator;

	public Rope(Animator a) {
		swinging = false;
		attacking = false;
		progress = 0;
		framesElapsed = 0;
		lagFrames = 15;
		animator = a;
	}

	public void reset() {
		swinging = false;
		progress = 0;
		framesElapsed = 0;
		attacking = false;
	}

	public string getSwingTriggerString (int n) {
		if (n == 1) {
			return "swing1";
		} else if (n == 2) {
			return "swing2";
		} else if (n == 3) {
			return "swing3";
		} else if (n == 4) {
			return "swing4";
		} else {
			return "";
		}
	}

	void updateOnInput(int currentStage) {
		if (progress == currentStage && framesElapsed < lagFrames) {
			progress += 1;
			framesElapsed = 0;
			animator.SetTrigger (getSwingTriggerString(progress));
		} else {
			this.reset ();
		}
	}

	public void update() {
		printString = "";

		framesElapsed += 1;
		if (framesElapsed > lagFrames) {
			this.reset ();
		}

		if (Input.GetKeyDown (KeyCode.RightBracket)) {
			if (progress == 0) {
				swinging = true;
				progress += 1;
				animator.SetTrigger (getSwingTriggerString (progress));
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

		if (Input.GetKeyDown (KeyCode.O)) {
			if (progress == 3 && framesElapsed < lagFrames) {
				attacking = true;
			}
		}

	}


}
