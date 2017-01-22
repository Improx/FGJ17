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
	[SerializeField] private GameObject waveSoundObject;
    private ScoreController theScoreController;

    // Use this for initialization
    void Start () {
        Waves = new List<Wave>();
        theScoreController = FindObjectOfType<ScoreController>();
    }
	
	// Update is called once per frame
	void Update () {
        
        foreach (var wave in Waves) {

            wave.Tick(Time.deltaTime);

        }


		List<Wave> waves = Waves.FindAll (wave => wave.IsDone);
		foreach (Wave w in waves) {
			if (w.IsDone) {
				Destroy (w.SoundSource);
				Waves.Remove (w);
			}
		}
    }

    public void GenerateRandomWave() {
        bool reversed = Random.Range(0, 10.0f) > 5.0f;

        float cone = Random.Range(15f, 25f);
        float speed = Random.Range(10f, 30f);
        float startDir = Random.Range(0f, 360f);
		GameObject soundobj = (GameObject)Instantiate (waveSoundObject);
		var wave = new Wave(Center.position, speed, cone, startDir, reversed, soundobj.GetComponent<WaveSoundSource>());
		soundobj.GetComponent<WaveSoundSource> ().Init (transform, speed);

        Waves.Add(wave);
    }

}