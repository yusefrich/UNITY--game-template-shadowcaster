using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Interact"))
        {
            SceneManager.LoadScene("SampleScene");
        }

	}
}
