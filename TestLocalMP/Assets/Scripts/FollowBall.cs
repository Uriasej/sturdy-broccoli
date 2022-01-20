using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBall : MonoBehaviour
{
    public GameObject ball;
    public Vector2 ballPos;

    void Start()
    {
        ball = GameObject.Find("Circle");
    }

    void Update()
    {
        ballPos = ball.transform.position;

        gameObject.transform.position = new Vector3(ballPos.x, ballPos.y, -100);
    }
}
