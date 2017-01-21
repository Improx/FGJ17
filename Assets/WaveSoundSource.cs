using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSoundSource : MonoBehaviour {

	private float rotationSpeed = 75.0f; // Degrees per second
	private Transform target;
	private Quaternion qTo;
	[SerializeField] private AudioSource source;

	public void Init(Transform middle, float rotSpeed){
		target = middle;
		rotationSpeed = rotSpeed;
		source.Play ();
	}

	void Update() {
		Vector3 v3 = target.position - transform.position;
		float angle = Mathf.Atan2(v3.y, v3.x) * Mathf.Rad2Deg;
		qTo = Quaternion.AngleAxis (angle, Vector3.forward);
		transform.rotation = Quaternion.RotateTowards (transform.rotation, qTo, rotationSpeed * Time.deltaTime);
	}
}
