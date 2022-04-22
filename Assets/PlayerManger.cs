using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManger : MonoBehaviour
{
    public bool[] playerTurns = new bool[2];//if they have taken their turn
    public PlayerMovement[] player1;
    public PlayerMovement[] player2;
    public PlayerMovement p1Selected;
    public PlayerMovement.Direction p1Direction;
    public PlayerMovement p2Selected;
    public PlayerMovement.Direction p2Direction;
    public SetupGame game;

    public void selectedPlayer(PlayerMovement playerMovement)
    {
        if(playerMovement.playerNumber == 1)
        {
            DeselectP1();

            p1Selected = playerMovement;

        }
        else
        {
            DeselectP2();
            p2Selected = playerMovement;
        }
    }

    private void DeselectP2()
    {
        for (int i = 0; i < 3; i++)
        {
            player2[i].Deselect();
        }
    }

    private void DeselectP1()
    {
        for (int i = 0; i < 3; i++)
        {
            player1[i].Deselect();
        }
    }

    public void movePlayers()
    {
        if (playerTurns[0] && playerTurns[1])
        {
            p1Selected.makeMove((int)p1Direction);
            p2Selected.makeMove((int)p2Direction);
            playerTurns[0] = false;
            playerTurns[1] = false;

            DeselectP1();
            DeselectP2();
            game.setTileSoft(new Vector2Int(p1Selected.position.x, p1Selected.position.y), 1);
            game.setTileSoft(new Vector2Int(p2Selected.position.x, p2Selected.position.y), 2);
            p1Selected = null;
            p2Selected = null;
        }
        

    }

}
