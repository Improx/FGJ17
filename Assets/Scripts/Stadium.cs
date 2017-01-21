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
        print(startDir);
        //Vector3 k = new Vector3(Mathf.Sin(Mathf.Deg2Rad * startDir), 0, Mathf.Cos(Mathf.Deg2Rad * startDir)).normalized;
        float kaa = (Center.position.z - PlayerDude.Instance.transform.position.z) / (Center.position.x - PlayerDude.Instance.transform.position.x);
        //print(k);
        print(kaa);
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