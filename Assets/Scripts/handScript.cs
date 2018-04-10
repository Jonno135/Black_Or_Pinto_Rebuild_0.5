using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class handScript : MonoBehaviour {

	public GameManager gm;
	public GameObject left;
	public GameObject right;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onTap(){
		GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
		//print ();
		if (clickedButton == null) {// just to be on the safe side
			return;
		}

		if (clickedButton == left) {
			gm.setGuessHands (1);
			gm.showBeans ();
		}
		else if(clickedButton == right) {
			gm.setGuessHands (2);
			gm.showBeans ();
		} else {
			//error
		}
	}

}
