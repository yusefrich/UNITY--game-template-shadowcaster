using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTextStartTrigger : MonoBehaviour {

	public GameObject text;

    public GameObject mainSound;

	public void SetText(){
		text.SetActive (true);
        mainSound.SetActive(false);
	}



}
