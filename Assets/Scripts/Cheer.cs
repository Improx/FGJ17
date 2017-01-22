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
                Anim.Play("Excited");
            } else {
				//anim ["Excited"].time = 0f;
                //anim.Stop("Excited");
				StopCoroutine(SitDown());
				StartCoroutine(SitDown());
            }
        }
    }

	Animation anim;
	Animation Anim{
		get{ 
			if (anim == null) {
				anim = GetComponent<Animation> ();
			}
			return anim;
		}
	}
	public AnimationClip cheer;

	// Use this for initialization
	void Start () {
		cheer.legacy = true;
		Anim.AddClip (cheer, "Excited");
		Anim ["Excited"].time = 1.0f;
		Anim ["Excited"].speed = 0.0f;
		/*anim.Play ("Excited");
		anim ["Excited"].speed = 0;*/
	}
	
	// Update is called once per frame
	void Update () {
		Anim ["Excited"].time = Frame;
	}

	private IEnumerator SitDown(){
		if (Anim != null) {
			while (Anim ["Excited"].time > 0) {
				Anim ["Excited"].time--;
				yield return new WaitForSeconds (0.1f);
			}
			Anim.Stop ("Excited");
		}
	}

	void OnDestroy(){
		StopCoroutine (SitDown ());
	}

	void Disable(){
		StopCoroutine (SitDown ());
	}
}
