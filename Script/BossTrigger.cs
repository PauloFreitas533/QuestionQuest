using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject evilWizard;
    public AudioClip newAudioClip; // O novo áudio que será reproduzido

    private AudioSource mainCameraAudioSource;

    void Start()
    {
        mainCameraAudioSource = Camera.main.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (evilWizard != null)
            {
                evilWizard.SetActive(true); // Habilitar o objeto Evil Wizard
                if (mainCameraAudioSource != null && newAudioClip != null)
                {
                    mainCameraAudioSource.clip = newAudioClip; // Substituir o áudio atual pelo novo áudio
                    mainCameraAudioSource.Play(); // Reproduzir o novo áudio
                }
            }
        }
    }
}
