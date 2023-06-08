using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrap : MonoBehaviour
{
    private bool isColliding = false;
    private float eixoZ;
    private Vector2 newPosition;
    public GameObject pointUp;
    public GameObject pointDown;
    public float speed;

    void Start()
    {
        newPosition = pointDown.transform.position;
    }

    void Update()
    {
        SawSpin();
        SawMove();
    }

    private void SawSpin()
    {
        eixoZ = eixoZ + Time.deltaTime * 1000;
        transform.rotation = Quaternion.Euler(0, 0, eixoZ);
    }

    private void SawMove()
    {
        if (Vector2.Distance(transform.position, pointDown.transform.position) < 0.1f)
        {
            newPosition = pointUp.transform.position;
        }

        if (Vector2.Distance(transform.position, pointUp.transform.position) < 0.1f)
        {
            newPosition = pointDown.transform.position;
        }

        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
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
