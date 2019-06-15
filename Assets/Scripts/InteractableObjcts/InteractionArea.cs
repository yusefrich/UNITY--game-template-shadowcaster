using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using MyBox;
using UnityEditor;
using UnityEngine;

public class InteractionArea : MonoBehaviour
{
    [Header("Interactable graphics references")]
    public bool turnGraphicsOfOnExit;
    public GameObject interactionGraphics;
    
    [Header("object to interact with")]
	public GameObject interactableObjReferece;
	BoxCollider2D playerCollider;
	//public Animator balloonAnim;
	//PlayerInteractArea
	// Use this for initialization
	void Start () {
		playerCollider = GameObject.FindWithTag ("PlayerInteractArea").GetComponent<BoxCollider2D> ();
	}
	

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerInteractArea")
        {
            if(interactionGraphics != null)
            {
                interactionGraphics.SetActive(true);
            }
            if (interactableObjReferece != null)
            {
                _IsInteractable interactableObj = interactableObjReferece.GetComponent<_IsInteractable>();
                interactableObj.SetInteractable(true);
                //setting the object to the player interactWith
                GameObject.FindWithTag("Player").GetComponent<Player>().SetObjectToInteract(interactableObjReferece);

            }

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerInteractArea")
        {
            if (interactionGraphics != null && turnGraphicsOfOnExit)
            {
                interactionGraphics.SetActive(false);
            }
            if (interactableObjReferece != null)
            {
                _IsInteractable interactableObj = interactableObjReferece.GetComponent<_IsInteractable>();
                interactableObj.SetInteractable(false);

            }

        }
    }
}

