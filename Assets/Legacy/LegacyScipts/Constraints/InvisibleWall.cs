using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWall : MonoBehaviour,_IsJumpable {

	Collider2D myCollider; 
	float playerContactDelayValue = .5f;
	float playerContactDelay;
	public bool horizontal = false;
	public bool vertical = false;

	// Use this for initialization
	void Start () {
		myCollider = gameObject.GetComponent<Collider2D> ();
		playerContactDelay = playerContactDelayValue;
		myCollider.isTrigger = false;

	}
	void ResetDelay(){
		playerContactDelay = playerContactDelayValue;

	}
	public void CharacterJumpingOver(){
		print("jump over is being called");

		myCollider.isTrigger = true;

	}

	void OnCollisionStay2D(Collision2D collision)
	{

		if (collision.collider.gameObject.tag == "Player") {
			if (collision.collider.gameObject.GetComponent<CustomCharacterController> ().characterInfo.isJumping) {
				myCollider.isTrigger = true;
				//collideOnJump = true;
				print ("collided on jump");
			} else {
				if (vertical) {
					if (collision.collider.gameObject.GetComponent<CustomCharacterController> ().characterInfo.movHorizontal ) {
						if (playerContactDelay <= 0f) {
							myCollider.isTrigger = true;

						} else {

							playerContactDelay -= Time.deltaTime;
						}
					} else {
						ResetDelay ();

					}

				}
				if (horizontal) {
					if (collision.collider.gameObject.GetComponent<CustomCharacterController> ().characterInfo.movVertical) { 
						if (playerContactDelay <= 0f) {
							myCollider.isTrigger = true;

						} else {

							playerContactDelay -= Time.deltaTime;
						}
					} else {
						ResetDelay ();

					}
				}

				if (!collision.collider.gameObject.GetComponent<CustomCharacterController> ().characterInfo.movHorizontal &&
					!collision.collider.gameObject.GetComponent<CustomCharacterController> ().characterInfo.movVertical) {
					ResetDelay ();
				}




			}
		}

	}

	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.collider.gameObject.tag == "Player") {
			
			ResetDelay ();
			myCollider.isTrigger = false;
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Player") {
			
			
			ResetDelay ();
			myCollider.isTrigger = false;
		}
	}
}
