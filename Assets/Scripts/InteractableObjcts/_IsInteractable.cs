using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum InteractionType
{
	npcStory, shadow, rock
}

public interface _IsInteractable  {


	InteractionType GetInteractionType();

	void InteractWith ();
	GameObject InteractingWith ();
	void EndInteraction ();
	void SetInteractable (bool status);
	bool IsInteractable ();

}
