using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheer : MonoBehaviour {
	[Range(0.0f, 100.0f)]
	[SerializeField] private float frame;
	Animation anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation> ();
		anim ["Excited"].clip.legacy = true;
		/*anim.Play ("Excited");
		anim ["Excited"].speed = 0;*/
	}
	
	// Update is called once per frame
	void Update () {
		//anim ["Excited"].time = frame;
	}
}
