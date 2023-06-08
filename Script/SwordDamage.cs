using UnityEngine;
using Cinemachine;
using System.Collections;

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

    public CinemachineVirtualCamera virtualCamera;

    public float leftScreenX = 0.55f;
    public float rightScreenX = 0.45f;

    private bool isAPressed = false;
    private bool isDPressed = false;
    private float pressStartTime = 0f;
    private float requiredPressTime = 0.6f;

    public int damage = 10;

    private bool canAttack = true;
    private bool isScreenXInterpolating = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            isAPressed = true;
            pressStartTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            isDPressed = true;
            pressStartTime = Time.time;
        }

        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Attack();
            canAttack = false;
            Invoke("ResetAttack", 0.23f); // Calls the ResetAttack method after 0.23 seconds
        }

        if (isAPressed && Time.time - pressStartTime >= requiredPressTime)
        {
            if (this.heroKnight.movementDirection == MovementDirection.Left)
            {
                float duration = 0.6f; // Duration of the interpolation in seconds
                StartCoroutine(UpdateScreenX(leftScreenX, duration));
            }
        }

        if (isDPressed && Time.time - pressStartTime >= requiredPressTime)
        {
            if (this.heroKnight.movementDirection == MovementDirection.Right)
            {
                float duration = 0.6f; // Duration of the interpolation in seconds
                StartCoroutine(UpdateScreenX(rightScreenX, duration));
            }
        }

        if (!isAPressed && !isDPressed && !isScreenXInterpolating)
        {
            float duration = 1f; // Duration of the interpolation in seconds
            StartCoroutine(UpdateScreenX(0.5f, duration));
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            isAPressed = false;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            isDPressed = false;
        }
    }

    private IEnumerator UpdateScreenX(float targetScreenX, float duration)
    {
        if (isScreenXInterpolating)
            yield break;

        CinemachineFramingTransposer transposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        float startScreenX = transposer.m_ScreenX;
        float elapsed = 0f;
        isScreenXInterpolating = true;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsed / duration);
            float interpolatedScreenX = Mathf.Lerp(startScreenX, targetScreenX, normalizedTime);
            transposer.m_ScreenX = interpolatedScreenX;
            yield return null;
        }

        isScreenXInterpolating = false;
    }

    private void Attack()
    {
        Transform attackPoint;
        CircleCollider2D attackCollider;
        if (this.heroKnight.movementDirection == MovementDirection.Right)
        {
            attackPoint = this.rightAttackPoint;
            attackCollider = this.rightAttackCollider;
        }
        else
        {
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
