using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PlayerManger : MonoBehaviour
{
    public bool[] playerTurns = new bool[2];//if they have taken their turn
    public PlayerMovement[] player1;
    public PlayerMovement[] player2;
    public PlayerMovement p1Selected;
    public PlayerMovement.Direction p1Direction;
    public PlayerMovement p2Selected;
    public PlayerMovement.Direction p2Direction;
    public Game game;
    public bool[] tileDefended = new bool[2];
    public int[] Score = new int[2];


    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            player1[0].SelectingPlayer();
            return;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            player1[1].SelectingPlayer();
            return;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            player1[2].SelectingPlayer();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            player2[0].SelectingPlayer();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            player2[1].SelectingPlayer();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            player2[2].SelectingPlayer();
            return;
        }
    }

    public void selectedPlayer(PlayerMovement playerMovement)
    {
        if (playerMovement.playerNumber == 1)
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
        tileDefended[1] = false;
    }

    private void DeselectP1()
    {
        for (int i = 0; i < 3; i++)
        {
            player1[i].Deselect();
        }
        tileDefended[0] = false;
    }

    public void movePlayers()
    {
        if (!playerTurns[0] || !playerTurns[1])
            return;


        playerTurns[0] = false;
        playerTurns[1] = false;


        p1Selected.makeMove((int)p1Direction);
        p2Selected.makeMove((int)p2Direction);
        if (p1Selected.position == p2Selected.position) //only 1 of the 2 checks need to happen as player pieces both moved onto the same place
        {
            if (game.gameBoard[p1Selected.position.x, p1Selected.position.y].thisTileType == Tile.TileType.player1Strong)
            {
                game.setTileOwned(new Vector2Int(p1Selected.position.x, p1Selected.position.y), 2, CheckIfDefended(p1Direction, player2, p1Selected));
            }
            else if (game.gameBoard[p1Selected.position.x, p1Selected.position.y].thisTileType == Tile.TileType.player2Strong)
            {
                game.setTileOwned(new Vector2Int(p1Selected.position.x, p1Selected.position.y), 1, CheckIfDefended(p1Direction, player2, p1Selected));
            }
            else// if its a soft tile only 1 of the 2 checks need to happen as player pieces both moved onto the same place
            {
                game.setTileOwned(new Vector2Int(p1Selected.position.x, p1Selected.position.y), 1, CheckIfDefended(p1Direction, player2, p1Selected));
            }
        }
        else//both checks need to happens as both pieces moved to different places
        {
            game.setTileOwned(new Vector2Int(p1Selected.position.x, p1Selected.position.y), 1, CheckIfDefended(p1Direction, player2, p1Selected));
            game.setTileOwned(new Vector2Int(p2Selected.position.x, p2Selected.position.y), 2, CheckIfDefended(p2Direction, player1, p2Selected));
        }


        game.intRounds = game.intRounds + 1;

        if (game.intRounds >= 30 && Score[0] != Score[1])
        {
            SceneManager.LoadScene("EndScreen");
        }

        p1Selected = null;
        p2Selected = null;
        DeselectP1();
        DeselectP2();
    }

    private bool CheckIfDefended(PlayerMovement.Direction direction, PlayerMovement[] oppositePlayerPieces, PlayerMovement selected)
    {
        for (int i = 0; i < 3; i++)
        {
            if (oppositePlayerPieces[i].position == selected.position ) return true;
        }
        return false;
    }
}