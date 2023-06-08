using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : MonoBehaviour
{
    private bool heartDropped = false;
    private Animator chestAnimator;
    [SerializeField] private bool checkPlayer;
    [SerializeField] private Transform heart;

    void Start()
    {
        chestAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (checkPlayer && !heartDropped)
        {
            chestAnimator.enabled = true;

            Invoke("DropHeart", 0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<HeroKnight>().keyHas)
        {
            checkPlayer = true;
		//Debug.Log("Pegou!");
            
            other.gameObject.GetComponent<HeroKnight>().key = 
                other.gameObject.GetComponent<HeroKnight>().key - 1;
            
            other.gameObject.GetComponent<HeroKnight>().keyHas = false;
        }

        if (other.gameObject.CompareTag("heart"))
        {
            Destroy(other.gameObject);
            heartDropped = true;
        }
    }

    void DropHeart()
    {
        if (!heartDropped)
        {
            Instantiate(heart, transform.position, transform.rotation);
            heartDropped = true;
        }
    }
}
