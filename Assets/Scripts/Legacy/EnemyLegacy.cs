using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is a script for the enemy and traps behaviour
//this script calls Enemy Action Area 
public class EnemyLegacy : MonoBehaviour,_IsDamagable {
	
	bool directionUp = true;
	bool directionRight = true;
	bool actionActive = false;
	bool death = false;
	public bool verticalMovement = true;
	public bool horizontalMovement = false;
	public LayerMask wallLayers;
	public GameObject maxWalkPoint;
	public GameObject minWalkPoint;
	public GameObject actionTrigger;
	public Animator anim;

	public bool startMovemetLeftDown = false; //NeedToFinish

	bool setAnimatiorOnceUp = true;
	bool setAnimatiorOnceDown = true;

	[Header("walking behavior")] 
	public GameObject[] walkPoints; // array that stores the walk points
	public int currentWalkPoint = 0; // value to reference this array place
	
	Vector3 maxWalkPointPos;
	Vector3 minWalkPointPos;
	Vector3 actionTriggerPos;
	SpriteRenderer spriteRenderer;


	// Use this for initialization
	void Start () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		spriteRenderer.enabled = false;

		maxWalkPointPos = maxWalkPoint.transform.position;
		minWalkPointPos = minWalkPoint.transform.position;
		if(actionTrigger != null){
			actionTriggerPos = actionTrigger.transform.position;
		}
		if (actionTrigger == null) {
			StartAction ();
		}
		if (verticalMovement) {
			anim.SetTrigger ("MoveUp");
		}

	}
	
	// Update is called once per frame
	void Update () {
		
		if (verticalMovement && actionActive) {
			Vector3 movementDirection;
			if (directionUp) {
				movementDirection = transform.up;
			} else {
				movementDirection = -transform.up;
			}

			if (transform.position.y > maxWalkPoint.transform.position.y) {
				directionUp = false;
				if (setAnimatiorOnceUp) {
					setAnimatiorOnceUp = false;
					setAnimatiorOnceDown = true;
					anim.SetTrigger ("MoveFront");
				}
			}
			if(transform.position.y < minWalkPoint.transform.position.y){
				directionUp = true;
				if (setAnimatiorOnceDown) {
					setAnimatiorOnceDown = false;
					setAnimatiorOnceUp = true;
					anim.SetTrigger ("MoveUp");

				}
			}

			if (!death) {
				transform.Translate (movementDirection * Time.deltaTime);
			}
		}else if (horizontalMovement && actionActive){
			Vector3 movementDirection;
			if (directionRight) {
				movementDirection = transform.right;
			} else {
				movementDirection = -transform.right;
			}

			if (transform.position.x > maxWalkPoint.transform.position.x) {
				directionRight = false;
				if (setAnimatiorOnceUp) {
					setAnimatiorOnceUp = false;
					setAnimatiorOnceDown = true;
					anim.SetTrigger ("MoveLeft");
				}

			}
			if (transform.position.x < minWalkPoint.transform.position.x) {
				directionRight = true;
				if (setAnimatiorOnceDown) {
					setAnimatiorOnceDown = false;
					setAnimatiorOnceUp = true;
					anim.SetTrigger ("MoveFront");

				}

			}

			if (!death) {
				transform.Translate (movementDirection * Time.deltaTime);
			}

		}
		maxWalkPoint.transform.position = maxWalkPointPos;
		minWalkPoint.transform.position = minWalkPointPos;
		if (actionTrigger != null) {
			actionTrigger.transform.position = actionTriggerPos;
		}
	}
	public void StartAction(){
		actionActive = true;
	}
	public void EndAction(){
		actionActive = false;
	}
	public void TakeHit(){
		death = true;
		anim.SetTrigger ("Death");

	}
	public void EndLife(){
		Destroy (gameObject);
	}

	void OnTriggerStay2D(Collider2D col){
		if (!death && col.tag != "Enemy") {
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

}
