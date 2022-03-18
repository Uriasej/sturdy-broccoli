using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    private Rigidbody2D rb2D;

    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "lava")
        {
            gameObject.transform.position = new Vector3(0, 0, 0);
        }
        else if (collision.tag == "nextlevel")
        {
            var NextLevelLogic = GameObject.Find("Square").GetComponent<NextLevelLogic>();
            
            NextLevelLogic.OnNextLevel();
        }

        else if (collision.tag == "deathBall")
        {
            rb2D.AddForce(new Vector3(0f, 30f, 0f), ForceMode2D.Impulse);

            GameObject deathBall = GameObject.Find("BallBouncer");

            ParticleSystem ballBounce = deathBall.transform.Find("DeathBall").gameObject.GetComponent<ParticleSystem>();

            ballBounce.Play();
        }

        else if (collision.tag == "deathBoth")
        {
            rb2D.AddForce(new Vector3(0f, 30f, 0f), ForceMode2D.Impulse);

            GameObject deathBoth = GameObject.Find("BounceBoth");

            ParticleSystem DeathBoth = deathBoth.transform.Find("DeathBoth").gameObject.GetComponent<ParticleSystem>();

            DeathBoth.Play();
        }

    }
}
