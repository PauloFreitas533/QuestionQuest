using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    [SerializeField]
    private Transform rightAttackPoint;

    [SerializeField]
    private Transform leftAttackPoint;

    [SerializeField]
    private CircleCollider2D rightAttackCollider;

    [SerializeField]
    private CircleCollider2D leftAttackCollider;

    [SerializeField]
    private HeroKnight heroKnight;

    public int damage = 10;

    private bool canAttack = true;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
    	  {
            Attack();
            canAttack = false;
        	Invoke("ResetAttack", 0.23f); // Chama o método ResetAttack após 1 segundo
    	  }
    }

    private void Attack()
    {
        Transform attackPoint;
	  CircleCollider2D attackCollider;
	  if (this.heroKnight.movementDirection == MovementDirection.Right){
	      attackPoint = this.rightAttackPoint;
            attackCollider = this.rightAttackCollider;
	  } else{
	      attackPoint = this.leftAttackPoint;
            attackCollider = this.leftAttackCollider;
        }

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackCollider.radius);
        foreach (Collider2D enemyCollider in hitColliders)
        {
            EnemyTakeDamageSystem enemy = enemyCollider.GetComponent<EnemyTakeDamageSystem>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

   private void ResetAttack()
   {
       canAttack = true;
   }

}
