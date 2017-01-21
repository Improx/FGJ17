using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StadiumSpawner : MonoBehaviour {

	[SerializeField] private Transform stadiumParent;
	[SerializeField] private GameObject floorBlock;
	[SerializeField] private GameObject chair;
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject lamp;
	[SerializeField] private GameObject soccerGoal;
	[SerializeField] private GameObject soccerPlayer;
	[SerializeField] private GameObject soccerBall;

	[SerializeField] private float chairHeight;
	[SerializeField] private float chairWidth;
	[SerializeField] private int rowsX;
	[SerializeField] private int rowsY;
	[SerializeField] private int rowAmount;
	[SerializeField] private Vector2 rowOffset;
	[SerializeField] private Vector3 midAreaSize;
	[SerializeField] private int playerRowMinFromBottom;
	[SerializeField] private int playerRowMaxFromTop;
	[SerializeField] private Material grassMaterial;
	[SerializeField] private int soccerPlayerAmount;
	private int currentRow;
	private Vector3 offsetFromCenter;
	private GameObject ground;

	[SerializeField] private List<GameObject> playerSpawnCandidates;

	void Awake(){
		int i = Random.Range (0, playerSpawnCandidates.Count - 1);
		playerSpawnCandidates [i].GetComponent<Chair> ().SpawnPlayer();
	}

	public void SpawnStadium(){
		playerSpawnCandidates = new List<GameObject> ();

		ClearStadium ();

		stadiumParent = GetCenterObject ().transform;

		offsetFromCenter = Vector3.left * midAreaSize.x / 2 + Vector3.back * midAreaSize.y / 2 + Vector3.up*midAreaSize.z;
		for (int i = 0; i < rowAmount; i++) {
			if (i >= playerRowMinFromBottom && i <= rowAmount-playerRowMaxFromTop) {
				SpawnRectangle (i, true);
			} else {
				SpawnRectangle (i);
			}
		}

		SpawnLamps ();
		SpawnGround ();
		SpawnSoccer ();

	}

	private void SpawnLamps(){
		SpawnLamp (Vector3.left * (midAreaSize.x + (5+rowAmount) * chairWidth)/2 + Vector3.back * (midAreaSize.y + (5+rowAmount) * chairWidth)/2);
		SpawnLamp (Vector3.right * (midAreaSize.x + (5+rowAmount) * chairWidth)/2 + Vector3.back * (midAreaSize.y + (5+rowAmount) * chairWidth)/2);
		SpawnLamp (Vector3.left * (midAreaSize.x + (5+rowAmount) * chairWidth)/2 + Vector3.forward * (midAreaSize.y + (5+rowAmount) * chairWidth)/2);
		SpawnLamp (Vector3.right * (midAreaSize.x + (5+rowAmount) * chairWidth)/2 + Vector3.forward * (midAreaSize.y + (5+rowAmount) * chairWidth)/2);
	}

	private void SpawnGround(){
		ground = GameObject.CreatePrimitive (PrimitiveType.Plane);
		ground.GetComponent<MeshRenderer> ().sharedMaterial = grassMaterial;
		ground.transform.SetParent (stadiumParent);
		ground.transform.position = Vector3.zero;
		ground.transform.localScale = new Vector3 (midAreaSize.x, 0.01f, midAreaSize.y);
	}

	private void SpawnSoccer(){
		//RIGHT GOAL
		GameObject g = (GameObject)Instantiate (soccerGoal);
		g.transform.SetParent (stadiumParent);
		g.transform.position = Vector3.right * midAreaSize.x/2 * 0.6f;
		g.transform.rotation = Quaternion.Euler (Vector3.up * 90);

		//LEFT GOAL
		g = (GameObject)Instantiate (soccerGoal, stadiumParent);
		g.transform.SetParent (stadiumParent);
		g.transform.position = Vector3.left * midAreaSize.x/2 * 0.6f;
		g.transform.rotation = Quaternion.Euler (Vector3.up * -90);

		//PLAYERS
		for(int i = 0; i < soccerPlayerAmount; i++){
			g = (GameObject)Instantiate (soccerPlayer);
			g.transform.position = new Vector3 (Random.Range (-0.4f, 0.4f) * midAreaSize.x/2, 1f, Random.Range (-0.4f, 0.4f) * midAreaSize.y/2);
			g.transform.SetParent (stadiumParent);
		}

		//BALL
		g = (GameObject)Instantiate (soccerBall);
		g.transform.position = Vector3.up*3;
		g.transform.SetParent (stadiumParent);

	}

	private void SpawnRectangle(int row, bool spawnPlayer = false){

		int xAmount = Mathf.FloorToInt (midAreaSize.x + currentRow * rowOffset.x * row);
		int yAmount = Mathf.FloorToInt (midAreaSize.y + currentRow * rowOffset.x * row);

		/*
		 * SPAWN FLOOR BLOCKS
		 */
		//bottom
		SpawnBlock (offsetFromCenter + new Vector3 (0, rowOffset.y * row, -rowOffset.x * row) + Vector3.right * xAmount/2, Vector3.zero);
		//left
		SpawnBlock (offsetFromCenter + new Vector3(-rowOffset.x * row, rowOffset.y * row, 0) + Vector3.forward * xAmount/2, Vector3.up*90);
		//top
		SpawnBlock (offsetFromCenter + Vector3.forward * midAreaSize.y + new Vector3(0,rowOffset.y * row, rowOffset.x * row) + Vector3.right * xAmount/2, Vector3.up*180);
		//right
		SpawnBlock (offsetFromCenter + Vector3.right * midAreaSize.x + new Vector3(rowOffset.x * row, rowOffset.y * row, 0) + Vector3.forward * xAmount/2, Vector3.up*-90);

		/*
		 * SPAWN SEATS
		 */
		//bottom
		SpawnRow (offsetFromCenter + new Vector3(-chairWidth*row, rowOffset.y * row, -rowOffset.x * row), Vector3.right, xAmount + 5*row, Vector3.zero, true&&spawnPlayer);
		//left
		SpawnRow (offsetFromCenter + new Vector3(-rowOffset.x * row, rowOffset.y * row, -chairWidth*row), Vector3.forward, yAmount + 5*row, Vector3.up*90, false&&spawnPlayer);
		//top
		SpawnRow (offsetFromCenter + Vector3.forward * midAreaSize.y + new Vector3(-chairWidth*row,rowOffset.y * row, rowOffset.x * row), Vector3.right, xAmount + 5*row, Vector3.up*180, true&&spawnPlayer);
		//right
		SpawnRow (offsetFromCenter + Vector3.right * midAreaSize.x + new Vector3(rowOffset.x * row, rowOffset.y * row, -chairWidth*row), Vector3.forward, yAmount + 5*row, Vector3.up*-90, false&&spawnPlayer);
	}

	private void SpawnRow(Vector3 sPos, Vector3 growDir, int chairAmount, Vector3 rot, bool isPlayerSpawnCandidate){
		GameObject c;

		int rowChairCount = Mathf.FloorToInt(chairAmount / chairWidth);
		int playerSpawnPos = Mathf.FloorToInt (rowChairCount / 2);

		for (int i = 1; i < rowChairCount; i++) {
			Vector3 pos = sPos + i * growDir * chairWidth;
			c = GetChair ();
			c.transform.position = pos;
			c.transform.rotation = Quaternion.Euler(rot);

			if (isPlayerSpawnCandidate && i == playerSpawnPos) {
				playerSpawnCandidates.Add (c);
			}
		}
	}

	private void SpawnBlock(Vector3 pos, Vector3 rot){
		GameObject g = (GameObject) Instantiate (floorBlock, pos, Quaternion.Euler(rot));
		g.transform.SetParent (stadiumParent);
	}

	private void SpawnLamp(Vector3 pos){
		GameObject g = (GameObject) Instantiate (lamp);
		g.transform.SetParent (stadiumParent);
		g.transform.position = pos;

		Vector3 targetDir = Vector3.RotateTowards (g.transform.forward, stadiumParent.transform.position - g.transform.position, 2 * Mathf.PI, 0f);
		g.transform.rotation = Quaternion.LookRotation(targetDir);
	}

	private GameObject GetChair(){
		GameObject c = (GameObject)Instantiate (chair);
		c.transform.SetParent (stadiumParent);
		return c;
	}

	private GameObject GetCenterObject(){
		GameObject g = new GameObject ();
		g.AddComponent<Stadium> ();
		g.GetComponent<Stadium> ().Center = g.transform;
		g.name = "StadiumParent";
		g.transform.SetParent (transform);
		return g;
	}

	public void ClearStadium(){
		if (stadiumParent != null) {
			for (int i = stadiumParent.childCount - 1; i >= 0; i--) {
				DestroyImmediate (stadiumParent.GetChild (i).gameObject);
			}
			DestroyImmediate (stadiumParent.gameObject);
		}
		
		playerSpawnCandidates.Clear ();
	}
}
