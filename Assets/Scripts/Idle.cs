using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MonoBehaviour {

	Animation anim;
	public AnimationClip headIdle;
	public AnimationClip headSpin;
	//public AnimationClip idleJump;
	public AnimationClip idleSway;
	public AnimationClip idleYawn;
	public AnimationClip legsIdle;
	//public AnimationClip idleRobot;
	private List<string> animNames = new List<string>();
	private float cheerframe;
	private int animationCounter;


	// Use this for initialization
	void Start () {
		cheerframe = gameObject.GetComponent<Cheer>().Frame;
		anim = GetComponent<Animation> ();
		animationCounter = Random.Range(0, 600);
		animNames.Add("HeadIdle");
		//animNames.Add("HeadSpin");
		animNames.Add("IdleSway");
		animNames.Add("IdleYawn");
		animNames.Add("LegsIdle");
		//animNames.Add("IdleRobot");
		anim.AddClip (headIdle, "HeadIdle");
		//anim.AddClip (headSpin, "HeadSpin");
		anim.AddClip (idleSway, "IdleSway");
		anim.AddClip (idleYawn, "IdleYawn");
		anim.AddClip (legsIdle, "LegsIdle");
		//anim.AddClip (idleRobot, "IdleRobot");



	}

	// Update is called once per frame
	void Update () {
		if ((animationCounter % 600 == 0) && !anim.isPlaying /*&& cheerframe == 0*/) {
			anim.Play (animNames [Random.Range (0, animNames.Count)]);
			print ("update");
		}
		animationCounter += 1;
	}
		
}
