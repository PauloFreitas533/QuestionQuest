using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void StartJogo(string scene)
    {
        SceneManager.LoadScene(scene);
	  Debug.Log("Olá!");
    }

    public void SairJogo()
    {
        Application.Quit();
    }
}
