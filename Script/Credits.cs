using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{

    public Button[] buttons;
    public float delay;

    public AudioSource audioSource;
    public AudioClip buttonAudio;

    private float delayBeforeExit = 1f;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }

        Invoke("ActiveButtons", delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ActiveButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
    }

    public void ExitGame()
    {
        StartCoroutine(ExitWithDelay());
    }

    public void RestartGame(string scene)
    {
        OnClickButton();
        SceneManager.LoadScene(scene);
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
