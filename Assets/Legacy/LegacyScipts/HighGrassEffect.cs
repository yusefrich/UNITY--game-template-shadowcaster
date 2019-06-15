using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighGrassEffect : MonoBehaviour {

    public Sprite yellow;
	public Color yellowMask;
    public Sprite blue;
	public Color blueMask;
    public Sprite green;
	public Color greenMask;


    public GameObject highGrassObj;


	private void OnTriggerStay2D(Collider2D collision)
	{
        switch (collision.tag)
        {
            case "YellowHG":
				highGrassObj.GetComponent<SpriteRenderer>().sprite = yellow;
				highGrassObj.GetComponent<SpriteRenderer>().color = yellowMask;

                break;
            case "BlueHG":
                highGrassObj.GetComponent<SpriteRenderer>().sprite = blue;
				highGrassObj.GetComponent<SpriteRenderer>().color = blueMask;

                break;
            case "GreenHG":
                highGrassObj.GetComponent<SpriteRenderer>().sprite = green;
				highGrassObj.GetComponent<SpriteRenderer>().color = greenMask;

                break;
        }
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
        if(collision.tag == "YellowHG" || collision.tag == "BlueHG" || collision.tag == "GreenHG")
        {
            highGrassObj.GetComponent<SpriteRenderer>().sprite = null;
        }
	}
}
