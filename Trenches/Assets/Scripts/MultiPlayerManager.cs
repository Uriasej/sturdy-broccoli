using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public class MultiPlayerManager : MonoBehaviour
{
    private int playerCount;

    private Color player1;

    public Color Team1;

    public Color Team2;

    public int teamId1Count;

    public int teamId2Count;

    private Color player2;

    private Color player3;

    private Color player4;

    private void Start()
    {
        // Player Counts
        playerCount = 0;
        teamId1Count = 0;
        teamId2Count = 0;

        //Setting up the player recognition
        player1 = Color.blue;

        player2 = Color.red;

        player3 = Color.yellow;

        player4 = Color.green;

        //Setting up team recognition

        Team1 = new Color(0.04705882f, 0.3490196f, 0.1764706f, 1f);
        Team2 = new Color(0.2850214f, 0.3301887f, 0.3176388f, 1f);
    }

    public void OnPlayerJoin(PlayerInput context)
    {
        playerCount = playerCount + 1;

        var playerJoined = context.gameObject;

        var localPlayer = context.GetComponent<PlayerController>();

        var playerTeamColor = playerJoined.transform.GetComponent<SpriteRenderer>();

        var playerRender = playerJoined.transform.Find("Helmet").GetComponent<SpriteRenderer>();

        var device = context.devices[0];

        if (playerCount == 1)
        {
            playerRender.color = player1;
            teamId1Count = teamId1Count + 1;
            playerTeamColor.color = Team1;
        }

        else if (playerCount == 2)
        {
            playerRender.color = player2;
            playerTeamColor.color = Team2;
            teamId2Count = teamId2Count + 1;
            playerJoined.transform.Rotate (0f, 180f, 0f);
        }

        else if (playerCount == 3)
        {
            playerRender.color = player3;
            teamId1Count = teamId1Count + 1;
            playerTeamColor.color = Team1;
        }

        else if (playerCount == 4)
        {
            playerRender.color = player4;
            teamId2Count = teamId2Count + 1;
            playerTeamColor.color = Team2;
        }

        if (device.GetType().ToString() == "UnityEngine.InputSystem.DualShock.DualShock4GamepadHID")
        {
            DualShockGamepad ds4 = (DualShockGamepad)device;
            ds4.SetLightBarColor(playerRender.color);
        }
    }
}
