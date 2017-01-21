using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WaveVisualizer : MonoBehaviour {

    public float length;

    private static WaveVisualizer instance;
    public static WaveVisualizer Instance {
        get {
            if (!instance) {
                instance = GameObject.FindObjectOfType<WaveVisualizer>();
            }

            return instance;
        }
    }

    public Wave wave;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    }

    private void OnDrawGizmos() {
        var dirAngle = wave.DirAngle;
        var coneAngle = wave.ConeAngle;
        var direcionL = new Vector3(Mathf.Sin(Mathf.Deg2Rad * (dirAngle + coneAngle / 2f)), 0, Mathf.Cos(Mathf.Deg2Rad * (dirAngle + coneAngle / 2f)));
        var direcionR = new Vector3(Mathf.Sin(Mathf.Deg2Rad * (dirAngle - coneAngle / 2f)), 0, Mathf.Cos(Mathf.Deg2Rad * (dirAngle - coneAngle / 2f)));

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + wave.Direction*length);


        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + direcionL * length);


        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + direcionR * length);

    }
}
