using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public class PlayerJoinLogic : MonoBehaviour
{
    private int playerCount;

    private Color player1;

    private Color player2;

    private Color player3;

    private Color player4;

    private void Start()
    {
        playerCount = 0;

        player1 = Color.blue;

        player2 = Color.red;

        player3 = Color.yellow;

        player4 = Color.green;
    }

    public void OnPlayerJoin(PlayerInput context)
    {
        playerCount = playerCount + 1;

        var playerJoined = context.gameObject;

        var Player = playerJoined.transform.Find("Player");

        var playerRender = playerJoined.GetComponent<SpriteRenderer>();

        var device = context.devices[0];
 


        if (playerCount == 1)
        {
            playerRender.color = player1;
        }

        else if (playerCount == 2)
        {
            playerRender.color = player2;
        }

        else if (playerCount == 3)
        {
            playerRender.color = player3;
        }

        else if (playerCount == 4)
        {
            playerRender.color = player4;
        }

        if (device.GetType().ToString() == "UnityEngine.InputSystem.DualShock.DualShock4GamepadHID")
        {
            DualShockGamepad ds4 = (DualShockGamepad)device;
            ds4.SetLightBarColor(playerRender.color);
        }
    }
}
