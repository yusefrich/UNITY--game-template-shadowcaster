using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Door : MonoBehaviour {

	public GameObject[] switches;
	public GameObject barrier;

    [Header("Barier")]
    public Animator anim;

	[FormerlySerializedAs("doorActive")] public bool doorOpen = false;
	bool feedbackPingOnce = true;


	private void Start()
	{
		CheckDoorBarrier();
	}

	void Update () 
	{
		//refactor part
		if (!doorOpen)
		{
			if (CheckButtons())
			{
				doorOpen = true;
				CheckDoorBarrier();
			}
		}
		
	}

	void CheckDoorBarrier()
	{
		if (doorOpen)
		{
			OpenBarrier();
		}
	}
	
    void OpenBarrier(){
        if(anim != null){
            anim.SetTrigger("Unlock");
        }else {
            print("Door Animator not atached");
        }

		if (feedbackPingOnce) {
			//PersistentUIManager.Instance.PingGoodFeedback (); //needs PersistentUIManager refactoring
			feedbackPingOnce = false;
		}
		barrier.SetActive (false);
	}


	bool CheckButtons(){ 
		//refactor part
		
		foreach (GameObject btnSwitch in switches)
		{
			if (!btnSwitch.GetComponent<DoorSwitch>().isBeingPressed())
			{
				return false;
			}
		}
		return true;
	}
}
