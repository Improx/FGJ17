using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreController : MonoBehaviour {

    [SerializeField]
    private int maxScore;
    public float MaxScore
    {
        get { return maxScore; }
    }

    [SerializeField]
    private int minScore;
    public float MinScore
    {
        get { return minScore; }
    }


    private float score;
    public float Score
    {
        get { return score; }
    }

    public event Action UpdateScore;

    private bool canGainScoreBool;
    public bool CanGainScoreBool
    {
        get { return canGainScoreBool; }
    }

    private static ScoreController instance;
    public static ScoreController Instance
    {
        get
        {
            if (!instance)
            {
                instance = GameObject.FindObjectOfType<ScoreController>();
            }

            return instance;
        }
    }

    void Start () {
        if (UpdateScore != null)
        {
            UpdateScore();
        }
    }
	


    public void addScore(float scoreToAdd) {
        score += scoreToAdd;
        if (score > maxScore) {
            score = maxScore;
        }
        print(score);
        if (UpdateScore != null)
        {
            UpdateScore();
        }
    }

    public void reduceScore(float scoreToreduce)
    {
        score -= scoreToreduce;
        if (score < minScore)
        {
            score = minScore;
        }
        print(score);
        if (UpdateScore != null)
        {
            UpdateScore();
        }

		GameController.Instance.AllNPCsLookAtPlayer ();
    }

    public void endGame() {

    }

    public void CanGainScore(float duration) {
        StartCoroutine(CanGainScoreIENumerator(duration));
        print("Can gain score");
    }

    private IEnumerator CanGainScoreIENumerator(float duration)
    {
        canGainScoreBool = true;
        yield return new WaitForSeconds(duration);
        canGainScoreBool = false;
        print("No more score");

    }


}
