using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private string menuScene = "MainMenu";
    private static GameOverController instance;
    public static GameOverController Instance
    {
        get
        {
            if (!instance)
            {
                instance = GameObject.FindObjectOfType<GameOverController>();
            }

            return instance;
        }
    }

    private float survivalTime;
    private bool timeActive;
    [SerializeField]
    private Text endText;
    void Awake() {
        survivalTime = 0;
        timeActive = true;
    }
    void Update() {
        if (timeActive) { survivalTime += Time.deltaTime; }
        
    }

    public void LoseGame() {
        timeActive = false;
        endText.text = "Game over!\n You survived " + Mathf.Round(survivalTime) + " seconds.";
        gameOverScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
        PlayerController.Instance.enabled = false;
        MouseAimCamera.Instance.enabled = false;
    }

    public void ReturnMainMenu()
    {
        print("Go main menu");
        SceneManager.LoadScene(menuScene);
    }
    public void Exit()
    {
        print("Exit");
        Application.Quit();
    }
}
