using UnityEngine;
using System.Collections;

public class StadiumSpawner : MonoBehaviour {

	[SerializeField] private GameObject floorBlock;
	[SerializeField] private GameObject chair;
	[SerializeField] private float chairHeight;
	[SerializeField] private float chairWidth;
	[SerializeField] private int rowsX;
	[SerializeField] private int rowsY;
	[SerializeField] private Vector2 rowOffset;
	[SerializeField] private Vector2 midAreaSize;
	private int currentRow;

	void Start(){
		Spawn ();
	}

	private void Spawn(){
		//Vector2 startingSize = midAreaSize + rows * rowOffset.x;
		GameObject b = (GameObject)Instantiate (floorBlock);
		SpawnRow (Vector3.zero, Mathf.FloorToInt(midAreaSize.x + currentRow*rowOffset.x) ,Vector3.zero);
		SpawnRow (Vector3.forward * midAreaSize.x, Mathf.FloorToInt(midAreaSize.x), Vector3.up*180);
		SpawnRow (Vector3.left * midAreaSize.y, Mathf.FloorToInt(midAreaSize.y), Vector3.up*90);
	}

	private void SpawnRow(Vector3 sPos, int chairAmount, Vector3 rot){
		GameObject c;
		for (int i = 0; i <  chairAmount / chairWidth; i++) {
			Vector3 pos = sPos + new Vector3(i * chairWidth, 0, 0);
			c = GetChair ();
			c.transform.position = pos;
			c.transform.rotation = Quaternion.Euler(rot);
		}
	}

	private GameObject GetChair(){
		GameObject c = (GameObject)Instantiate (chair);
		return c;
	}
}
