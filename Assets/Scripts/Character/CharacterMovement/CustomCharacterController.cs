using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

public class CustomCharacterController : MonoBehaviour {
	
	[Header("My character info struct")]
	public CharacterStatus characterInfo;
	
	[Header("Movement")]
	public LayerMask groundLayerMask;
	public GameObject[] safeBorderPoints;//substitutes for jumping over objects
	public float speed;
	private Rigidbody2D rb2D;
	private MovementOutputData movementOutputDataObject = new MovementOutputData();

	[Header("Jump")] 
	public bool hasJumpFunction;
	[ConditionalField("hasJumpFunction")] public LayerMask jumpOverLayerMask;
	[ConditionalField("hasJumpFunction")] public float jumpingTimeValue = .5f;
	[ConditionalField("hasJumpFunction")] private float jumpingTime;
	[ConditionalField("hasJumpFunction")] private Vector3 mainJumpDirection;

	[Header("my character references")] 
	private Vector3 characterRespawnPosition;
	
	private void Start()
	{
		jumpingTime = jumpingTimeValue;
		rb2D = GetComponent<Rigidbody2D> ();
		characterRespawnPosition = gameObject.transform.position;
		//setting the player life status to true
		characterInfo.alive = true;
		movementOutputDataObject.Alive = true;
		movementOutputDataObject.Falling = false;

	}
	
	/// <summary>
	/// this function moves the player using the attached rigidbody velocity
	/// </summary>
	/// <param name="moveInput">input direction of the movemento</param>
	public void Move(Vector2 moveInput)
    {

	    //start the movement of the player
		for (int i = 0; i < safeBorderPoints.Length; i++) {
			safeBorderPoints [i].GetComponent<PlayerSaveBorder> ().CheckBorder (ref moveInput);
		}
	    
	    //starting the character animations
        CharacterMovementAnimation(moveInput);

        //setting character directions
        characterInfo.movUp = (moveInput.y > 0) ? true : false;
        characterInfo.movDown = (moveInput.y < 0) ? true : false;
        characterInfo.movRight = (moveInput.x > 0) ? true : false;
        characterInfo.movLeft = (moveInput.x < 0) ? true : false;
        characterInfo.movVertical = (characterInfo.movUp || characterInfo.movDown) ? true : false;
        characterInfo.movHorizontal = (characterInfo.movRight || characterInfo.movLeft) ? true : false;

	    //velocity of the player
        Vector2 velocity = moveInput.normalized * speed;


        if (characterInfo.isJumping)//time that the player stays jumping 
        {
            //fire ray to jump over other things
            //make a array
            RaycastHit2D[] hitInfo = Physics2D.RaycastAll(transform.position, mainJumpDirection, .5f, jumpOverLayerMask);

            for (int i = 0; i < hitInfo.Length; i++)
            {
                _IsJumpable jumpableObject = hitInfo[i].collider.GetComponent<_IsJumpable>();
                if (jumpableObject != null)
                {
                    jumpableObject.CharacterJumpingOver();
                }
            }


            jumpingTime -= Time.deltaTime;
            if (jumpingTime <= 0)
            {
                jumpingTime = jumpingTimeValue;
                //reseting force added
                rb2D.AddForce(transform.forward * 0, ForceMode2D.Impulse);
                rb2D.velocity = Vector3.zero;
                rb2D.angularVelocity = 0f;
                //reseting jump
                Jump(false, new Vector3(0, 0, 0));

            }
        }
        else
        {
	        rb2D.velocity = characterInfo.alive ? velocity : Vector2.zero;

	        if(!CheckGround())
			{
				movementOutputDataObject.IsMoving = false;
			}
        }

	}

	void CharacterMovementAnimation(Vector2 moveInput)
	{
		movementOutputDataObject.Xspeed = moveInput.x;
		movementOutputDataObject.Yspeed = moveInput.y;

		movementOutputDataObject.IsJumping = characterInfo.isJumping;

		if (!characterInfo.characterLocked)
		{
			if (moveInput.x > 0 || moveInput.y > 0 || moveInput.x < 0 || moveInput.y < 0)
			{
				movementOutputDataObject.LastX = moveInput.x;
				movementOutputDataObject.LastY = moveInput.y;
				
				movementOutputDataObject.IsMoving = true;
			}
			else
			{
				movementOutputDataObject.IsMoving = false;
			}

		}
		else
		{
			movementOutputDataObject.IsMoving = false;
		}
	}

	//checks the ground that the player is in
	bool CheckGround()
	{
		bool isOnSafeGround = false;
		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.forward, .5f, groundLayerMask);
		if (hitInfo.collider != null)
		{
			isOnSafeGround = true;
		}
		else
		{
			isOnSafeGround = false;
			Fall();
		}
		return isOnSafeGround;
	}

	public void Jump(bool setCharacterJumpingVar, Vector3 jumpDirection)//variable to set if the player is jumping
	{
		characterInfo.isJumping = setCharacterJumpingVar;
		mainJumpDirection = jumpDirection;
	}
	//this method is called if the character health reaches zero or he fall of the ground
	public void Die()
	{
		characterInfo.alive = false;
		movementOutputDataObject.Alive = false;
	}

	public void Fall()
	{
		//falling is only lethal to the player if he is in low life
		if (gameObject.CompareTag("Player"))
		{
			characterInfo.alive = false;
			movementOutputDataObject.Alive = false;

		}
		//falling is aways lethal to the enemy
		else
		{
			Die();
		}
	}
	//this method is called at the end of the death animation if the character is vanish from the game
	public void DestroyDeath()
	{
		Destroy(gameObject);
	}
	
	//call this method to reaspawn and revive the character to its initial location
	public void Respawn()
	{
		print("player respawnder");
		gameObject.transform.position = characterRespawnPosition;
		characterInfo.alive = true;
		movementOutputDataObject.Alive = true;
		movementOutputDataObject.Falling = false;
	}

	public String GetMovementOutputData()
	{
		return JsonUtility.ToJson(movementOutputDataObject);
	}


	public struct CharacterStatus
	{
		public bool movRight, movLeft, movUp, movDown;
		public bool isJumping; 
		public bool movHorizontal, movVertical;

		public bool characterLocked;
		
		//character life status
		public bool alive;

		public void Reset()
		{
			movUp = false;
			movDown = false;
			movLeft = false;
			movRight = false;

			isJumping = false;

			movHorizontal = false;
			movVertical = false;
		}
	}


}
[Serializable]
public class MovementOutputData
{
	public float Xspeed;
	public float Yspeed;
	public float LastX;
	public float LastY;
	public bool IsMoving;
	public bool IsJumping;

	public bool Falling;
	public bool Alive;
}
