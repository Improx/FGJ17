using UnityEngine;
using System.Collections;

public class StadiumSpawner : MonoBehaviour {

	[SerializeField] private GameObject floorBlock;
	[SerializeField] private GameObject chair;
	[SerializeField] private float chairHeight;
	[SerializeField] private float chairWidth;
	[SerializeField] private int rowsX;
	[SerializeField] private int rowsY;
	[SerializeField] private int rowAmount;
	[SerializeField] private Vector2 rowOffset;
	[SerializeField] private Vector2 midAreaSize;
	private int currentRow;

	void Start(){
		SpawnStadium ();
	}

	public void SpawnStadium(){
		for (int i = 0; i < rowAmount; i++) {
			SpawnRectangle (i);
		}
	}

	private void SpawnRectangle(int row){
		//Vector2 startingSize = midAreaSize + rows * rowOffset.x;
		GameObject b = (GameObject)Instantiate (floorBlock);

		int xAmount = Mathf.FloorToInt (midAreaSize.x + currentRow * rowOffset.x * row);
		int yAmount = Mathf.FloorToInt (midAreaSize.y + currentRow * rowOffset.x * row);

		SpawnBlock (Vector3.zero + new Vector3 (0, rowOffset.y * row, -rowOffset.x * row) + Vector3.right * xAmount/2, Vector3.zero);
		SpawnBlock (Vector3.zero + new Vector3(-rowOffset.x * row, rowOffset.y * row, 0) + Vector3.forward * xAmount/2, Vector3.up*90);
		SpawnBlock (Vector3.forward * midAreaSize.y + new Vector3(0,rowOffset.y * row, rowOffset.x * row) + Vector3.right * xAmount/2, Vector3.up*180);
		SpawnBlock (Vector3.right * midAreaSize.x + new Vector3(rowOffset.x * row, rowOffset.y * row, 0) + Vector3.forward * xAmount/2, Vector3.up*-90);

		SpawnRow (Vector3.zero + new Vector3(-chairWidth*row, rowOffset.y * row, -rowOffset.x * row), Vector3.right, xAmount + 5*row, Vector3.zero);
		SpawnRow (Vector3.zero + new Vector3(-rowOffset.x * row, rowOffset.y * row, -chairWidth*row), Vector3.forward, yAmount + 5*row, Vector3.up*90);
		SpawnRow (Vector3.forward * midAreaSize.y + new Vector3(-chairWidth*row,rowOffset.y * row, rowOffset.x * row), Vector3.right, xAmount + 5*row, Vector3.up*180);
		SpawnRow (Vector3.right * midAreaSize.x + new Vector3(rowOffset.x * row, rowOffset.y * row, -chairWidth*row), Vector3.forward, yAmount + 5*row, Vector3.up*-90);
	}

	private void SpawnRow(Vector3 sPos, Vector3 growDir, int chairAmount, Vector3 rot){
		GameObject c;
		for (int i = 1; i < (chairAmount / chairWidth); i++) {
			Vector3 pos = sPos + i * growDir * chairWidth;
			c = GetChair ();
			c.transform.position = pos;
			c.transform.rotation = Quaternion.Euler(rot);
		}
	}

	private void SpawnBlock(Vector3 pos, Vector3 rot){
		Instantiate (floorBlock, pos, Quaternion.Euler(rot));
	}

	private GameObject GetChair(){
		GameObject c = (GameObject)Instantiate (chair);
		return c;
	}
}
