using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour {

	public void ChangeToMap(string scene) {
		Application.LoadLevel (scene);

 	}
}