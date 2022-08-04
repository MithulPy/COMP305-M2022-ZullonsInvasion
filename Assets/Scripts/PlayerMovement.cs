using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5.0f;
    public float jumpSpeed = 9f;
    public Boolean grounded = true;
    public float groundCheckRadius = 1.15f;
    public float jumps = 0;
    public float jumpMax = 1;
    private LayerMask groundMask;

    private Rigidbody2D playerRB;
    private Animator am;
    private SpriteRenderer sr;
    AudioSource jumpsound;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        groundMask = LayerMask.GetMask("GroundLayer");
        am = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        jumpsound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // grounded = Physics2D.OverlapCircle(transform.position, 2.0f, groundMask);
        if (grounded) am.SetBool("Jumping", false);
        else am.SetBool("Jumping", true);

        // Walking
        if (Input.GetButton("Horizontal"))
        {
            am.SetBool("Walking",true);
            float axis = Input.GetAxis("Horizontal");
            if (axis < 0) sr.flipX = true; else sr.flipX = false;
            transform.Translate( axis * Vector2.right * playerSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 0);
        }
        else {
            am.SetBool("Walking",false);
        }

        // Jumping
        if (Input.GetButtonUp("Jump"))
        {
            jumpsound.Play();
            am.SetBool("Jumping",true);
            if ( grounded ) playerRB.AddForce(new Vector2(0, jumpSpeed),ForceMode2D.Impulse);
        }

        // Grounding
        RaycastHit2D groundCheck = Physics2D.Raycast(transform.position, Vector2.down, groundCheckRadius, groundMask);
        if (groundCheck)
        {
            if (groundCheck.collider.CompareTag("Ground"))
            {
                grounded = true;
                am.SetBool("Jumping",false);
            }
            else grounded = false;
        }
        else grounded = false;

    }

}
