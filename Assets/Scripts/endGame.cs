using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class endGame : MonoBehaviour
{

    public TextMeshProUGUI textWinner;
    public TextMeshProUGUI textLoser;

    public PlayerManger playerManger;


    // Start is called before the first frame update
    void Start()
    {
        playerManger = FindObjectOfType<PlayerManger>();

        if (playerManger.Score[0] < playerManger.Score[1])
        {
            textWinner.text = playerManger.Score[0].ToString();
            textLoser.text = playerManger.Score[1].ToString();
        }
        else
        {
            textWinner.text = playerManger.Score[1].ToString();
            textLoser.text = playerManger.Score[0].ToString();
        }
    }

    // Update is called once per frame
    public void menu() 
    {
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
