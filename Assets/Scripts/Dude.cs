using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dude : MonoBehaviour {

    public Vector3 pos;
	[SerializeField] private List<MeshRenderer> skinObjects;
	[SerializeField] private List<MeshRenderer> shirtObjects;
	[SerializeField] private List<MeshRenderer> pantsObjects;

	[SerializeField] private ColorRange skinColorRange;
	[SerializeField] private ColorRange shirtColorRange;
	[SerializeField] private ColorRange pantsColorRange;


	// Use this for initialization
	void Start () {
        pos = transform.position;
		RandomizeColors ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Stadium.Instance != null) {
			foreach (var wave in Stadium.Instance.Waves) {

				var dir = (new Vector3 (transform.position.x, 0, transform.position.z).normalized -
				                      new Vector3 (wave.Origin.x, 0, wave.Origin.z).normalized).normalized;

				var angle = Vector3.Angle (dir, wave.Direction);

				//Debug.Log(angle);
				if (angle <= wave.ConeAngle / 2) {
					SetYOffset (2);
				} else {
					SetYOffset (0);
				}
			}
		}
    }

    public void SetYOffset(float offset) {
        transform.position = pos + new Vector3(0, offset, 0);
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
