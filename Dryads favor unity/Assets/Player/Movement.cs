using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;

public class Movement : MonoBehaviour
{
    public Rigidbody2D body;
    public float speed;
    [Range(0f, 1f)]

    public float groundDecay;
    public bool grounded;
    public Collider2D GroundCheck;
    public LayerMask groundMask;
    float xInput;
    float yInput;

    void Start()
    {

    }




    void Update()
    {
        GetInput();
        MoveWithInput();

    }
    void FixedUpdate()
    {
        CheckGround();
        ApplyFriction();

    }
    void GetInput()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }
    void MoveWithInput()
    {

        if (math.abs(xInput) > 0)
        { body.linearVelocity = new Vector2(xInput * speed, body.linearVelocity.y); }

        if (math.abs(yInput) > 0 && grounded)
        { body.linearVelocity = new Vector2(body.linearVelocity.x, yInput * speed); }
    }

    void CheckGround()
    {
        grounded = Physics2D.OverlapAreaAll(GroundCheck.bounds.min, GroundCheck.bounds.max, groundMask).Length > 0;
    }

    void ApplyFriction()
    {
        if (grounded && xInput == 0 && yInput == 0)
        {body.linearVelocity *= groundDecay; }
       
    }
}

