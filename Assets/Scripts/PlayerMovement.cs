using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject[] positionCanGoHighlight = new GameObject[4];//up,left,down,right(wasd)
    public int playerNumber; //player 1 or 2
    public Vector2 upperBounds;
    public Vector2Int position;
    public enum Direction { up, left, down, right }
    public enum Player { p1, p2 }
    public PlayerManger pm;



    void Update()
    {
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        if (pm == null)
            pm = FindObjectOfType<PlayerManger>().GetComponent<PlayerManger>();
        if (positionCanGoHighlight[0].gameObject.activeInHierarchy)
            if (playerNumber == 1 && Input.GetKeyDown(KeyCode.W) || playerNumber == 2 && Input.GetKeyDown(KeyCode.UpArrow))
            {
                this.StoreMovement(Direction.up);
            }
        if (positionCanGoHighlight[1].gameObject.activeInHierarchy)
            if (playerNumber == 1 && Input.GetKeyDown(KeyCode.A) || playerNumber == 2 && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                this.StoreMovement(Direction.left);
            }
        if (positionCanGoHighlight[2].gameObject.activeInHierarchy)
            if (playerNumber == 1 && Input.GetKeyDown(KeyCode.S) || playerNumber == 2 && Input.GetKeyDown(KeyCode.DownArrow))
            {
                this.StoreMovement(Direction.down);
            }
        if (positionCanGoHighlight[3].gameObject.activeInHierarchy)
            if (playerNumber == 1 && Input.GetKeyDown(KeyCode.D) || playerNumber == 2 && Input.GetKeyDown(KeyCode.RightArrow))
            {
                this.StoreMovement(Direction.right);
            }
    }
    private void StoreMovement(Direction dir)
    {
        if (playerNumber == 1)
        {
            pm.p1Direction = dir;
            pm.playerTurns[0] = true;
            pm.movePlayers();
        }
        else if (playerNumber == 2)
        {
            pm.playerTurns[1] = true;
            pm.p2Direction = dir;
            pm.movePlayers();
        }
        else
        {
            Debug.LogError("Check here for mistake, which you makes sometimes but not always but only sometimes.King.");
        }
    }

    public void OnMouseDown()
    {
        SelectingPlayer();
    }

    public void SelectingPlayer()
    {
        if (playerNumber == 1)
        {
            pm.selectedPlayer(this);
            pm.playerTurns[0] = false;
            pm.tileDefended[0] = false;
        }
        else
        {
            pm.selectedPlayer(this);
            pm.playerTurns[1] = false;
            pm.tileDefended[1] = false;
        }
        Selected();
    }

    public void Deselect()
    {
        for (int i = 0; i < 4; i++)
        {
            positionCanGoHighlight[i].SetActive(false);
        }
    }
    public void Selected()  // 0,1,2,3 = up,left,down,right
    {
        if (position.y != upperBounds.y - 1 && checkIfPlayerCanGo(Direction.up))
            positionCanGoHighlight[(int)Direction.up].SetActive(true);           
        else
            positionCanGoHighlight[(int)Direction.up].SetActive(false);

        if (position.x != 0 && checkIfPlayerCanGo(Direction.left))
            positionCanGoHighlight[(int)Direction.left].SetActive(true);
        else
            positionCanGoHighlight[(int)Direction.left].SetActive(false);

        if (position.y != 0 && checkIfPlayerCanGo(Direction.down))
            positionCanGoHighlight[(int)Direction.down].SetActive(true);
        else
            positionCanGoHighlight[(int)Direction.down].SetActive(false);

        if (position.x != upperBounds.x - 1 && checkIfPlayerCanGo(Direction.right))
            positionCanGoHighlight[(int)Direction.right].SetActive(true);
        else
            positionCanGoHighlight[(int)Direction.right].SetActive(false);
    }

    public void makeMove(int dir)
    {
        //takes in up,left,down,right(wasd)
        switch (dir)
        {
            case 0:
                transform.position += Vector3.up; position += Vector2Int.up;
                break;
            case 1:
                transform.position += Vector3.left; position += Vector2Int.left;
                break;
            case 2:
                transform.position += Vector3.down; position += Vector2Int.down;
                break;
            case 3:
                transform.position += Vector3.right; position += Vector2Int.right;
                break;

            default:
                break;
        }
    }
    private bool checkIfPlayerCanGo(Direction thisDir)
    {
        Vector2Int dir;
        switch (thisDir)
        {
            case Direction.up:
                dir = Vector2Int.up;
                break;
            case Direction.left:
                dir = Vector2Int.left;
                break;
            case Direction.down:
                dir = Vector2Int.down;
                break;
            case Direction.right:
                dir = Vector2Int.right;
                break;
            default:
                dir = Vector2Int.up;
                break;
        }
        
        if (playerNumber == 1)
        {
            pm.tileDefended[0] = false;
            for (int i = 0; i < 3; i++)
            {
                if (pm.player2[i].position == position + dir) pm.tileDefended[0]=true;
            }
            for (int i = 0; i < 3; i++)
            {
                if (pm.player1[i].position == position + dir) return false;
            }
        }
        else         //player 2
        {
            pm.tileDefended[1] = false;
            for (int i = 0; i < 3; i++)
            {
                if (pm.player1[i].position == position + dir) pm.tileDefended[1] = true;
            }
            for (int i = 0; i < 3; i++)
            {
                if (pm.player2[i].position == position + dir) return false;
            }
        }
        return true;
    }
}