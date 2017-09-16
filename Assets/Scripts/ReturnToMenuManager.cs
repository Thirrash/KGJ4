using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMenuManager : MonoBehaviour {
	
	void Update () {
		if(Input.GetKey(KeyCode.Escape))
            Application.LoadLevel(0);
    }
}
