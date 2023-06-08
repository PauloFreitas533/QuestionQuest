using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamageSystem : MonoBehaviour
{
    [SerializeField]
    private int life;

    [SerializeField]
    private EnemyLifeBar enemyLifeBar;

    private bool isColliding = false;

    private void Start()
    {
        this.enemyLifeBar.MaxLife = this.life--;
        this.enemyLifeBar.Life = this.life--;
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
        // Lógica para a morte do inimigo, como destruir o objeto ou reproduzir uma animação
        Destroy(gameObject);
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
}
