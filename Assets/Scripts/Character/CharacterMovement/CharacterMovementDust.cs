using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementDust : MonoBehaviour {

    bool playingAnimation = false;

	internal void MoveToPlayer(GameObject gameObject)
	{
        if(!playingAnimation)
        {
            transform.position = gameObject.transform.position;
        }
	}

    public void NotOnAnim(){
        playingAnimation = false;

    }
    public void OnAnim(){
        playingAnimation = true;

    }
}
