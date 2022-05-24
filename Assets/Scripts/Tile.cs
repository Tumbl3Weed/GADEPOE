using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject p1Soft;
    public GameObject p2Soft;
    public GameObject p1Strong;
    public GameObject p2Strong;
    public enum TileType { neutral, player1Soft, player2Soft, player1Strong, player2Strong}
    public TileType thisTileType = TileType.neutral;
    public Vector2 location;

    public void setToSoft(int player)
    {
        p1Soft.SetActive(false);
        p2Soft.SetActive(false);
        p1Strong.SetActive(false);
        p2Strong.SetActive(false);
        if (player == 1)
        {
            p1Soft.SetActive(true);
            thisTileType = TileType.player1Soft;
        }
        else if (player == 2)
        {
            p2Soft.SetActive(true);
            thisTileType = TileType.player2Soft;
        }
        else
        {
            Debug.LogError("wrong player value sent" + player.ToString());
        }
    }

    public void setToStrong(int player)
    {
        p1Soft.SetActive(false);
        p2Soft.SetActive(false);
        p1Strong.SetActive(false);
        p2Strong.SetActive(false);
        if (player == 1)
        {
            p1Strong.SetActive(true);
            thisTileType = TileType.player1Strong;
        }
        else if(player == 2)
        {
            p2Strong.SetActive(true);
            thisTileType = TileType.player2Strong;
        }
        else
        {
            Debug.LogError("Noob Programmer detected");
        }
    }
}
