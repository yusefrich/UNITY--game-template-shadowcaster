using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentGameManager : MonoBehaviour {

	public static PersistentGameManager Instance { get; private set;}

	private bool gamePaused = false;
	private bool playerLock = false;
	
	private void Awake()
	{
		if (Instance == null) 
		{
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else 
		{
			Destroy (gameObject);

		}
	}

	private void Update()
	{
		if (Input.GetButtonDown("Start"))
		{
			PersistentUIManager.Instance.PauseGame (false);
		}
		if (Input.GetButtonDown("Back"))
		{			
			PersistentUIManager.Instance.PauseGame (true);
		}

	}

	public void PauseGame(){
		gamePaused = true;
		GameObject.FindWithTag("Player").GetComponent<CustomCharacterController>().characterInfo.characterLocked = true;
	}
	public void ReturnGame(){
		GameObject.FindWithTag("Player").GetComponent<CustomCharacterController>().characterInfo.characterLocked = false;
		gamePaused = false;

	}
	public void LockPlayer(){
		playerLock = true;
	}
	public void UnlockPlayer(){
		playerLock = false;
	}
	public void ResetGame(){

	}

	//GetFunctions
	public bool GetPlayerLocked(){
		return playerLock;
	}
	public bool GetGamePaused(){
		return gamePaused;
	}

}
