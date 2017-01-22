using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

	[SerializeField] private List<GoalController> goals;
	[SerializeField] private List<Text> scoreTexts;

	void Update(){
		for (int i = 0; i < scoreTexts.Count; i++) {
			scoreTexts [i].text = goals [i].score.ToString ();
		}
	}
}
