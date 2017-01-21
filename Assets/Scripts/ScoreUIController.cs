using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUIController : MonoBehaviour {

    private ScoreController theScoreController;
    [SerializeField]
    private Slider EmbarassmentSlider;
    [SerializeField]
    private Image fillArea;

    [SerializeField]
    private Color happyColor;
    [SerializeField]
    private Color embarassmentColor;
    [SerializeField]
    private Color neutralColor;


    // Use this for initialization
    void Start () {
        theScoreController = FindObjectOfType<ScoreController>();
        EmbarassmentSlider.maxValue = theScoreController.MaxScore;
        EmbarassmentSlider.minValue = theScoreController.MinScore;
        EmbarassmentSlider.value = 0;
        theScoreController.UpdateScore += UpdateScore;

    }
	
	// Update is called once per frame
	private void UpdateScore() {
        EmbarassmentSlider.value = theScoreController.Score;
        if (EmbarassmentSlider.value > 0)
        {
            fillArea.color = happyColor;
        }
        else if (EmbarassmentSlider.value < 0)
        {
            fillArea.color = embarassmentColor;
        }
        else {
            fillArea.color = neutralColor;
        }
        print(theScoreController.Score);

    }
}
