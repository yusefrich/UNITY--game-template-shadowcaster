using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCallDeathLegacy : MonoBehaviour {

	public EnemyLegacy enemy;


	public void Death(){
		enemy.EndLife ();
	}
}
