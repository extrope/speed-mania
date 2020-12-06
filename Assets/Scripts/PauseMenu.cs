using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        // Check if Escape key is pressed on keyboard
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // Check if game is paused
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }
    // Resume function
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    // Pause function
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    // Return to Menu function
    public void GoToMenu ()
    {
        SceneManager.LoadScene("MainMenu");
    }
    // Exit Game button
    public void ExitGame ()
    {
        Application.Quit();
    }

    public void ResetRace()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
    }
}
