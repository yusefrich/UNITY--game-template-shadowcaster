using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGameBtn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		Scene scene = SceneManager.GetActiveScene ();
		string sceneName = scene.name;
		if (Input.GetButtonDown ("Interact")) {
			SceneManager.LoadScene (sceneName);

		}

	}
}
