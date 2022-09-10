using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMaster : MonoBehaviour
{
    public int team1Score = 0, team2Score = 0;
    public int scoreToWin = 11;

    public TextMeshProUGUI team1ScoreText, team2ScoreText, winText;
    public GameObject finishCanvas;

    GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("Ball");
    }

    void UpdateUI ()
    {
        team1ScoreText.SetText("" + team1Score);
        team2ScoreText.SetText("" + team2Score);
    }

    void GameEnd (int winningTeam)
    {
        finishCanvas.SetActive(true);
        winText.SetText("Team " + winningTeam + " wins!");
        Destroy(ball);
    }

    public void TeamScored (int teamNum)
    {
        if (teamNum == 1)
            team1Score++;
        else
            team2Score++;

        UpdateUI();
        if (team1Score > scoreToWin) 
        {
            GameEnd(1);
        }
        if (team2Score > scoreToWin)
        {
            GameEnd(2);
        }

    }
}
