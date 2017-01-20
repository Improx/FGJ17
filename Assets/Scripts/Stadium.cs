using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stadium : MonoBehaviour {

    public List<ListWrapper> Columns = new List<ListWrapper>();

    public bool DoWave;
    public float wavePos;
    public float waveSpeed;
    public float waveLength;

    public float maxYOffset;

    public int rowLength;

    // Use this for initialization
    void Start () {
        wavePos = -waveLength;
	}
	
	// Update is called once per frame
	void Update () {
		if (DoWave) {
            rowLength = Columns.Count;

            wavePos = Time.deltaTime * waveSpeed + wavePos;
            if (wavePos > (rowLength + waveLength)) {
                wavePos = -waveLength;


            }

            for (int i = 0; i < Columns.Count; i++) {
                foreach (var seats in Columns[i].Seats) {
                    float offset = 0;
                    if ((i - waveLength) < wavePos && (i + waveLength) > wavePos) {
                        offset = (waveLength - Mathf.Abs(wavePos - i)) / waveLength * maxYOffset;
                    }
                    if (seats.Ocupant) {
                        seats.Ocupant.SetYOffset(offset);
                    }
                }
            }
        }
	}
}

[System.Serializable]
public class ListWrapper {
    public List<Seat> Seats;

    public int Length {
        get {
            return Seats.Count;
        }
    }
}