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

	public bool isShamed;
	public float currentShame;
	[SerializeField]
	private float shameLimit;
	[SerializeField]
	private float shameReducedPerFrame;

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

	void Update() {
		if (!isShamed && currentShame >= 0) {
			currentShame -= shameReducedPerFrame * Time.deltaTime;
		}
	}

	public void addScore(float scoreToAdd) {
		score += scoreToAdd;
		if (score > maxScore) {
			score = maxScore;
		}
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
		if (UpdateScore != null)
		{
			UpdateScore();
		}

		StackingShame(scoreToreduce);
		//GameController.Instance.AllNPCsLookAtPlayer (score.Scale(minScore,maxScore, 10f,50f));
	}

	private void StackingShame(float shameToAdd) {
		currentShame += shameToAdd;
		if (currentShame >= shameLimit) {
			currentShame = shameLimit;
			GameController.Instance.AllNPCsLookAtPlayer(score.Scale(minScore, maxScore, 10f, 50f));
		}
	}


	public void endGame() {
		if (score <= minScore) {
			print("You lost");
		}

	}

	public void CanGainScore(float duration) {
		if (!CanGainScoreBool) {
			StartCoroutine(CanGainScoreIENumerator(duration));
			print("Can gain score");
		}

	}

	private IEnumerator CanGainScoreIENumerator(float duration)
	{
		canGainScoreBool = true;
		yield return new WaitForSeconds(duration);
		canGainScoreBool = false;
		print("No more score");

	}


}