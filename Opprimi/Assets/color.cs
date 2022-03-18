using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color : MonoBehaviour
{
    GameObject cubeManager;

    public bool canKill;

    [SerializeField] ParticleSystem Boom;
    
    void Start()
    {
        gameObject.transform.GetComponent<SpriteRenderer>().color = Color.red;

        cubeManager = GameObject.Find("Cube Manager").gameObject;

        canKill = true;
    }

    void FixedUpdate()
    {
        if (gameObject.transform.position.y == -5)
        {
            Vector2 cubePos = new Vector2(gameObject.transform.position.x, -5);
            Instantiate(Boom, cubePos, Quaternion.identity);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            gameObject.transform.GetComponent<SpriteRenderer>().color = Color.white;

            canKill = false;

            gameObject.tag = "ground";
        
            if (cubeManager.GetComponent<CubeSpawner>().cubeCount < 80)
            {
                cubeManager.GetComponent<CubeSpawner>().ChooseCubeSpawn();
            }
        }
    }
}
