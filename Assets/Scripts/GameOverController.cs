using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour {
    [SerializeField]
    private GameObject gameOverScreen;

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


    public void LoseGame() {
        gameOverScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
        PlayerController.Instance.enabled = false;
        MouseAimCamera.Instance.enabled = false;
    }

    public void ReturnMainMenu()
    {
        print("Go main menu");
    }
    public void Exit()
    {
        print("Exit");
    }
}
