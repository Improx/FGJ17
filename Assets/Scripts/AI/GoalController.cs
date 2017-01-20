using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {

    [SerializeField]
    private Vector3 ballPosition;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Football") {
            print("Score!!");
            other.transform.position = ballPosition;
        }
    }
}
