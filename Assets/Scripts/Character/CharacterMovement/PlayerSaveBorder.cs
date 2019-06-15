using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveBorder : MonoBehaviour {

	[Header("check direction")]
	public bool rightBorder = false;
	public bool leftBorder = false; 
	public bool upBorder = false;
	public bool downBorder = false;
	
	[Header("Collisors")]
	public LayerMask groundLayerMask; 

	[Header("Checking collisions")]
	public float coolDownPeriodInSeconds = .5f;
	//internal variables
	float timeStamp;

	public void CheckBorder (ref Vector2 movInput) {
		if (rightBorder) {
			if (movInput.x > 0) {
				RaycastHit2D hitInfo = Physics2D.Raycast (transform.position, transform.forward, .5f, groundLayerMask);
				if (hitInfo.collider == null) {
					//modify input
					if (timeStamp > 0) {
						timeStamp -= Time.deltaTime;
						movInput = new Vector2 (0, movInput.y);
					} 
				} else {
					timeStamp = coolDownPeriodInSeconds;
				}
			} else {
				timeStamp = coolDownPeriodInSeconds;
			}
		} 
		if (leftBorder) {
			if (movInput.x < 0) {
				RaycastHit2D hitInfo = Physics2D.Raycast (transform.position, transform.forward, .5f, groundLayerMask);
				if (hitInfo.collider == null) {
					//modify input
					if (timeStamp > 0) {
						timeStamp -= Time.deltaTime;
						movInput = new Vector2 (0, movInput.y);
					}
				} else {
					timeStamp = coolDownPeriodInSeconds;
				}
			}else {
				timeStamp = coolDownPeriodInSeconds;
			}
		} 
		if(upBorder){
			if (movInput.y > 0) {
				RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.forward, .5f, groundLayerMask);
				if (hitInfo.collider == null) {
					//modify input
					if (timeStamp > 0) {
						timeStamp -= Time.deltaTime;
						movInput = new Vector2 (movInput.x, 0);
					}
				} else {
					timeStamp = coolDownPeriodInSeconds;
				}
			}else {
				timeStamp = coolDownPeriodInSeconds;
			}
		} 
		if(downBorder){
			if (movInput.y < 0){
				RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.forward, .5f, groundLayerMask);
				if (hitInfo.collider == null) {
					//modify input
					if (timeStamp > 0) {
						timeStamp -= Time.deltaTime;
						movInput = new Vector2 (movInput.x, 0);
					}
				} else {
					timeStamp = coolDownPeriodInSeconds;
				}
			}else {
				timeStamp = coolDownPeriodInSeconds;
			}
		}
	}
}
