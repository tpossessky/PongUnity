using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PongGame : MonoBehaviour {
    
    [SerializeField] private BallPhysics ball;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PlayersController playersController;
    
    private int p1Goals;
    private int p2Goals;
    private bool winCondition;
    private int maxGoals = 2;
    void Start() {
        ball.OnGoalScored += HandleGoalScored;
    }

    void HandleGoalScored(int playerWhoScored) {
        playersController.ResetPosition();

        if (playerWhoScored == 0) {
            p1Goals++;
            uiManager.SetPlayer1Goals(p1Goals);
            if (p1Goals == maxGoals) {
                uiManager.Player1Win();
                winCondition = true;
            }
        }
        else {
            p2Goals++;
            uiManager.SetPlayer2Goals(p2Goals);
            if (p2Goals == maxGoals) {
                uiManager.Player2Win();
                winCondition = true;
            }        
        }
    }

    public void SendBall(InputAction.CallbackContext context) {
        if (winCondition) {
            uiManager.NewGame();
            p1Goals = 0;
            p2Goals = 0;
            winCondition = false;
        }
        playersController.StartRound();
        ball.StartMove();
    }
    
}