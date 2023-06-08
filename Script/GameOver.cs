using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip buttonAudio;

    public void StartGame(string scene)
    {
        OnClickButton();
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        OnClickButton();
        //Application.Quit(); trocar para essa chamada antes de buildar o jogo
        Time.timeScale = 1; // Despausa o jogo
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
        Process.GetCurrentProcess().Kill(); // Encerra o processo do jogo
        #endif
    }

    public void OnClickButton()
    {
        audioSource.PlayOneShot(buttonAudio);
    }
}
