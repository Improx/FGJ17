using UnityEngine;
using System.Collections;

public class Chair : MonoBehaviour {

	[SerializeField] private GameObject dude;
	[SerializeField] private Vector3 seatOffset;
	[SerializeField] private Vector3 rotationOffset;
	private GameObject myDude;

	void Start(){
		SpawnDudeOnChair ();
	}

	private void SpawnDudeOnChair(){
		myDude = (GameObject)Instantiate (dude, transform.position + seatOffset, transform.rotation * Quaternion.Euler(rotationOffset));
	}
}
