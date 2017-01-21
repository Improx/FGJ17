using UnityEngine;
using System.Collections;

public class Chair : MonoBehaviour {

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
		myDude = (GameObject)Instantiate (GameController.Instance.DudePrefab, transform.position + seatOffset, transform.rotation * Quaternion.Euler(rotationOffset));
	}

	public void SpawnPlayer(){
		print ("spawned player!");
		player = (GameObject)Instantiate (GameController.Instance.PlayerPrefab, transform.position + seatOffset, transform.rotation * Quaternion.Euler(rotationOffset));
	}
}
