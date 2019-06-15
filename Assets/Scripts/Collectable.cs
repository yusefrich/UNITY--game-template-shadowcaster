using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
	Animator anim;
	public bool shadowUpgrade = false;
	bool collected = false;

	void Start(){
		anim = gameObject.GetComponent<Animator> ();
	}

	void OnTriggerEnter2D(Collider2D col){
		if (shadowUpgrade && !collected) {
			Player playerObj = col.GetComponent<Player> ();
			if (playerObj != null) {//hitei meu jogador
				print("objeto coletado!!!!");
				anim.SetTrigger ("Collected");
				collected = true;
			}
		}
	}
	public void DestroyCristal(){
		Destroy (gameObject);

	}
}
