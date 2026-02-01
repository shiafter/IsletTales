using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public bool UIBlockingInput;
    public GameObject gameOverUI;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1.0f;
        UIBlockingInput = false;
    }
    public void OnPlayerDead()
    {
        UIBlockingInput = true;

        if(gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            MusicManager.StopBackgroundMusic();
        }
        Time.timeScale = 0;
    }
}
