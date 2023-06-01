using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamageSystem : MonoBehaviour
{
    [SerializeField]
    private int life;

    [SerializeField]
    private EnemyLifeBar enemyLifeBar;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HeroKnight heroKnight = collision.GetComponent<HeroKnight>();
            if (heroKnight != null)
            {
                heroKnight.Damage();
            }
        }
    }
}
