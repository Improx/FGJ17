using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    public GameObject DudePrefab;
    public GameObject PlayerPrefab;

	public List<Dude> dudes;

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


    private bool abouttostartwave = false;
	// Use this for initialization
	void Start () {
        changeState(EventState.Wait);

    }

	public void AllNPCsLookAtPlayer(float range){
		StartCoroutine (npcShame (range));
        ScoreController.Instance.isShamed = false;
        ScoreController.Instance.currentShame = 0;
    }

	private IEnumerator npcShame(float range){
		yield return new WaitForSeconds (0.5f);
		foreach (Dude d in dudes) {
			if (Mathf.Abs ((d.gameObject.transform.position - GameController.Instance.playerReference.transform.position).magnitude) < range) {
				if (ScoreController.Instance.Score < ScoreController.Instance.MinScore/2) {
					d.LookAtPlayer ("angry");
				} else {
					d.LookAtPlayer ("wtf");
				}
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
        switch (State) {
            case EventState.Wait:
                /*if (Input.GetKeyDown(KeyCode.E)) {
                    changeState(EventState.Idle);
                }*/
                float cooldown = Random.Range(5, 10);
                if (!abouttostartwave) {
                    StartCoroutine(StartWave(cooldown));
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
    private IEnumerator StartWave(float cooldown)
    {

        abouttostartwave = true;

        yield return new WaitForSeconds(cooldown);
        abouttostartwave = false;
        if (State == EventState.Wait) {
            State = EventState.Idle;
        }
    }
}

public enum EventState {
    Wait,
    Idle,
    Waves
}


