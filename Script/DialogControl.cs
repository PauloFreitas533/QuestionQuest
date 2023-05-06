using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogControl : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogObj;
    public Image profile;
    public Text speakText;
    public Text actorNameText;

    [Header("Settings")]
    public float typingSpeed;

    public void Speak(Sprite p, string txt, string actorName)
    {
        dialogObj.SetActive(true);
        profile.sprite = p;
        speakText.text = txt;
        actorNameText.text = actorName;
    }
}
