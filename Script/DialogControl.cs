using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogControl : MonoBehaviour
{
    private HeroKnight hero;
    public DialogGhost dialogGhost;
    public Button[] index = new Button[4];

    [Header("Components")]
    public GameObject dialogObj;
    public Image profile;
    public Text speakText;
    public Text actorNameText;
    public Text answerText1;
    public Text answerText2;
    public Text answerText3;
    public Text answerText4;
    public int correctAnswer;

    [Header("Settings")]
    public float typingSpeed;
    //private string sentences;
    //private int index;

    public void SetHero(HeroKnight heroInstance)
    {
        hero = heroInstance;
    }
    
    void Awake()
    {
        hero = FindObjectOfType<HeroKnight>();
        dialogGhost = FindObjectOfType<DialogGhost>();
    }

    public void Speak(Sprite p, string txt, string actorName, string answer1, 
        string answer2, string answer3, string answer4, int correctIntAnswer)
    {
        dialogObj.SetActive(true);
        profile.sprite = p;
        speakText.text = txt;
        actorNameText.text = actorName;
        answerText1.text = answer1;
        answerText2.text = answer2;
        answerText3.text = answer3;
        answerText4.text = answer4;
        correctAnswer = correctIntAnswer;
        ShowDialog();
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

    public void ShowDialog()
    {
        index[0].onClick.RemoveAllListeners();
        index[1].onClick.RemoveAllListeners();
        index[2].onClick.RemoveAllListeners();
        index[3].onClick.RemoveAllListeners();

        index[0].onClick.AddListener(() => AnswerButton(0));
        index[1].onClick.AddListener(() => AnswerButton(1));
        index[2].onClick.AddListener(() => AnswerButton(2));
        index[3].onClick.AddListener(() => AnswerButton(3));
    }

    public void AnswerButton(int buttonIndex)
    {
        if (buttonIndex == correctAnswer)
        {
            dialogGhost.gameObject.SetActive(false);
        }
        else
        {
            hero.Damage();
        }
        dialogObj.SetActive(false);
        Time.timeScale = 1f;
    }
}
