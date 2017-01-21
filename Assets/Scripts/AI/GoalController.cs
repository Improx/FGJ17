using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {

    [SerializeField]
    private Vector3 ballPosition;
	[SerializeField]
	private static Stadium instance;
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
            //print("Score!!");
			Instance.GenerateRandomWave ();
            other.transform.position = ballPosition;
        }
    }
}
