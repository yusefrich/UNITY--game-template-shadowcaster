using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Player : MonoBehaviour,_IsDamagable, _ItemHolder {

	[Header("inputs")] 
    [Header("Movement")]
    public float shadowMovementRadius = 2f;
	private GameObject constraintShadow;
    //private Vector2 myMoveInput;
	//bool playerInnactive = false;

	[Header("Camera Smoothing")]
	public float smoothTime = .3f;
	float smoothVelocityY = 0f;
	float smoothVelocityX = 0f;
	
	[Header("Atack")] 
	[FormerlySerializedAs("atackDirection")] public GameObject sightDirection;
	[FormerlySerializedAs("atackArea")] public GameObject sightPoint;
	//new attack algorithm
	private CustomAttackController attackController;

	[Header ("UI")]
	//public GameObject pausedMenu;
	bool gamePaused = false; // neded to pause the player, use on a persistent game manager

	GameObject interactionObj;
	private CustomCharacterController characterController;

	// Use this for initialization
	void Start ()
	{
		//instantiate the father component to group all my shadows
		characterController = GetComponent<CustomCharacterController>();
		attackController = GetComponent<CustomAttackController>();

	}
    private void FixedUpdate()
    {
	    
	    //movement input and calling move function
        Vector2 myMoveInput = new Vector2(0, 0);
        if (!characterController.characterInfo.characterLocked)
        {
            myMoveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            Vector2 lookInput = new Vector2(Input.GetAxisRaw("HorizontalLook"), Input.GetAxisRaw("VerticalLook"));
            bool playerUsingLookInput = false;

            if (lookInput.x > 0 || lookInput.y > 0 || lookInput.x < 0 || lookInput.y < 0)
            {
                playerUsingLookInput = true;
				float inputAngle = Mathf.Atan2(-lookInput.x, lookInput.y) * Mathf.Rad2Deg;
				Vector3 attackDirectionEulerAngles = Vector3.forward * inputAngle;
				sightDirection.transform.eulerAngles = attackDirectionEulerAngles;
            }

            if (myMoveInput.x > 0 || myMoveInput.y > 0 || myMoveInput.x < 0 || myMoveInput.y < 0)
            {
                //setting the direction of the player atack
                if (!playerUsingLookInput)
                {
                    float inputAngle = Mathf.Atan2(-myMoveInput.x, myMoveInput.y) * Mathf.Rad2Deg;
	                Vector3 attackDirectionEulerAngles = Vector3.forward * inputAngle;
	                sightDirection.transform.eulerAngles = attackDirectionEulerAngles;
                }
            }
        }
	    characterController.Move(myMoveInput);


    }

	void UpdateCameraSize()
	{
		Bounds bounds = new Bounds(transform.position, Vector3.zero);

		float targetYPosition = Mathf.SmoothDamp (
			Camera.main.gameObject.transform.position.y,
			bounds.center.y, 
			ref smoothVelocityY, 
			smoothTime
		);
		float targetXPosition = Mathf.SmoothDamp (
			Camera.main.gameObject.transform.position.x,
			bounds.center.x, 
			ref smoothVelocityX, 
			smoothTime
		);

		Camera.main.gameObject.transform.position = new Vector3 (
			targetXPosition, 
			targetYPosition, 
			Camera.main.gameObject.transform.position.z
		);
        
		float distanceBetweenElements = Vector3.Distance(bounds.max, bounds.min);

		if(distanceBetweenElements / 1.5f > 7f)
		{
			Camera.main.orthographicSize = distanceBetweenElements / 1.5f;
		}
		else 
		{
			Camera.main.orthographicSize = 7f;
		}

	}

	void Update()
    {	    
	    UpdateCameraSize();

	    if (!characterController.characterInfo.alive || characterController.characterInfo.characterLocked)
		    return;
	
		if (Input.GetButtonDown("Interact"))
		{
			_IsInteractable objectBeingInteracted = interactionObj.GetComponent<_IsInteractable>();
			if (objectBeingInteracted != null && objectBeingInteracted.IsInteractable())
			{
				switch (objectBeingInteracted.GetInteractionType())
				{
					case InteractionType.npcStory:
						objectBeingInteracted.InteractWith();
						break;
					case InteractionType.shadow:
						
						GameObject shadow = objectBeingInteracted.InteractingWith();
						print(shadow.transform.parent.gameObject.name);
						InventoryManager.Instance.SetNewItemHolder(shadow.transform.parent.gameObject.GetComponent<_ItemHolder>());
						break;
					case InteractionType.rock:
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}		
	}

    public void EndLife()//being called continuously
    {
		if (characterController.characterInfo.alive) {
			//player is now death
			characterController.Die();
		}

    }

	public void TakeHit(){
		EndLife();
	}


    //Função chamada pelo animator
    public void PlayWalkSound()
    {
        GetComponent<AudioSource>().Play();
    }

	public void SetObjectToInteract(GameObject obj){
		interactionObj = obj;
	}

	public GameObject GetHolder()
	{
		return gameObject;
	}

	public HolderType GetType()
	{
		return HolderType.player;
	}


	public void UseActiveItem(GameObject itemEffect)
	{
		attackController.Attack(sightPoint.transform.position, itemEffect);//passar objeto
	}

	public void UsePassiveItem()
	{
		
	}

	public void UseCollectableItem()
	{
		
	}
}
