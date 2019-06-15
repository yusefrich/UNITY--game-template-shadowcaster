using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionAreaLegacy : MonoBehaviour {

	public EnemyLegacy enemyScrpt;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
		Player playerObj = col.GetComponent<Player> ();
		if (playerObj != null) {//hitei meu jogador
			enemyScrpt.StartAction();
		}
	}

}
