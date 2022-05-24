using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Game : MonoBehaviour
{
    public int sizex = 10, sizey = 10;
    public GameObject tilePrefab;
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;

    public Tile[,] gameBoard;
    public int amountOfPieces = 3;

    [SerializeField]
    public Vector2Int[] startingLocations;
    public PlayerManger playerManger;
    public int strongTileCounter = 0;
    public int coinFlickPlayerTurn = 0;

    public GameObject canvas;
    public Text coinResults;
    public Text coinFace;

    public TextMeshProUGUI textScoreP1;

    public TextMeshProUGUI textScoreP2;

    public TMPro.TextMeshProUGUI rounds;

    public TMPro.TextMeshProUGUI strongTileRound;

    public int intRounds = 0;

    public int intStrongTileRounds = 0;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            canvas.SetActive(false);
        }
        rounds.text = "Rounds: " + intRounds;
    }

    void Start()
    {
        canvas.SetActive(false);
        gameBoard = new Tile[sizex, sizey];
        playerManger.player1 = new PlayerMovement[amountOfPieces];
        playerManger.player2 = new PlayerMovement[amountOfPieces];
        for (int x = 0; x < sizex; x++)
        {
            for (int y = 0; y < sizey; y++)
            {
                gameBoard[x, y] = Instantiate(tilePrefab, new Vector3(x, y, 0), transform.rotation).GetComponent<Tile>();
                gameBoard[x, y].location.x = x;
                gameBoard[x, y].location.y = y;
            }
        }
        for (int i = 0; i < amountOfPieces; i++)
        {
            CreatePlayer1(i);
            CreatePlayer2(i);
        }
    }

    private void CreatePlayer1(int i)
    {
        int startingLocationPos = UnityEngine.Random.Range(0, startingLocations.Length);
        startingLocationPos = GetStartPos(startingLocationPos);

        playerManger.player1[i] = Instantiate(Player1Prefab, new Vector3(startingLocations[startingLocationPos].x,
            startingLocations[startingLocationPos].y, 0), transform.rotation).GetComponent<PlayerMovement>();
        gameBoard[startingLocations[startingLocationPos].x,
            startingLocations[startingLocationPos].y].GetComponent<Tile>().thisTileType = Tile.TileType.player1Soft;
        PlayerMovement p1 = playerManger.player1[i].GetComponent<PlayerMovement>();
        p1.position = new Vector2Int(startingLocations[startingLocationPos].x, startingLocations[startingLocationPos].y);
        p1.upperBounds = new Vector2(sizex, sizey);
        p1.playerNumber = 1;
        setTileSoft(p1.position, 1);
    }
    private void CreatePlayer2(int i)
    {
        int startingLocationPos = UnityEngine.Random.Range(0, startingLocations.Length);
        startingLocationPos = GetStartPos(startingLocationPos);

        playerManger.player2[i] = Instantiate(Player2Prefab, new Vector3(startingLocations[startingLocationPos].x,
            startingLocations[startingLocationPos].y, 0), transform.rotation).GetComponent<PlayerMovement>();
        gameBoard[startingLocations[startingLocationPos].x,
            startingLocations[startingLocationPos].y].GetComponent<Tile>().thisTileType = Tile.TileType.player2Soft;
        PlayerMovement p2 = playerManger.player2[i].GetComponent<PlayerMovement>();
        p2.position = new Vector2Int(startingLocations[startingLocationPos].x, startingLocations[startingLocationPos].y);
        p2.upperBounds = new Vector2(sizex, sizey);
        p2.playerNumber = 2;
        setTileSoft(p2.position, 2);
    }

    private int GetStartPos(int startingLocationPos)
    {
        while (gameBoard[startingLocations[startingLocationPos].x,
            startingLocations[startingLocationPos].y].GetComponent<Tile>().thisTileType !=
            Tile.TileType.neutral)
        {
            startingLocationPos = UnityEngine.Random.Range(0, startingLocations.Length);
        }
        return startingLocationPos;
    }

    public void setTileOwned(Vector2Int pos, int player, bool defended)
    {
       
        //first check if it is an enemy strong tile
        if (gameBoard[pos.x, pos.y].thisTileType == Tile.TileType.player1Strong && player == 2)
        {
            Debug.Log("Setup Game: pos:" + pos.ToString() + " player: " + player.ToString() + " defended: " + defended.ToString() +" strong: True");
            if (winCoinFlip(player))
            {

                if (defended)
                {
                    setTileOwned(pos, 2, false);

                }
                else
                    setTileSoft(pos, 2);


            }
            else
            {
                setTileSoft(pos, 1);

            }
        }
        else if (gameBoard[pos.x, pos.y].thisTileType == Tile.TileType.player2Strong && player == 1)
        {
            Debug.Log("Setup Game: pos:" + pos.ToString() + " player: " + player.ToString() + " defended: " + defended.ToString() + " strong: True");
            if (winCoinFlip(player))
            {
                if (defended)
                {
                    setTileOwned(pos, 1, false);
                }
                else
                    setTileSoft(pos, 1);
            }
            else
            {
                setTileSoft(pos, 2);
            }
        }
        //once resolved set the tile to soft of this player and increase turn counter
        else if (gameBoard[pos.x, pos.y].thisTileType == Tile.TileType.neutral ||
            gameBoard[pos.x, pos.y].thisTileType == Tile.TileType.player1Soft ||
            gameBoard[pos.x, pos.y].thisTileType == Tile.TileType.player2Soft)
        {
            Debug.Log("Setup Game: pos:" + pos.ToString() + " player: " + player.ToString() + " defended: " + defended.ToString() + " strong: false");
            if (defended)
            {
                if (winCoinFlip(player))
                {
                    setTileSoft(pos, player);
                }
                else
                {
                    if (player == 1)
                        setTileSoft(pos, 2);
                    if (player == 2)
                        setTileSoft(pos, 1);
                }
            }
            else
                setTileSoft(pos, player);
        }
        strongTileCounter++;
                                                         //Cast 
        strongTileRound.text = "Next Strong Tile: " + ((int)strongTileCounter / 2).ToString();
        

   
        //if the turn is 7 or 8, set a tile strong for that player
        if (strongTileCounter >= 7)
        {
            setTileStrong(pos, player);
            if (strongTileCounter == 8)//if turn 8, set the counter to 0 again
                strongTileCounter = 0;
        }
        CalculateScore();
    }

    private void CalculateScore()
    {
        playerManger.Score[0] = 0;
        playerManger.Score[1] = 0;
        foreach (var tile in gameBoard)
        {
            if (tile.thisTileType.Equals(Tile.TileType.player1Soft) || tile.thisTileType.Equals(Tile.TileType.player1Strong))
            {
                playerManger.Score[0]++;
            }
            if (tile.thisTileType.Equals(Tile.TileType.player2Soft) || tile.thisTileType.Equals(Tile.TileType.player2Strong))
            {
                playerManger.Score[1]++;
            }
        }
        textScoreP1.text = "Player 1 : " + playerManger.Score[0].ToString();
        textScoreP2.text = "Player 2 : " + playerManger.Score[1].ToString();
    }

    private bool winCoinFlip(int player)
    {
        canvas.SetActive(true);
        if (0 == UnityEngine.Random.Range(0, 2))
        {
            Debug.Log("Won coin flip");
            coinResults.text = "Player "+player.ToString()+" flicks a coin. " +player.ToString()+" Won!";
            coinFace.text = "It was Heads!";
            return true;
        }
        else
        {
            Debug.Log("Lost coin flip");
            coinResults.text = "Player " + player.ToString() + " flicks a coin. " + player.ToString() + " Lost!";
            coinFace.text = "It was Tails!";
            return false;
        }
    }

    private void setTileSoft(Vector2Int pos, int player)
    {
        gameBoard[(int)pos.x, (int)pos.y].setToSoft(player);
    }

    private void setTileStrong(Vector2 pos, int player)
    {
        List<Tile> tilesToCheck = new List<Tile>(FindObjectsOfType<Tile>());
        List<Tile> tiles = new List<Tile>(0);

        if (player == 1)
            foreach (var tile in tilesToCheck)
            {
                if (tile.thisTileType.Equals(Tile.TileType.player1Soft))
                    tiles.Add(tile);
            }

        if (player == 2)
            foreach (var tile in tilesToCheck)
            {
                if (tile.thisTileType.Equals(Tile.TileType.player2Soft))
                    tiles.Add(tile);
            }

        int randomVal = UnityEngine.Random.Range(0, tiles.Count);
        gameBoard[(int)tiles[randomVal].location.x, (int)tiles[randomVal].location.y].setToStrong(player);
    }
}