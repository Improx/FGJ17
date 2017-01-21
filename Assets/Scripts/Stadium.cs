using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Stadium : MonoBehaviour {

    public List<Wave> Waves;

    public Transform Center;

    [SerializeField]
    private AudioClip cheerSound;
    private AudioSource myAudioSource;

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
        myAudioSource = GetComponent<AudioSource>();
        Waves = new List<Wave>();
        theScoreController = FindObjectOfType<ScoreController>();
    }
	
	// Update is called once per frame
	void Update () {
        
        foreach (var wave in Waves) {

            wave.Tick(Time.deltaTime);

        }

        Waves.RemoveAll(wave => wave.IsDone);
        if (Waves.Count <= 0)
        {
            myAudioSource.Stop();
        }
    }

    public void GenerateRandomWave() {
        bool reversed = Random.Range(0, 10.0f) > 0.5f;

        float cone = Random.Range(15f, 25f);
        float speed = Random.Range(10f, 30f);
        float startDir = Random.Range(0f, 360f);
        var wave = new Wave(Center.position, speed, cone, startDir, reversed);
        if (cheerSound != null) {
            myAudioSource.PlayOneShot(cheerSound);
            print("aaaa");
        }
        Waves.Add(wave);
    }

}