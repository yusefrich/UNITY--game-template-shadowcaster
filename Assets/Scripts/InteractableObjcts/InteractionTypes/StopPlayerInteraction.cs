using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlayerInteraction : MonoBehaviour,_IsInteractable {

    private bool interactable = false;


    public GameObject dialogCanvasCutscene;
    private bool showCutsceneOnce = true;

    public InteractionType GetInteractionType()
    {
        throw new System.NotImplementedException();
    }

    public void InteractWith()
    {
        
    }

    public GameObject InteractingWith()
    {
        return gameObject;
    }

    public void EndInteraction()
    {
        Destroy(dialogCanvasCutscene);
        PersistentGameManager.Instance.UnlockPlayer();
    }

    public bool IsInteractable()
    {
        return interactable;
    }

    public void SetInteractable(bool status)
    {
        interactable = status;
        if (status && dialogCanvasCutscene != null && showCutsceneOnce)
        {
            PersistentGameManager.Instance.LockPlayer();

            dialogCanvasCutscene.SetActive(true);
            showCutsceneOnce = false;
        }
    }
}
