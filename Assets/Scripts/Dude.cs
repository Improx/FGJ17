using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dude : MonoBehaviour {

    public Vector3 pos;

	// Use this for initialization
	void Start () {
        pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void SetYOffset(float offset) {
        transform.position = pos + new Vector3(0, offset, 0);
    }
}
