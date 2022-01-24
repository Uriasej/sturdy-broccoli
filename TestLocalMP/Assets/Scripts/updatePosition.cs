using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updatePosition : MonoBehaviour
{
    private GameObject lava;

    private GameObject ball;

    private Vector3 ballStartPos;

    private bool displayPos;

    private float ballY;

    private float lavaY;

    private Vector2 lavaPos;
    private float distanceFromLava;

    private float distanceFromLavaY;

    void Start()
    {
        ball = GameObject.Find("Circle").gameObject;

        lava = GameObject.Find("lava").gameObject;

        lavaPos = lava.transform.position;

        ballStartPos = ball.transform.position;

        lavaY = lavaPos.y;

        displayPos = true;
    }

    void Update()
    {
        ballY = ball.transform.position.y;
        distanceFromLavaY = ballY - lavaY;

        if (displayPos == true)
        {
            gameObject.GetComponent<UnityEngine.UI.Text>().text = "Ball distance from lava: " + distanceFromLavaY;
        }
    }

    void FixedUpdate()
    {
        if (distanceFromLavaY < 0)
        {
            displayPos = false;
            gameObject.GetComponent<UnityEngine.UI.Text>().text = "Resetting Ball";
            new WaitForSeconds(1);
            gameObject.GetComponent<UnityEngine.UI.Text>().text = "Resetting Ball.";
            new WaitForSeconds(1);
            gameObject.GetComponent<UnityEngine.UI.Text>().text = "Resetting Ball..";
            new WaitForSeconds(1);
            gameObject.GetComponent<UnityEngine.UI.Text>().text = "Resetting Ball...";

            new WaitForSeconds(1);

            ball.transform.position = ballStartPos;
            displayPos = true;
        }
    }
}