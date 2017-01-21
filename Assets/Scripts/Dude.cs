using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dude : MonoBehaviour {
	[SerializeField] private List<MeshRenderer> skinObjects;
	[SerializeField] private List<MeshRenderer> shirtObjects;
	[SerializeField] private List<MeshRenderer> pantsObjects;

	[SerializeField] private ColorRange skinColorRange;
	[SerializeField] private ColorRange shirtColorRange;
	[SerializeField] private ColorRange pantsColorRange;


    public Vector3 DirToCenter;

    public Cheer cheerer;

	// Use this for initialization
	void Start () {

        var center = Stadium.Instance.Center.transform.position;
        DirToCenter = (center - transform.position).normalized;

        DirToCenter.y = 0;
        DirToCenter.Normalize();
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
