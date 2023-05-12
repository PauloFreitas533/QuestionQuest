using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogGhost : MonoBehaviour
{
    public Sprite profile;
    public string speakTxt;
    public string actorName;
    public string answerText1;
    public string answerText2;
    public string answerText3;
    public string answerText4;
    public int correctAnswer;

    public LayerMask playerLayer;
    public float radious;

    private DialogControl dc;
    private bool onRadious = true;
    private float originalRadious;
    private bool isCoroutineRunning = false;

    private void Start()
    {
        originalRadious = radious;
        dc = FindObjectOfType<DialogControl>();
    }

    private void FixedUpdate()
    {
         Interact();
    }

    private void Update()
    {
        if (onRadious)
        {
            dc.Speak(profile, speakTxt, actorName, answerText1, answerText2, answerText3, answerText4, correctAnswer);
            onRadious = false;
        }
    }

    public void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radious, playerLayer);

        if (hit != null)
        {
            onRadious = true;
            radious = 0;
            Time.timeScale = 0f;

            if (!isCoroutineRunning)
            {
                StartCoroutine(RestoreRadious());
            }
        }
        else
        {
            onRadious = false;
            Time.timeScale = 1f;
        }
    }

    private IEnumerator RestoreRadious()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(2f);

        radious = originalRadious;
        isCoroutineRunning = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radious);
    }
}
