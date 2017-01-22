using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalController : MonoBehaviour {

    [SerializeField]
    private Vector3 ballPosition;
	[SerializeField]
	private static Stadium instance;
    [SerializeField]
	public Text ScoreTextObject;

    public int score = 0;

    void Start()
    {
        ScoreTextObject.text = score.ToString();
    }

	public static Stadium Instance {
		get {
			if (!instance) {
				instance = GameObject.FindObjectOfType<Stadium>();
			}

			return instance;
		}
	}

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Football") {
            print("Score!!");
			Instance.GenerateRandomWave ();
            other.transform.position = ballPosition;
            score += 1;
            ScoreTextObject.text = score.ToString();
            print(score);
        }
    }
}
