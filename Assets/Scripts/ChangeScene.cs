using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ChangeScene : MonoBehaviour {

	public GameManager gm;

	public void ChangeToMap(string scene) {
		//Awake ();
		Application.LoadLevel (scene);
 	}
}
