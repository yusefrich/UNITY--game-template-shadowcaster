using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour {


	void OnTriggerStay2D(Collider2D col){
		_IsDamagable damagableObj = col.GetComponent<_IsDamagable> ();
		if (damagableObj != null) {
			damagableObj.TakeHit ();

		}
	}
	
	//call this function to deactivate the 
	public void DeactivateAttackArea()
	{
		Destroy(gameObject);
		//gameObject.SetActive(false);
		
	}

}
