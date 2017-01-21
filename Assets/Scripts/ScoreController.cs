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

	void Start () {
        if (UpdateScore != null)
        {
            UpdateScore();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q)) {
            addScore(1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            reduceScore(1);
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
    }

    public void endGame() {

    }
}
