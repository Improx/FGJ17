using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dude : MonoBehaviour {

    public Vector3 pos;
	[SerializeField] private List<MeshRenderer> skinObjects;
	[SerializeField] private List<MeshRenderer> shirtObjects;
	[SerializeField] private List<MeshRenderer> pantsObjects;

	// Use this for initialization
	void Start () {
        pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
            foreach (var wave in Stadium.Instance.Waves) {

                var dir = (new Vector3(transform.position.x, 0, transform.position.z).normalized -
                    new Vector3(wave.Origin.x, 0, wave.Origin.z).normalized).normalized;

                var angle = Vector3.Angle(dir, wave.Direction);

                //Debug.Log(angle);
                if (angle <= wave.ConeAngle / 2) {
                    SetYOffset(2);
                } else {
                    SetYOffset(0);
                }
            }
    }

    public void SetYOffset(float offset) {
        transform.position = pos + new Vector3(0, offset, 0);
    }

	private void RandomizeColors(){
		
	}
}
