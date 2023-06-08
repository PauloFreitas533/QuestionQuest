using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            HeroKnight heroKnight = col.GetComponent<HeroKnight>();
            if (heroKnight != null)
            {
                heroKnight.heartCollected = true;
                Destroy(gameObject);
                if (heroKnight.life < heroKnight.lifeMax)
                {
                    heroKnight.life++;
                    switch (heroKnight.life)
                    {
                        case 2:
                            heroKnight.lifeOn5.enabled = true;
                            heroKnight.lifeOff5.enabled = false;
                            heroKnight.lifeOn4.enabled = true;
                            heroKnight.lifeOff4.enabled = false;
                            heroKnight.lifeOn3.enabled = true;
                            heroKnight.lifeOff3.enabled = false;
                            heroKnight.lifeOn2.enabled = false;
                            heroKnight.lifeOff2.enabled = true;
                            heroKnight.lifeOn1.enabled = false;
                            heroKnight.lifeOff1.enabled = true;
                            break;
                        case 3:
                            heroKnight.lifeOn5.enabled = true;
                            heroKnight.lifeOff5.enabled = false;
                            heroKnight.lifeOn4.enabled = true;
                            heroKnight.lifeOff4.enabled = false;
                            heroKnight.lifeOn3.enabled = false;
                            heroKnight.lifeOff3.enabled = true;
                            heroKnight.lifeOn2.enabled = false;
                            heroKnight.lifeOff2.enabled = true;
                            heroKnight.lifeOn1.enabled = false;
                            heroKnight.lifeOff1.enabled = true;
                            break;
                        case 4:
                            heroKnight.lifeOn5.enabled = true;
                            heroKnight.lifeOff5.enabled = false;
                            heroKnight.lifeOn4.enabled = false;
                            heroKnight.lifeOff4.enabled = true;
                            heroKnight.lifeOn3.enabled = false;
                            heroKnight.lifeOff3.enabled = true;
                            heroKnight.lifeOn2.enabled = false;
                            heroKnight.lifeOff2.enabled = true;
                            heroKnight.lifeOn1.enabled = false;
                            heroKnight.lifeOff1.enabled = true;
                            break;
                        case 5:
                            heroKnight.lifeOn5.enabled = false;
                            heroKnight.lifeOff5.enabled = true;
                            heroKnight.lifeOn4.enabled = false;
                            heroKnight.lifeOff4.enabled = true;
                            heroKnight.lifeOn3.enabled = false;
                            heroKnight.lifeOff3.enabled = true;
                            heroKnight.lifeOn2.enabled = false;
                            heroKnight.lifeOff2.enabled = true;
                            heroKnight.lifeOn1.enabled = false;
                            heroKnight.lifeOff1.enabled = true;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
