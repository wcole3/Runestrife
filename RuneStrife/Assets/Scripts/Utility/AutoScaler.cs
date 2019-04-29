using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScaler : MonoBehaviour {
    //smoothly shrink or grow objects
    public float scalerSpeed;
	
	// Update is called once per frame
	void Update () {
        transform.localScale += (new Vector3(scalerSpeed, scalerSpeed, scalerSpeed) * Time.deltaTime);
	}
}
