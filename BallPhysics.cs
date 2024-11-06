using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BallPhysics : MonoBehaviour
{
    [SerializeField] private GameObject ball;

    public float initialSpeed = 10f;
    private float speed;
    public float speedIncrement = 5f;
    private bool isMoving = false;
    
    private readonly Vector3 startingPosition = new(25, 0, 0);
    private Vector3 direction = new Vector3(1, 0, 2).normalized;

    public event Action<int> OnGoalScored;
    // Start is called before the first frame update
    void Start() {
        ball.transform.position = startingPosition;
        setRandDir();
        speed = initialSpeed;
    }

    void setRandDir() {
        var randomX = UnityEngine.Random.Range(-3f, 3f);
        var randomZ = UnityEngine.Random.Range(-3f, 3f);
        direction = new Vector3(randomX, 0, randomZ).normalized;

    }

    public void StartMove() {
        if (isMoving) 
            return;
        
        speed = initialSpeed;
        setRandDir();
        isMoving = true;
    }
    
 
    
    // Update is called once per frame
    void Update() {
        if (!isMoving) 
            return;
        ball.transform.Translate(direction * (speed * Time.deltaTime));
    }
    
    private void OnCollisionEnter(Collision col) {
        
        if (col.gameObject.CompareTag("Player")) {
            collidePlayer(col);
        }
        else if (col.gameObject.CompareTag("Wall")) {
            collideWall();
        }
        else if (col.gameObject.CompareTag("Player1Net")) {
            goalScored(1);
        }
        else if (col.gameObject.CompareTag("Player2Net")) {
            goalScored(0);
        }
    }

    
    void collidePlayer(Collision player) {
        var playerZLoc = player.gameObject.transform.position.z;
        var ballZLoc = ball.gameObject.transform.position.z;
        
        //value between 2.5 and -2.5
        var normalizedCollisionLocation = Math.Round(ballZLoc - playerZLoc, 1);
        var angle = normalizedCollisionLocation * 18.0f;

        direction = new Vector3(-direction.x, 0,Mathf.Tan((float) angle * Mathf.Deg2Rad)).normalized; 
        speed += speedIncrement;
    }
    
    void collideWall() {
        direction = new Vector3(direction.x, 0, -direction.z).normalized;
    }

    private void goalScored(int playerWhoScored)
    {
        ball.transform.position = startingPosition;
        isMoving = false;
        direction = Vector3.zero;

        OnGoalScored?.Invoke(playerWhoScored == 0 ? 0 : 1);
    }
}
