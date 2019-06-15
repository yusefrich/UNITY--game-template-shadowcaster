﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSound : MonoBehaviour {

    public GameObject SoundEffect;

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if(collision.tag == "Player")
        {
            SoundEffect.SetActive(true);
        }
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
        if (collision.tag == "Player")
        {
            SoundEffect.SetActive(false);
        }
	}
}
