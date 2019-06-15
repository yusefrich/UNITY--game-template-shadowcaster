using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullInteraction : MonoBehaviour,_IsInteractable {
	bool interactable = false;
	public InteractionType GetInteractionType()
	{
		throw new System.NotImplementedException();
	}

	public void InteractWith (){
		print ("Null interaction ok!!");
	}

	public GameObject InteractingWith()
	{
		return gameObject;
	}

	public void EndInteraction()
	{
		print("no need to skip null interaction");
	}

	public void SetInteractable (bool status){
		interactable = status;
	}
	public bool IsInteractable (){
		return interactable;
	}
}
