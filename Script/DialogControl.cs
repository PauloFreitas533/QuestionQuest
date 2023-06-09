using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogControl : MonoBehaviour
{
    private HeroKnight hero;
    private Question1 question1;
    private Question2 question2;
    private Question3 question3;
    private Question4 question4;
    private Question5 question5;
    private Question6 question6;
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

    public void SetHero(HeroKnight heroInstance)
    {
        hero = heroInstance;
    }
    
    void Awake()
    {
        hero = FindObjectOfType<HeroKnight>();
        question1 = FindObjectOfType<Question1>();
        question2 = FindObjectOfType<Question2>();
        question3 = FindObjectOfType<Question3>();
        question4 = FindObjectOfType<Question4>();
        question5 = FindObjectOfType<Question5>();
        question6 = FindObjectOfType<Question6>();
    }

    public void Speak(Sprite p, string txt, string actorName, string answer1, 
        string answer2, string answer3, string answer4, int correctIntAnswer, string dialogOption)
    {
        if (dialogOption == "question1" && question1 != null)
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
        ShowDialog(dialogOption);
        }
        else if (dialogOption == "question2" && question2 != null)
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
            ShowDialog(dialogOption);
        }
	else if (dialogOption == "question3" && question3 != null)
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
            ShowDialog(dialogOption);
        }
	else if (dialogOption == "question4" && question4 != null)
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
            ShowDialog(dialogOption);
        }
	else if (dialogOption == "question5" && question5 != null)
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
            ShowDialog(dialogOption);
        }
	else if (dialogOption == "question6" && question6 != null)
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
            ShowDialog(dialogOption);
        }
    }

    public void Escape()
    {
        dialogObj.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowDialog(string dialogOption)
    {
        index[0].onClick.RemoveAllListeners();
        index[1].onClick.RemoveAllListeners();
        index[2].onClick.RemoveAllListeners();
        index[3].onClick.RemoveAllListeners();

        index[0].onClick.AddListener(() => AnswerButton(0, dialogOption));
        index[1].onClick.AddListener(() => AnswerButton(1, dialogOption));
        index[2].onClick.AddListener(() => AnswerButton(2, dialogOption));
        index[3].onClick.AddListener(() => AnswerButton(3, dialogOption));
    }

    public void AnswerButton(int buttonIndex, string dialogOption)
    {
        if (buttonIndex == correctAnswer)
        {
            if (dialogOption == "question1")
            {
                question1.gameObject.SetActive(false);
            }
            else if (dialogOption == "question2")
            {
                question2.gameObject.SetActive(false);
            }
            else if (dialogOption == "question3")
            {
                question3.gameObject.SetActive(false);
            }
            else if (dialogOption == "question4")
            {
                question4.gameObject.SetActive(false);
            }
            else if (dialogOption == "question5")
            {
                question5.gameObject.SetActive(false);
            }
            else if (dialogOption == "question6")
            {
                question6.gameObject.SetActive(false);
            }
        }
        else
        {
            hero.Damage();
        }
        dialogObj.SetActive(false);
        Time.timeScale = 1f;
    }
}