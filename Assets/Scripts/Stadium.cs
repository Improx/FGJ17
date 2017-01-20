using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stadium : MonoBehaviour {

    public List<ListWrapper> Rows = new List<ListWrapper>();

    public bool DoWave;
    public float wavePos;
    public float waveSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (DoWave) {
            int rowLength = Rows[0].Length;

            wavePos = Time.deltaTime * waveSpeed + wavePos;
            if (wavePos > rowLength) {
                wavePos = wavePos - rowLength;
            }
        }
	}
}

[System.Serializable]
public class ListWrapper {
    public List<GameObject> Seats;

    public int Length {
        get {
            return Seats.Count;
        }
    }
}