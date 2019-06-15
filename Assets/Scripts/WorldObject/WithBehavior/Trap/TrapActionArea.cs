using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActionArea : MonoBehaviour,_IsJumpable {

	bool characterJumpingOver = false;

	public void CharacterJumpingOver(){
	}

	void OnTriggerStay2D(Collider2D col){
		CustomCharacterController characterObj = col.GetComponent<CustomCharacterController> ();
		if (characterObj != null) {//hitei meu jogador
			_IsDamagable damagableObj = col.GetComponent<_IsDamagable> ();
			if (!characterObj.characterInfo.isJumping) {
				damagableObj.TakeHit ();
			}
		} else {//hitei outro inimigo
			_IsDamagable damagableObj = col.GetComponent<_IsDamagable> ();
			if (damagableObj != null) {
				damagableObj.TakeHit ();

			}
		}
	}
}
