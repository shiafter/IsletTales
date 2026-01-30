using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public bool UIBlockingInput;
    private void Awake()
    {
        instance = this;
    }
    public void OnPlayerDead()
    {
        UIBlockingInput = true;

        Time.timeScale = 0;
    }
}
