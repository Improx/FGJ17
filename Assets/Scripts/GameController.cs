using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public GameObject DudePrefab;
    public GameObject PlayerPrefab;

	public PlayerController playerReference;

    public EventState State;

    public float EventTime;

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
        changeState(EventState.Wait);

    }
	
	// Update is called once per frame
	void Update () {
        switch (State) {
            case EventState.Wait:
                /*if (EventTime + 5 < Time.time) {
                    changeState(EventState.Idle);
                }*/
                if (Input.GetKeyDown(KeyCode.E)) {
                    changeState(EventState.Idle);
                }
                break;
            case EventState.Idle:
                if (Random.Range(0, 100) < 10) {
                    changeState(EventState.Waves);

                    Stadium.Instance.GenerateRandomWave();
                }
                break;
            case EventState.Waves:
                if (Stadium.Instance.Waves.Count == 0) {
                    changeState(EventState.Wait);
                }
                break;
            default:
                break;
        }
    }

    void changeState (EventState newState) {
        State = newState;
        EventTime = Time.time;
    }

    public void ChangePoints(int change) {

    }
}

public enum EventState {
    Wait,
    Idle,
    Waves
}

