using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupGame : MonoBehaviour
{
    public int sizex = 10, sizey = 10;
    public GameObject tilePrefab;
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;
    public GameObject player1TilePrefab;
    public GameObject player2TilePrefab;
    public GameObject player1StrongPrefab;
    public GameObject player2StrongPrefab;
    public Tile[,] gameBoard;
    public int amountOfPieces = 3;
    [SerializeField]
    public Vector2Int[] startingLocations;
    public PlayerManger playerManger;

    void Start()
    {
        gameBoard = new Tile[sizex, sizey];
        playerManger.player1 = new PlayerMovement[amountOfPieces];
        playerManger.player2 = new PlayerMovement[amountOfPieces];
        for (int x = 0; x < sizex; x++)
        {
            for (int y = 0; y < sizey; y++)
            {
                gameBoard[x, y] = Instantiate(tilePrefab, new Vector3(x, y, 0), transform.rotation, transform).GetComponent<Tile>();
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
        int startingLocationPos = Random.Range(0, startingLocations.Length);
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
        int startingLocationPos = Random.Range(0, startingLocations.Length);
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
            startingLocationPos = Random.Range(0, startingLocations.Length);
        }
        return startingLocationPos;
    }
    public void setTileSoft(Vector2Int pos, int player)
    {
        gameBoard[(int)pos.x, (int)pos.y].setToSoft(player);

    }

    public void setTileStrong(Vector2 pos, int player)
    {
        gameBoard[(int)pos.x, (int)pos.y].setToStrong(player);
    }
}
