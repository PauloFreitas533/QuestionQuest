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
    public Text answerText1;
    public Text answerText2;
    public Text answerText3;
    public Text answerText4;
    //public Button Btn-Escape;

    [Header("Settings")]
    public float typingSpeed;
    //private string sentences;
    //private int index;

    public void Speak(Sprite p, string txt, string actorName, string answer1, string answer2, string answer3, string answer4)
    {
        dialogObj.SetActive(true);
        profile.sprite = p;
        speakText.text = txt;
        actorNameText.text = actorName;
        answerText1.text = answer1;
        answerText2.text = answer2;
        answerText3.text = answer3;
        answerText4.text = answer4;
        //StartCoroutine(TypeSentence());
    }

    //IEnumerator TypeSentence()
    //{
    //    foreach (char letter in sentences[index].ToCharArray())
    //    {
    //        speakText.text += letter;
    //        yield return new WaitForSeconds(typingSpeed);
    //    }
    //}

    //public void NextSentence()
    //{
    //    if(speakText.text == sentences[index])
    //    {
    //        if(index < sentences.Length - 1)
    //        {
    //            index++;
    //            speakText.text = "";
    //            StartCoroutine(TypeSentence());
    //        }
    //        else
    //        {
    //            speakText.text = "";
    //            index = 0;
    //            dialogObj.SetActive(false);
    //        }
    //    }
    //}

    public void Escape()
    {
        dialogObj.SetActive(false);
        Time.timeScale = 1f;
    }
}
