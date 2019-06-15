using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour, _IsInteractable
{
    
    private bool interactable;
    
    [Header("interactable text reference")]
    public AutoTypeTextEffect[] texts;
    private int currentInteractingText = 0;
    [Header("interaction type")] 
    public InteractionType myType;


    public InteractionType GetInteractionType()
    {
        return myType;
    }

    public void InteractWith()
    {
        //next dialog in conversation
        if (texts[currentInteractingText].IsTextComplete())
        {
            texts[currentInteractingText].gameObject.SetActive(false);
            
            currentInteractingText++;
            
            if (currentInteractingText >= texts.Length)
            {
                currentInteractingText = 0;
            }
            
            texts[currentInteractingText].gameObject.SetActive(true);
            texts[currentInteractingText].StartText();
        }
        texts[currentInteractingText].EndText();
        print("interacted");
    }

    public GameObject InteractingWith()
    {
        return gameObject;
    }

    public void EndInteraction()
    {
        //endConversation
        print("skiped");
    }

    public void SetInteractable(bool status)
    {
        interactable = status;
        if (interactable)
        {
            StartDialogInteractions();
        }
        else
        {
            EndDialogInteractions();
        }
    }

    public bool IsInteractable()
    {
        return interactable;
    }

    void StartDialogInteractions()
    {
        texts[0].gameObject.SetActive(true);
        texts[0].StartText();

    }
    void EndDialogInteractions()
    {
        foreach (var text in texts)
        {
            if (text.gameObject.activeInHierarchy)
            {
                text.EndText();
                text.gameObject.SetActive(false);
            }
        }
        gameObject.SetActive(false);
        currentInteractingText = 0;

    }
}
