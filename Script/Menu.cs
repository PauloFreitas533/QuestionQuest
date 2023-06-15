using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip buttonAudio;

    public void StartGame()
    {
        Destroy(FindObjectOfType<Question1>());
        Destroy(FindObjectOfType<Question2>());
        Destroy(FindObjectOfType<Question3>());
        Destroy(FindObjectOfType<Question4>());
        Destroy(FindObjectOfType<Question5>());
        Time.timeScale = 1;
        OnClickButton();
        Invoke("LoadNewGameScene", 0.5f);
    }

    public void LoadNewGameScene()
    {
        SceneManager.LoadScene(1);
	Cursor.visible = false;
    }

    public void ExitGame()
    {
        OnClickButton();
        Time.timeScale = 1; // Despausa o jogo
        Invoke("ExitScene", 0.5f);
    }

    public void ExitScene()
    {
        Application.Quit();
    }


    public void OnClickButton()
    {
        audioSource.PlayOneShot(buttonAudio);
    }
}
