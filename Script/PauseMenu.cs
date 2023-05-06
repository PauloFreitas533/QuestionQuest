using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class PauseMenu : MonoBehaviour
{

    public Transform pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.gameObject.activeSelf)
            {
                pauseMenu.gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                pauseMenu.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        //Application.Quit(); trocar para essa chamada antes de buildar o jogo
        Time.timeScale = 1; // Despausa o jogo
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Process.GetCurrentProcess().Kill(); // Encerra o processo do jogo
        #endif
    }

    public void RestartGame(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
