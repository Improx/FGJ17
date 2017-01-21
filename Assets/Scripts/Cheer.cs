using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheer : MonoBehaviour {

    private float frame;
	public float Frame {
        get {
            return frame;
        }
        set {
            frame = value;

            if (frame > 0) {
                anim.Play("Excited");
            } else {
                anim.Stop("Excited");
            }
        }
    }

	Animation anim;
	public AnimationClip cheer;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation> ();
		cheer.legacy = true;
		anim.AddClip (cheer, "Excited");
		anim ["Excited"].time = 1.0f;
		anim ["Excited"].speed = 0.0f;
		/*anim.Play ("Excited");
		anim ["Excited"].speed = 0;*/
	}
	
	// Update is called once per frame
	void Update () {
		anim ["Excited"].time = Frame;
	}

    
}
