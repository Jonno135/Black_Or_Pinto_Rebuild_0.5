using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BeanScript : MonoBehaviour {

	public GameManager gm;
	public GameObject black;
	public GameObject pinto;

	//public int blackProfit = 1;
	//public int pintoProfit = 1;

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
		if (clickedButton == black) {
			gm.setGuessBeans (1);  //goes through whole process of taking turn from guess to re-rolling beans.
			gm.hideBeans ();		//THEN hides beans, initiating the start of the next turn.
		}
		else if(clickedButton == pinto) {
			gm.setGuessBeans (2);
			gm.hideBeans ();
		} else {
			//error
		}
	}
	/*
	public int getBeanProfit(GameObject beanType){
		if(beanType == null){
			//error check
		}
		if(beanType == black){
			return blackProfit;
		} else if (beanType == pinto) {
			return pintoProfit;
		} else {
			return 0;
			//error check
		}
	}
	*/
}
