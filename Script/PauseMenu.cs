using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class PauseMenu : MonoBehaviour
{

    public Transform pauseMenu;

    public AudioSource audioSource;
    public AudioClip buttonAudio;

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
		Cursor.visible = false;
            }
            else
            {
                pauseMenu.gameObject.SetActive(true);
                Time.timeScale = 0f;
	        Cursor.visible = true;
            }
        }
    }

    public void ResumeGame()
    {
        OnClickButton();
	Cursor.visible = false;
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        Destroy(FindObjectOfType<Question1>());
        Destroy(FindObjectOfType<Question2>());
        Destroy(FindObjectOfType<Question3>());
        Destroy(FindObjectOfType<Question4>());
        Destroy(FindObjectOfType<Question5>());
        OnClickButton();
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
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
