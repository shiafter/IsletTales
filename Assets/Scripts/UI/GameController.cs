using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public bool UIBlockingInput;

    public GameObject gameOverUI;
    public GameObject winUI;

    private bool gameEnd = false;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1.0f;
        UIBlockingInput = false;
        gameEnd = false;
    }
    public void OnPlayerDead()
    {
        if (gameEnd) return;
        gameEnd=true;

        UIBlockingInput = true;

        if(gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            MusicManager.StopBackgroundMusic();
        }
        Time.timeScale = 0;
    }
    public void OnGameWin()
    {
        if (gameEnd) return;
        gameEnd = true;

        UIBlockingInput = true;
        Time.timeScale = 0f;

        if (winUI)
            winUI.SetActive(true);
    }
}
