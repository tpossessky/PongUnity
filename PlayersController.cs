using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersController : MonoBehaviour
{
    [SerializeField] private GameObject goP1;
    [SerializeField] private GameObject goP2;
    private CharacterController p1;
    private CharacterController p2;

    [SerializeField] private float speed = 1f;

    private Vector3 p1Direction;
    private Vector3 p2Direction;

    private const float pX = 0f;
    private const float pY = 0f;
    
    //position max/min z=12
    void Start()
    {
        
    }

    private void Awake()
    {
        p1 = goP1.GetComponent<CharacterController>();
        p2 = goP2.GetComponent<CharacterController>();

    }

    // Update is called once per frame

    void Update()
    {
        // Get current positions
        var p1PosZ = goP1.transform.position.z;
        var p2PosZ = goP2.transform.position.z;

        // Check bounds for p1
        var newP1PosZ = p1PosZ + (p1Direction.z * speed * Time.deltaTime);
        if (newP1PosZ is < -12f or > 12f)
        {
            // Stop p1 from moving beyond bounds
            p1Direction.z = 0;
        }

        // Check bounds for p2
        var newP2PosZ = p2PosZ + (p2Direction.z * speed * Time.deltaTime);
        if (newP2PosZ is < -12f or > 12f)
        {
            // Stop p2 from moving beyond bounds
            p2Direction.z = 0;
        }

        // Move p1 and p2 with adjusted directions
        if(p1.enabled)
            p1.Move(p1Direction * (speed * Time.deltaTime));
        if(p2.enabled)
            p2.Move(p2Direction * (speed * Time.deltaTime));
    }

    public void MoveP1(InputAction.CallbackContext context)
    {
        //actually z
        var input = context.ReadValue<Vector2>().y;
        p1Direction = new Vector3(pX, pY, input);
        
    }
    
    public void MoveP2(InputAction.CallbackContext context)
    {
        //actually z
        
        var input = context.ReadValue<Vector2>().y;
        p2Direction = new Vector3(pX, pY, input);
    }

    public void ResetPosition() {
        p1.enabled = false;
        p2.enabled = false;
        p1.transform.position = new Vector3(0, 0, 0);
        p2.transform.position = new Vector3(50, 0, 0);
    }

    public void StartRound() {
        p1.enabled = true;
        p2.enabled = true;  
    }
}
