using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stadium : MonoBehaviour {

    public List<ListWrapper> Columns = new List<ListWrapper>();
    

    public List<Wave> Waves;

    public float maxYOffset;

    public int rowLength;

    private static Stadium instance;
    public static Stadium Instance {
        get {
            if (!instance) {
                instance = GameObject.FindObjectOfType<Stadium>();
            }

            return instance;
        }
    }

    // Use this for initialization
    void Start () {
        Waves = new List<Wave>();
        rowLength = Columns.Count;
    }
	
	// Update is called once per frame
	void Update () {
        rowLength = Columns.Count;

        var maxes = new float[rowLength];
        foreach (var wave in Waves) {

            wave.Tick(Time.deltaTime);



            for (int i = 0; i < Columns.Count; i++) {
                foreach (var seats in Columns[i].Seats) {
                    if ((i - wave.waveLength) < wave.wavePos && (i + wave.waveLength) > wave.wavePos) {
                        maxes[i] = Mathf.Max((wave.waveLength - Mathf.Abs(wave.wavePos - i)) / wave.waveLength * maxYOffset, maxes[i]);
                    }
                }
            }
        }

        Waves.RemoveAll(wave => wave.IsDone);

        for (int i = 0; i < Columns.Count; i++) {
            foreach (var seat in Columns[i].Seats) {
                if (seat.Ocupant) {
                    seat.Ocupant.SetYOffset(maxes[i]);
                }
            }
        }

    }

    public void GenerateRandomWave() {
        bool reversed = Random.Range(0, 10.0f) > 0.5f;

        float length = Random.Range(1f, 2f);
        float speed = Random.Range(1f, 4f);
        var wave = new Wave(speed, length, reversed);
        Waves.Add(wave);
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