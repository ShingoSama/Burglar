using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenuUI;
    public Button buttonStart;
    public void EndGame()
    {
        gameOverMenuUI.SetActive(true);
        buttonStart.Select();
    }
    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitTitleGame()
    {
        SceneManager.LoadScene("MainGameMenu");
    }
}
