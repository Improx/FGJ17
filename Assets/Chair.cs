using UnityEngine;
using System.Collections;

public class Chair : MonoBehaviour {

	[SerializeField] private GameObject dude;
	[SerializeField] private GameObject playerObj;
	[SerializeField] private Vector3 seatOffset;
	[SerializeField] private Vector3 rotationOffset;
	private GameObject myDude;
	private GameObject player;

	void Start(){
		if (player == null) {
			SpawnDudeOnChair ();
		}
	}

	private void SpawnDudeOnChair(){
		myDude = (GameObject)Instantiate (dude, transform.position + seatOffset, transform.rotation * Quaternion.Euler(rotationOffset));
	}

	public void SpawnPlayer(){
		print ("spawned player!");
		player = (GameObject)Instantiate (playerObj, transform.position + seatOffset + Vector3.up*10, transform.rotation);
	}
}
