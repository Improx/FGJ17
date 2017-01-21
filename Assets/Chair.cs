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
		myDude = (GameObject)Instantiate (GameController.Instance.DudePrefab);
		myDude.transform.position = transform.position + transform.rotation * seatOffset;
		myDude.transform.rotation = transform.rotation * Quaternion.Euler(rotationOffset);
		GameController.Instance.dudes.Add (myDude.GetComponent<Dude>());
	}

	public void SpawnPlayer(){
		player = (GameObject)Instantiate (GameController.Instance.PlayerPrefab);
		player.transform.position = transform.position + transform.rotation * seatOffset;
		player.transform.rotation = transform.rotation * Quaternion.Euler(rotationOffset);
		GameController.Instance.playerReference = player.GetComponent<PlayerController>();
	}
}
