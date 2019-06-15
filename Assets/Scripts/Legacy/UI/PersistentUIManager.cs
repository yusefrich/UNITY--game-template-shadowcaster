using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentUIManager : MonoBehaviour {

    public static PersistentUIManager Instance { get; private set;}

    [Header("canvas used in the game")]
    public GameObject deathMenuCanvas;
	public GameObject[] goodFeedback;
	public GameObject pausedMenu;
	bool gamePaused = false;

    GameObject functionCaller;

	[Header("ShadowUI")]
	public Animator[] shadowUI;


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


	public void UpdateShadowUI(bool addShadow, bool activeShadow, int totalOfShadows, int instantiatedShadowsCount){
		for (int i = 0; i < totalOfShadows; i++) {
			shadowUI[i].SetBool("AddShadow", addShadow);

		}

		if(activeShadow)
		{
			for (int i = 0; i < instantiatedShadowsCount; i++)
			{
				shadowUI[i].SetBool("ActiveShadow", activeShadow);
			}
		} else {
			for (int i = 0; i < shadowUI.Length; i++)
			{
				shadowUI[i].SetBool("ActiveShadow", activeShadow);
			}
		}
	}

	public void PingGoodFeedback(){
		StartCoroutine (PingFeedback ());
		for (int i = 0; i < goodFeedback.Length; i++) {
			goodFeedback[i].SetActive (true);
		}
	}
	public void PingBadFeedback(){
		//bad feedback yet to implement

	}

	IEnumerator PingFeedback(){
		for (int i = 0; i < 2; i++) {
			if (i == 1) {
				goodFeedback[0].SetActive (false);
				goodFeedback[1].SetActive (false);
				goodFeedback[2].SetActive (false);
				goodFeedback[3].SetActive (false);
			}
			yield return new WaitForSeconds (.1f);
		}
	}

	public void PauseGame(bool tutorial){
		gamePaused = !gamePaused;
		
		if(!gamePaused){
			pausedMenu.GetComponent<PauseMenu>().DisableObjects();

		}
		pausedMenu.gameObject.SetActive (gamePaused);
		if(gamePaused){
			pausedMenu.GetComponent<PauseMenu>().Tutorial(tutorial);
		}

	}

	public bool GamePaused
	{
		get { return gamePaused; }
		set { gamePaused = value; }
	}
/*
	public bool GamePaused(){
		return gamePaused;
	}
*/
}
