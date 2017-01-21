using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stadium : MonoBehaviour {

    public List<Wave> Waves;

    public Transform Center;

    private static Stadium instance;
    public static Stadium Instance {
        get {
            if (!instance) {
                instance = GameObject.FindObjectOfType<Stadium>();
            }

            return instance;
        }
    }

    [SerializeField]
    private float EnableScore;
    private ScoreController theScoreController;

    // Use this for initialization
    void Start () {
        Waves = new List<Wave>();
        theScoreController = FindObjectOfType<ScoreController>();
    }
	
	// Update is called once per frame
	void Update () {
        
        foreach (var wave in Waves) {

            EnableScore = wave.Tick(Time.deltaTime);
            //print(Mathf.Round(EnableScore));
            if (Mathf.Abs(Mathf.Round(EnableScore)) < 100 && Mathf.Abs(Mathf.Round(EnableScore)) > 80) {
                theScoreController.CanGainScore(3);
            }


            /*for (int i = 0; i < Columns.Count; i++) {
                foreach (var seats in Columns[i].Seats) {
                    if ((i - wave.waveLength) < wave.wavePos && (i + wave.waveLength) > wave.wavePos) {
                        maxes[i] = Mathf.Max((wave.waveLength - Mathf.Abs(wave.wavePos - i)) / wave.waveLength * maxYOffset, maxes[i]);
                    }
                }
            }*/
        }

        Waves.RemoveAll(wave => wave.IsDone);
    }

    public void GenerateRandomWave() {
        bool reversed = Random.Range(0, 10.0f) > 0.5f;

        float cone = Random.Range(15f, 25f);
        float speed = Random.Range(10f, 30f);
        float startDir = Random.Range(0f, 360f);
        var wave = new Wave(Center.position, speed, cone, startDir, reversed);
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