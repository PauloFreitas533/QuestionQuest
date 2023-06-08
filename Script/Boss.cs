using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Transform target;
    private Animator animator;
    public float minYPosition = -24f;
    private bool isExhausted = false;
    private bool isDistant = true;
    private int tentativas;
    public Collider2D leftCollider;
    public Collider2D rightCollider;
    private bool isColliding = false;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (target != null && isExhausted == false)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();
            transform.position += direction * moveSpeed * Time.deltaTime;
       	    float distance = Vector2.Distance(transform.position, target.position); // Verifica a distância entre o Boss e o jogador

            if (target.position.x < transform.position.x)
            {
                float currentScaleX = Mathf.Abs(transform.localScale.x);
                transform.localScale = new Vector3(-currentScaleX, transform.localScale.y, transform.localScale.z);
		if (distance < 4f && isDistant)
                {
                    OnTriggerEnter2D(leftCollider);
                } 
            }
            else
            {
                float currentScaleX = Mathf.Abs(transform.localScale.x);
                transform.localScale = new Vector3(currentScaleX, transform.localScale.y, transform.localScale.z);
		if (distance < 4f && isDistant)
                {
                    OnTriggerEnter2D(rightCollider);
                }
            }

            // Limitar posição Y
            if (transform.position.y < minYPosition)
            {
                transform.position = new Vector3(transform.position.x, minYPosition, transform.position.z);
            }
        }
    }

    // Função chamada pela animação para reiniciar o estado de ataque
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isColliding)
        {
            HeroKnight heroKnight = collision.GetComponent<HeroKnight>();
            if (heroKnight != null)
            {
		animator.SetTrigger("Attack");
		tentativas = tentativas + 1;
		isDistant = false;
		isColliding = true;
		StartCoroutine(ResetDistance());
            }
        }
    }

    // Função chamada pela animação para reiniciar o estado de ataque
    public void EndAttack()
    {
	  animator.SetTrigger("EndAttack");
	  if(tentativas == 3)
	  {
	     StartCoroutine(ResetExhaustedState());
          }
    }

    private IEnumerator ResetExhaustedState()
    {
	isExhausted = true;
        yield return new WaitForSeconds(3f);
        isExhausted = false;
        tentativas = 0;
    }

    private IEnumerator ResetDistance()
    {
        yield return new WaitForSeconds(5f);
        isDistant = true;
	GetComponent<Collider2D>().enabled = true;
        isColliding = false;
    }
}


