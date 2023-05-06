using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogScript : MonoBehaviour
{
    public string[] dialogGhost;
    public int dialogIndex;

    public GameObject dialogPanel;
    //public Text dialogText;

    public bool readyToSpeak = false;
    public bool startDialog = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!startDialog)
        {
            //Time.timeScale = 0f;
            StartDialog();
        }
    }

    void StartDialog()
    {
        startDialog = true;
        dialogIndex = 0;    
        dialogPanel.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            readyToSpeak = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            readyToSpeak = false;
        }
    }
}
