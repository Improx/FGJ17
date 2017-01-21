using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StadiumSpawner : MonoBehaviour {

	[SerializeField] private Transform stadiumParent;
	[SerializeField] private GameObject floorBlock;
	[SerializeField] private GameObject chair;
	[SerializeField] private GameObject player;

	[SerializeField] private float chairHeight;
	[SerializeField] private float chairWidth;
	[SerializeField] private int rowsX;
	[SerializeField] private int rowsY;
	[SerializeField] private int rowAmount;
	[SerializeField] private Vector2 rowOffset;
	[SerializeField] private Vector2 midAreaSize;
	private int playerRow;
	private int currentRow;
	private Vector3 offsetFromCenter;

	[SerializeField] private List<GameObject> playerSpawnCandidates;

	void Awake(){
		int i = Random.Range (0, playerSpawnCandidates.Count - 1);
		playerSpawnCandidates [i].GetComponent<Chair> ().SpawnPlayer();
	}

	public void SpawnStadium(){
		playerSpawnCandidates = new List<GameObject> ();

		playerRow = Random.Range (3, rowAmount);

		ClearStadium ();

		stadiumParent = GetCenterObject ().transform;

		offsetFromCenter = Vector3.left * midAreaSize.x / 2 + Vector3.back * midAreaSize.y / 2;
		for (int i = 0; i < rowAmount; i++) {
			if (i == playerRow) {
				SpawnRectangle (i, true);
			} else {
				SpawnRectangle (i);
			}
		}


	}

	private void SpawnRectangle(int row, bool spawnPlayer = false){

		int xAmount = Mathf.FloorToInt (midAreaSize.x + currentRow * rowOffset.x * row);
		int yAmount = Mathf.FloorToInt (midAreaSize.y + currentRow * rowOffset.x * row);

		//bottom
		SpawnBlock (offsetFromCenter + new Vector3 (0, rowOffset.y * row, -rowOffset.x * row) + Vector3.right * xAmount/2, Vector3.zero);
		//left
		SpawnBlock (offsetFromCenter + new Vector3(-rowOffset.x * row, rowOffset.y * row, 0) + Vector3.forward * xAmount/2, Vector3.up*90);
		//top
		SpawnBlock (offsetFromCenter + Vector3.forward * midAreaSize.y + new Vector3(0,rowOffset.y * row, rowOffset.x * row) + Vector3.right * xAmount/2, Vector3.up*180);
		//right
		SpawnBlock (offsetFromCenter + Vector3.right * midAreaSize.x + new Vector3(rowOffset.x * row, rowOffset.y * row, 0) + Vector3.forward * xAmount/2, Vector3.up*-90);

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
		int playerSpawnPos = 0;

		int rowChairCount = Mathf.FloorToInt(chairAmount / chairWidth);

		for (int i = 1; i < rowChairCount; i++) {
			Vector3 pos = sPos + i * growDir * chairWidth;
			c = GetChair ();
			c.transform.position = pos;
			c.transform.rotation = Quaternion.Euler(rot);

			if (isPlayerSpawnCandidate && i < rowChairCount*0.6f && i > rowChairCount*0.4f) {
				playerSpawnCandidates.Add (c);
			}
		}
	}
	//asd

	private void SpawnBlock(Vector3 pos, Vector3 rot){
		GameObject g = (GameObject) Instantiate (floorBlock, pos, Quaternion.Euler(rot));
		g.transform.SetParent (stadiumParent);
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
