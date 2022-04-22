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
                StoreMovement(Direction.up);
            }
        if (positionCanGoHighlight[1].gameObject.activeInHierarchy)
            if (playerNumber == 1 && Input.GetKeyDown(KeyCode.A) || playerNumber == 2 && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StoreMovement(Direction.left);
            }
        if (positionCanGoHighlight[2].gameObject.activeInHierarchy)
            if (playerNumber == 1 && Input.GetKeyDown(KeyCode.S) || playerNumber == 2 && Input.GetKeyDown(KeyCode.DownArrow))
            {
                StoreMovement(Direction.down);
            }
        if (positionCanGoHighlight[3].gameObject.activeInHierarchy)
            if (playerNumber == 1 && Input.GetKeyDown(KeyCode.D) || playerNumber == 2 && Input.GetKeyDown(KeyCode.RightArrow))
            {
                StoreMovement(Direction.right);
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
        else
        {
            pm.playerTurns[1] = true;
            pm.p2Direction = dir;
            pm.movePlayers();
        }
    }

    private void OnMouseDown()
    {
        if (playerNumber == 1)
            pm.selectedPlayer(this);
        else
            pm.selectedPlayer(this);
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

        if (position.y != upperBounds.y - 1)
            positionCanGoHighlight[(int)Direction.up].SetActive(true);
        else
            positionCanGoHighlight[(int)Direction.up].SetActive(false);

        if (position.x != 0)
            positionCanGoHighlight[(int)Direction.left].SetActive(true);
        else
            positionCanGoHighlight[(int)Direction.left].SetActive(false);

        if (position.y != 0)
            positionCanGoHighlight[(int)Direction.down].SetActive(true);
        else
            positionCanGoHighlight[(int)Direction.down].SetActive(false);

        if (position.x != upperBounds.x - 1)
            positionCanGoHighlight[(int)Direction.right].SetActive(true);
        else
            positionCanGoHighlight[(int)Direction.right].SetActive(false);
    }

    public void makeMove(int dir)

    {   //takes in up,left,down,right(wasd)
        Debug.Log("makeMove  hahhaa");
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

}



