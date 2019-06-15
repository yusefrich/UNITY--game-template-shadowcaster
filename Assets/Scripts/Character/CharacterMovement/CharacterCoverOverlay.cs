using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCoverOverlay : MonoBehaviour
{
    public GameObject characterOverlayOutline;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bush"))
        {
            characterOverlayOutline.SetActive(false);
            GetComponent<SpriteRenderer>().sprite = other.GetComponent<WorldObjectBehavior>().WObject.Overlay;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bush"))
        {
            characterOverlayOutline.SetActive(true);
            GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}
