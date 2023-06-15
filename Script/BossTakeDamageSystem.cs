using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BossTakeDamageSystem : MonoBehaviour
{
    [SerializeField]
    private int life;

    [SerializeField]
    private EnemyLifeBar enemyLifeBar;

    private bool isColliding = false;
    private Animator deathSpriteAnimator;
    public AudioSource enemyDeathSound;
    public GameObject evilWizard;

    private void Start()
    {
        this.enemyLifeBar.MaxLife = this.life--;
        this.enemyLifeBar.Life = this.life--;
	deathSpriteAnimator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (this.life > 0)
        {
            this.life -= damage;
        }

        this.enemyLifeBar.Life = this.life--;

        if (this.life <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //deathSpriteAnimator.SetTrigger("Die"); // Trigger the death animation
	//enemyDeathSound.Play();
	Invoke("LoadCreditsScene", 2f);
        evilWizard.SetActive(false);
    }

    private IEnumerator ReactivateCollider(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<Collider2D>().enabled = true;
        isColliding = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isColliding)
        {
            HeroKnight heroKnight = collision.GetComponent<HeroKnight>();
            if (heroKnight != null)
            {
		isColliding = true;
		StartCoroutine(ReactivateCollider(1f));
                heroKnight.Damage();
            }
        }
    }

    public void LoadCreditsScene()
    {
        SceneManager.LoadScene(3);
    }
}
