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
    public Animator animator;


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
        animator.SetFloat("Speed", Mathf.Abs(xInput));
        CheckInput();
        HandleJump();

    }
    void FixedUpdate()
    {
        CheckGround();
        MoveWithInput();
       
    }
    void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }
    void MoveWithInput()
    {
        if (math.abs(xInput) > 0)
        {
            float increment = xInput * acceleration;
            float newspeed = Mathf.Clamp(body.linearVelocity.x + increment, -groundSpeed, groundSpeed);
            body.linearVelocity = new Vector2(newspeed, body.linearVelocity.y);

            float direction = Mathf.Sign(xInput);
            transform.localScale = new Vector3(direction * 0.15f, 0.15f, 1);
        }
        else if (grounded)
        {
            body.linearVelocity = new Vector2(0, body.linearVelocity.y);
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
        grounded = false;
        Collider2D[] hits = Physics2D.OverlapBoxAll(GroundCheck.bounds.center, GroundCheck.bounds.size, 0f, groundMask);

        if (hits.Length > 0)
        {
            grounded = true;
        }
        else
        {
            
            Collider2D[] allHits = Physics2D.OverlapBoxAll(GroundCheck.bounds.center, GroundCheck.bounds.size, 0f);
            foreach (var hit in allHits)
            {
                if (hit.GetComponent<Destructible>() != null)
                {
                    grounded = true;
                    break;
                }
            }
        }
    }
}


