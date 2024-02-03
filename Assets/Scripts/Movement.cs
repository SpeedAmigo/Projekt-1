using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region Components

    private float xAxis;
    private float yAxis;
    private Rigidbody rb;

    [Header("Player Speed")]
    public float speed;

    #endregion

    #region Initialization

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    #endregion

    #region Input Handling

    private void MyInput()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = new Vector3(xAxis, 0, yAxis).normalized;
        rb.AddRelativeForce(inputDirection * speed * 10f, ForceMode.Force);
    }

    #endregion

    #region Speed Limitation

    private void SpeedLimit()
    {
        Vector3 velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (velocity.magnitude > speed)
        {
            Vector3 limitVel = velocity.normalized * speed;
            rb.velocity = new Vector3(limitVel.x, rb.velocity.y, limitVel.z);
        }
    }

    #endregion

    #region Sprint Handling

    private void Sprint()
    {
        speed = Input.GetKey(KeyCode.LeftShift) ? 8f : 5f;
    }

    #endregion

    #region Raycast Handler

    private bool onGround;

    public void RaycastF()    
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1.5f))
        {
           onGround = true;
        }
        else
        {
            onGround = false;
        }
    }

    #endregion

    #region Jump Handling

    public void JumpF()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround == true) 
        {
            rb.velocity = new Vector3(0, 5f, 0);
        }
    }

    #endregion

    #region Update Functions

    private void FixedUpdate()
    {
        Sprint();
        SpeedLimit();
        MyInput();
    }

    private void Update()
    {
       JumpF();
       RaycastF();
    }

    #endregion
}
