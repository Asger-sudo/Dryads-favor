using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using System.Runtime.InteropServices;

public class Movement : MonoBehaviour
{
     public Rigidbody2D body;
    public BoxCollider2D GroundCheck;
    public LayerMask groundMask;


    public float acceleration; 
    [Range(0f, 5f)]
    public float groundSpeed;
    public float groundDecay;


    public float jumpspeed;
    

    public bool grounded;
    float xInput;
    float yInput;

    void Start()
    {

    }
    void Update()
    {
        CheckInput();
        HandleJump();

    }
    void FixedUpdate()
    {
        CheckGround();
        MoveWithInput();
        ApplyFriction();
       
    }
    void CheckInput()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }
    void MoveWithInput()
    {
        if (math.abs(xInput) > 0)
        {
            float increment = xInput * acceleration;
            float newspeed = Mathf.Clamp(body.linearVelocity.x + increment, -groundSpeed, groundSpeed);
            body.linearVelocity = new Vector2(newspeed, body.linearVelocity.y);

            float direction = Mathf.Sign(xInput);
            transform.localScale = new Vector3(direction* 0.1f,0.1f, 1);
        }

    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpspeed);
        }
    }

    void CheckGround()
    {
        grounded = Physics2D.OverlapAreaAll(GroundCheck.bounds.min, GroundCheck.bounds.max, groundMask).Length > 0;
    }

    void ApplyFriction()
    {
        if (grounded && xInput == 0 && body.linearVelocity.y <=0)
        {body.linearVelocity *= groundDecay; }
       
    }
}

