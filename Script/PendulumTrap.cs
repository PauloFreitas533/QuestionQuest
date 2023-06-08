using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumTrap : MonoBehaviour
{
    public Rigidbody2D pendulumRB;
    public float rangerRight;
    public float rangerLeft;
    public float limitSpeed;

    void Start()
    {
        pendulumRB.angularVelocity = limitSpeed;
    }

    void Update()
    {
        Push();
    }

    public void Push()
    {
        if (transform.rotation.z > 0 && transform.rotation.z < rangerRight && (pendulumRB.angularVelocity > 0) 
            && pendulumRB.angularVelocity < limitSpeed)
        {
            pendulumRB.angularVelocity = limitSpeed;
        }
        else if (transform.rotation.z < 0 && transform.rotation.z > rangerRight && (pendulumRB.angularVelocity < 0)
            && pendulumRB.angularVelocity > limitSpeed * -1)
        {
            pendulumRB.angularVelocity = limitSpeed;
        }
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
