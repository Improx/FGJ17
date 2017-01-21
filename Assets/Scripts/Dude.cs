using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dude : MonoBehaviour {
	[SerializeField] private GameObject head;
	[SerializeField] private List<MeshRenderer> skinObjects;
	[SerializeField] private List<MeshRenderer> shirtObjects;
	[SerializeField] private List<MeshRenderer> pantsObjects;

	[SerializeField] private ColorRange skinColorRange;
	[SerializeField] private ColorRange shirtColorRange;
	[SerializeField] private ColorRange pantsColorRange;

    public Vector3 DirToCenter;

	public bool Shaming = false;
	private float unShameTime = 5.0f;
	[SerializeField] private float unShameTimer;
	private Quaternion lookingAt;

    public Cheer cheerer;
	private Vector3 defaultHeadRot;

	// Use this for initialization
	void Start () {

        var center = Stadium.Instance.Center.transform.position;
        DirToCenter = (center - transform.position).normalized;
		defaultHeadRot = head.transform.rotation.eulerAngles;

        DirToCenter.y = 0;
        DirToCenter.Normalize();
		RandomizeColors ();
    }

    // Update is called once per frame
    void Update () {

        float offset = 0;
        foreach (var wave in Stadium.Instance.Waves) {

                
            var angle = Vector3.Angle(-DirToCenter, wave.Direction);
            
            if (angle <= wave.ConeAngle / 2) {
                
                offset = (1-(angle / (wave.ConeAngle / 2)));
            }
        }
        SetYOffset(offset);

		// SHAME TIMER
		if (Shaming) {
			unShameTimer -= Time.deltaTime;

			if (lookingAt != GameController.Instance.playerReference.Head.transform.rotation) {
				LookAtPlayer (false);
			}
		}

		if (unShameTimer < 0) {
			Shaming = false;
			head.transform.rotation = Quaternion.Euler(defaultHeadRot);
		}
    }

    public void SetYOffset(float offset) {
        if (cheerer) {
            cheerer.Frame = offset;
        }
    }

	private void RandomizeColors(){
		Color skinColor = Random.ColorHSV (skinColorRange.hueMin, skinColorRange.hueMax, skinColorRange.satMin, skinColorRange.satMax, skinColorRange.valMin, skinColorRange.valMax);
		Color shirtColor = Random.ColorHSV (shirtColorRange.hueMin, shirtColorRange.hueMax, shirtColorRange.satMin, shirtColorRange.satMax, shirtColorRange.valMin, shirtColorRange.valMax);
		Color pantsColor = Random.ColorHSV (pantsColorRange.hueMin, pantsColorRange.hueMax, pantsColorRange.satMin, pantsColorRange.satMax, pantsColorRange.valMin, pantsColorRange.valMax);

		foreach (MeshRenderer m in skinObjects) {
			m.material.color = skinColor;
		}

		foreach (MeshRenderer m in shirtObjects) {
			m.material.color = shirtColor;
		}

		foreach (MeshRenderer m in pantsObjects) {
			m.material.color = pantsColor;
		}
	}

	public void LookAt(Vector3 pos, bool resetShameTime = true){
		lookingAt = Quaternion.LookRotation (pos - transform.position) * Quaternion.Euler (-Vector3.up * 90);
		head.transform.rotation = lookingAt;
		Shaming = true;

		if(resetShameTime)
			unShameTimer = unShameTime;
	}

	public void LookAtPlayer(bool resetShameTime = true){
		LookAt (GameController.Instance.playerReference.Head.transform.position, resetShameTime);
	}


    /*private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        float len = Vector3.Distance(transform.position, Stadium.Instance.Center.transform.position);

        Gizmos.DrawLine(transform.position, transform.position + DirToCenter * len);

    }*/
}

[System.Serializable]
public class ColorRange{
	public float hueMin;
	public float hueMax;
	public float satMin;
	public float satMax;
	public float valMin;
	public float valMax;
}
