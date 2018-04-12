using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform Target;
    public Vector3 Offset = new Vector3(0, 1, -1);
	// Use this for initialization
	void Start () {
		if(Target == null)
        {
            enabled = false;
        }
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = Target.position + Offset;
	}
}
