using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public GameObject DudePrefab;

    public EventState State;

    private static GameController instance;
    public static GameController Instance {
        get {
            if (!instance) {
                instance = GameObject.FindObjectOfType<GameController>();
            }
            return instance;
        }
    }

    public int Points;
	// Use this for initialization
	void Start () {
        State = EventState.Idle;
	}
	
	// Update is called once per frame
	void Update () {
        switch (State) {
            case EventState.Idle:
                if (Random.Range(0, 100) < 10) {
                    State = EventState.Waves;

                    Stadium.Instance.GenerateRandomWave();
                }
                break;
            case EventState.Waves:
                if (Stadium.Instance.Waves.Count == 0) {
                    State = EventState.Idle;
                }
                break;
            default:
                break;
        }
    }

    public void ChangePoints(int change) {

    }
}

public enum EventState {
    Idle,
    Waves
}

