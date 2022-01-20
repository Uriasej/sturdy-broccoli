using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{

    public Vector3 playerPos;

    public Vector3 lavaLevel;

    public AudioSource splash;

    public AudioSource death;

    void Start()
    {
        splash = gameObject.GetComponent<AudioSource>();
        death = GameObject.Find("Death").GetComponent<AudioSource>();
    }
    void Update()
    {
        var player = GameObject.Find("Player");
        playerPos = player.transform.position;
        lavaLevel = GameObject.Find("lava").transform.position;
    }

    public void DiedByLava()
    {
        gameObject.transform.position = new Vector3(playerPos.x, lavaLevel.y, playerPos.z);

        splash.Play();
    }

    public void Died()
    {
        GameObject.Find("Death").transform.position = new Vector3(playerPos.x, playerPos.y, playerPos.z);

        GameObject.Find("Death").GetComponent<AudioSource>().Play();

        death.Play();

        GameObject.Find("Death").GetComponent<ParticleSystem>().Play();
    }
}
