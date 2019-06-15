using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassBarrierSet : MonoBehaviour
{

	private BoxCollider2D myColl;
	private SpriteRenderer myRend;
	public GameObject mask;

	
	// Use this for initialization
	void Start ()
	{
		myColl = gameObject.GetComponent<BoxCollider2D>();
		myRend = gameObject.GetComponent<SpriteRenderer>();

		SetColliderSize();
		SetMaskSize();
	}

	void SetColliderSize()
	{
		myColl.size = myRend.size;
	}

	void SetMaskSize()
	{
		mask.transform.localScale = new Vector3(myRend.size.x, myRend.size.y, mask.transform.localScale.z);
	}
}
