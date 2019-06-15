using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour {

    public LayerMask characterLayerMask;

	public GameObject[] pointsToFireRay;
	public GameObject graphics;
	public Animator anim;

	bool activated = false;
	bool firstBtnPress = true;
    bool playInSoundOnce = true;
    bool playOutSoundOnce = false;

	public Sprite nullSprites;
	public Sprite pressingSprite;
	public Sprite pressedSprite;
	
	//status variables
	private bool beingPressed;


	private void Update()
	{
		beingPressed = CheckIfPlayerOver();
	}

	public bool isBeingPressed()
	{ 
		return beingPressed;
	}

	public bool CheckIfPlayerOver()
    {
	    //value for returning
		bool value = false;
	    //loop to get the points
		for (int i = 0; i < pointsToFireRay.Length; i++) {
			RaycastHit2D hitInfo = Physics2D.Raycast (pointsToFireRay [i].transform.position, transform.forward, .5f, characterLayerMask);

			if (hitInfo.collider != null) {
				graphics.GetComponent<SpriteRenderer> ().sprite = pressingSprite;

				value = true;
				if (firstBtnPress) {
				PlayInSound();
				anim.SetTrigger("Press");
				firstBtnPress = false;
				}
			}
		}
	    //checking returned value
		if (!value) {
            PlayOutSoun();
			if (activated) {
				graphics.GetComponent<SpriteRenderer> ().sprite = pressedSprite;

			} else {
				graphics.GetComponent<SpriteRenderer> ().sprite = nullSprites;

			}
			firstBtnPress = true;

		}
		return value;
    }

	public void SetActivatedSprite(){
		activated = true;
	}

    void PlayInSound()
    {
        playOutSoundOnce = true;
        if(playInSoundOnce){
            GetComponent<AudioSource>().pitch = 1.5f;
            GetComponent<AudioSource>().Play();
            playInSoundOnce = false;
        }

    }
    void PlayOutSoun()
    {
        playInSoundOnce = true;
        if (playOutSoundOnce)
        {
            print("FuiChamado");
            GetComponent<AudioSource>().pitch = 1f;
            GetComponent<AudioSource>().Play();
            playOutSoundOnce = false;
        }
    }
}
