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
    }

    public void ExitGame()
    {
        OnClickButton();
        //Application.Quit(); trocar para essa chamada antes de buildar o jogo
        Time.timeScale = 1; // Despausa o jogo
        Invoke("ExitScene", 0.5f);
    }

    public void ExitScene()
    {
        //Application.Quit(); trocar para essa chamada antes de buildar o jogo
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
