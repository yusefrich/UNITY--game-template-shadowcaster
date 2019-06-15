using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialog : MonoBehaviour {
	bool eventTrigged = false;

	[Header("PlayerReference")]
	public Player playerScritp;

	[Header ("Canvas reference")]
	public GameObject saveEndingCanvas;
	public GameObject abadonEndingCanvas;
	public GameObject dialogCanvas;


	public GameObject secondDialogCanvas;

	[Header("TriggerType")]
	public bool saveEnding = false;
	public bool abadonEnding = false;
	bool onSecondDialogRange = false;
	bool secondDialogEnd = false;

	// Use this for initialization

	void Update(){
		if (onSecondDialogRange && secondDialogEnd) {
			if (Input.GetButtonDown ("Interact")) {
				saveEndingCanvas.SetActive (true);
				//playerScritp.PlayerInactive (true);
				PersistentGameManager.Instance.LockPlayer ();
			}
		}
	}
	public void SecondDialogEnd(){

		secondDialogEnd = true;
	}
    public void FreePlayer()
    {
        //playerScritp.PlayerInactive(false, false);
		PersistentGameManager.Instance.UnlockPlayer ();

    }

	void OnTriggerEnter2D(Collider2D col){
		if (!eventTrigged && !abadonEnding && !saveEnding) {
			if (col.tag == "Player") {
                
				dialogCanvas.SetActive (true);
				eventTrigged = true;
               // playerScritp.PlayerInactive(true, true);
				PersistentGameManager.Instance.LockPlayer ();

			}
		}
		if (abadonEnding) {
			if (col.tag == "Player") {
				abadonEndingCanvas.SetActive (true);
				//playerScritp.PlayerInactive (true,true);
				PersistentGameManager.Instance.LockPlayer ();

			}
		}
		if (saveEnding) { 
			if (col.tag == "Player") {
                secondDialogCanvas.SetActive (true);
                //playerScritp.PlayerInactive(true, true);

			}
		}
	}
	void OnTriggerStay2D(Collider2D col){
		if (saveEnding) {
			if(col.tag == "Player"){
				onSecondDialogRange = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (saveEnding) {
			if(col.tag == "Player"){
				onSecondDialogRange = false;
			}
		}

	}
}
