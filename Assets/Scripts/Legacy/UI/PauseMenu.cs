using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class PauseMenu : MonoBehaviour {

	public Player player;
    public GameObject tutorial;
    public GameObject pause;

    bool isTutorial = false;


	
	// Update is called once per frame
	void Update () {
        if (!isTutorial)
        {
            if (Input.GetButtonDown("CastShadow"))
            {
                Scene scene = SceneManager.GetActiveScene();
                string sceneName = scene.name;
                SceneManager.LoadScene(sceneName);
            }
            if (Input.GetButtonDown("CastFirstShadow"))
            {
                player.EndLife();
				PersistentUIManager.Instance.PauseGame(false);

            }
            if (Input.GetButtonDown("DeleteShadows"))
            {
                Application.Quit();
            }

        }

        if (Input.GetButtonDown("Interact"))
        {
			PersistentUIManager.Instance.PauseGame(false);
        }

	}
    public void DisableObjects()
    {
        tutorial.SetActive(false);
        pause.SetActive(false);

    }

    public void Tutorial(bool tutorialValue)
    {
        if(tutorialValue){
            tutorial.SetActive(true);
            pause.SetActive(false);

        }else {
            tutorial.SetActive(false);
            pause.SetActive(true);

        }
        isTutorial = tutorialValue;
    } 

}
