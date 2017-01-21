using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dude : MonoBehaviour {

    public Vector3 pos;
	[SerializeField] private List<MeshRenderer> skinObjects;
	[SerializeField] private List<MeshRenderer> shirtObjects;
	[SerializeField] private List<MeshRenderer> pantsObjects;

    public Vector3 DirToCenter;

	// Use this for initialization
	void Start () {
        pos = transform.position;

        var center = Stadium.Instance.Center.transform.position;
        DirToCenter = (center - transform.position).normalized;

        DirToCenter.y = 0;
        DirToCenter.Normalize();
    }

    // Update is called once per frame
    void Update () {
            foreach (var wave in Stadium.Instance.Waves) {

                
                var angle = Vector3.Angle(-DirToCenter, wave.Direction);
            
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

    /*private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        float len = Vector3.Distance(transform.position, Stadium.Instance.Center.transform.position);

        Gizmos.DrawLine(transform.position, transform.position + DirToCenter * len);

    }*/
}
