using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Serialization;

public class AutoTypeTextEffect : MonoBehaviour {

	string text;
	string currentText = ""; 
	float delay = .05f;

	AudioSource sound;

	bool nextTextSet = false;
	float nextTextDelay = 2f;

	[Header("new dialog interaction system")]
	[Header("variables to set the balloon border")]
	public GameObject upBalloonBorder;
	public GameObject downBalloonBorder;
	public GameObject rightBalloonBorder;
	public GameObject leftBalloonBorder;
	[Header("balloon size offset")] 
	public float widthOffset = .4f;
	public float heightOffset = .4f;
	[Header("father balloon reference")] 
	public GameObject myBalloon;

	

	// Use this for initialization
	public void StartText ()
	{
		SetBalloonSize();
		
		print("texto startado");
		text = GetComponent<Text> ().text;
		sound = GetComponent<AudioSource> ();

		StartCoroutine (showText ());

	}
	IEnumerator showText(){
		print("mostrando texto");
		for (int i = 0; i < text.Length + 1; i++) {
			if (i > 1 && currentText.Length >= text.Length)
			{
				i = currentText.Length;
			}
			else
			{
				sound.Play();
				currentText = text.Substring (0, i);
				print(text);
				this.GetComponent<Text> ().text = currentText;
			}

			yield return new WaitForSeconds (delay);
		}
	}

	public void EndText()
	{
		currentText = text;
		this.GetComponent<Text> ().text = currentText;
	}

	public bool IsTextComplete()
	{
		bool status;
		if (currentText.Length < text.Length)
		{
			status = false;
		}
		else
		{
			status = true;
		}

		return status;
	}

	void SetBalloonSize()
	{
		float width = Vector2.Distance(rightBalloonBorder.transform.position, leftBalloonBorder.transform.position);
		float height = Vector2.Distance(downBalloonBorder.transform.position, upBalloonBorder.transform.position);

		myBalloon.GetComponent<SpriteRenderer>().size = new Vector2(width + widthOffset, height + heightOffset);
	}
	
}
