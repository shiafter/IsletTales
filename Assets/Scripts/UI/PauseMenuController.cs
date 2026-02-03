using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseMenu;
    public GameObject settingPanel;
    private bool inSettingPanel = false;
    private void Awake()
    {
        gamePaused = false;
        Time.timeScale = 1f;

        pauseMenu.SetActive(false);
        settingPanel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inSettingPanel)
            {
                BackFromSetting();
            }
            else if(gamePaused)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }
     public void Continue()
    {
        pauseMenu.SetActive(false);
        settingPanel.SetActive(false);

        Time.timeScale = 1.0f;
        GameController.instance.UIBlockingInput = false;
        gamePaused = false;
        inSettingPanel = false;
    }
    void Pause()
    {
        pauseMenu.SetActive(true);
        settingPanel.SetActive(false);

        Time.timeScale = 0f;
        GameController.instance.UIBlockingInput = true;
        gamePaused = true;
        inSettingPanel = false;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        gamePaused = false;
        inSettingPanel = false;

        SceneManager.LoadScene("MainMenuScene");
        MusicManager.PlayBackgroundMusic(false);
    }
    public void LoadSetting()
    {
        pauseMenu.SetActive(false);
        settingPanel.SetActive(true);

        inSettingPanel = true;
        GameController.instance.UIBlockingInput = true;
    }
    public void BackFromSetting()
    {
        settingPanel.SetActive(false);
        pauseMenu.SetActive(true);

        inSettingPanel = false;
        GameController.instance.UIBlockingInput = true;
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
