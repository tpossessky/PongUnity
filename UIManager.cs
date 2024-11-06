using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {
    

    [SerializeField] private TextMeshProUGUI p1ScoreText;
    [SerializeField] private TextMeshProUGUI p2ScoreText; 
    
    [SerializeField] private TextMeshProUGUI p1Win;
    [SerializeField] private TextMeshProUGUI p2Win;

    
    public void SetPlayer1Goals(int goals) {
        p1ScoreText.text = goals.ToString();
    }

    public void SetPlayer2Goals(int goals) {
        p2ScoreText.text = goals.ToString();
    }


    public void Player1Win() {
        p1Win.text = "Player 1 Wins!";

    }

    public void Player2Win() {
        p2Win.text = "Player 2 Wins!";
    }

    public void NewGame() {

        
        p1Win.text = "";
        p2Win.text = "";
        SetPlayer1Goals(0);
        SetPlayer2Goals(0);
    }
    
}
