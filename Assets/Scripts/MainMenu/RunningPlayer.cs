using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Animator anim = GetComponent<Animator> ();
		anim.SetBool ("IsMoving", true);
		anim.SetFloat ("Xspeed", 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
