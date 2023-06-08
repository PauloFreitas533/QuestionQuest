using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip buttonAudio;

    private float delayBeforeExit = 1f;

    public void StartGame(string scene)
    {
        OnClickButton();
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        StartCoroutine(ExitWithDelay());
    }

    private IEnumerator ExitWithDelay()
    {
        OnClickButton();
        yield return new WaitForSeconds(delayBeforeExit);

        // Application.Quit();
        // Restante das linhas do código após o atraso
        Time.timeScale = 1; // Despausa o jogo

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Process.GetCurrentProcess().Kill();
        #endif
    }

    public void OnClickButton()
    {
        audioSource.PlayOneShot(buttonAudio);
    }
}
