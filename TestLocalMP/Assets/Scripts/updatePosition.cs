using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updatePosition : MonoBehaviour
{
    private GameObject lava;

    private GameObject ball;

    private float lavaY;

    private Vector2 lavaPos;
    private float distanceFromLava;

    private float distanceFromLavaY;

    void Start()
    {
        ball = gameObject;

        lava = GameObject.Find("lava").gameObject;

        lavaPos = lava.transform.position;

        lavaY = lavaPos.y;
    }

    void Update()
    {
        distanceFromLava = Vector2.Distance(lavaPos, ball.transform.position);
    }
}