using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {

    [SerializeField]
    private Vector3 ballPosition;
	[SerializeField]
	private Stadium stad;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Football") {
            print("Score!!");
			stad.GenerateRandomWave ();
            other.transform.position = ballPosition;
        }
    }
}
