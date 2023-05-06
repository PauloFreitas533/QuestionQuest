using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogGhost : MonoBehaviour
{
    public Sprite profile;
    public string speakTxt;
    public string actorName;

    public LayerMask playerLayer;
    public float radious;

    private DialogControl dc;
    public bool isDialogActive = false;

    private void Start()
    {
        dc = FindObjectOfType<DialogControl>();
    }

    private void FixedUpdate()
    {
         Interact();
    }

    public void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radious, playerLayer);
        
        if(hit != null)
        {
            isDialogActive = true;
            if (isDialogActive)
            {
            dc.Speak(profile, speakTxt, actorName);
                Time.timeScale = 0f;
            }
            else
            {
                isDialogActive = false;
                Time.timeScale = 1f;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radious);
    }
}
