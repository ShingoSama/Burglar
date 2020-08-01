using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainGameMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public static bool isPlayerDead = false;
    public GameObject pauseMenuUI;
    public Button buttonBack;
    public Button buttonStart;
    public GameObject MainGameMenuUI;
    public GameObject OptionsMainGameMenuUI;
    public InventoryUI inventoryUI;
    private bool isInInventaryUI;
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuUI.active)
            {
                ResumeGame();
            }
            else
            {
                isInInventaryUI = inventoryUI.GetInventoryUIActive();
                if (!isPlayerDead)
                {
                    if (!isInInventaryUI)
                    {
                        if (!gameIsPaused)
                            PauseGame();
                    }
                }
            }
        }
    }
    public void OnClickMainGameOptionsClick()
    {
        MainGameMenuUI.SetActive(false);
        OptionsMainGameMenuUI.SetActive(true);
        buttonBack.Select();
    }
    public void OnClickMainGameBackClick()
    {
        OptionsMainGameMenuUI.SetActive(false);
        MainGameMenuUI.SetActive(true);
        buttonStart.Select();
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void ExitTitleGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene("MainGameMenu");
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        buttonStart.Select();
    }
    public void PlayerDead(bool dead)
    {
        isPlayerDead = dead;
    }
    public bool GetMenuUIActive()
    {
        return pauseMenuUI.active;
    }
}
