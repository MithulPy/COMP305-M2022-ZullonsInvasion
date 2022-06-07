using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float playerSpeed = 5.0f;
    private float jumpSpeed = 300.0f;
    private float jumps = 0;
    private float jumpMax = 1;
    private LayerMask groundMask;

    private Rigidbody2D playerRB;
    private Animator am;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        groundMask = LayerMask.GetMask("GroundLayer");
        am = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        if (Input.GetButtonUp("Jump") && jumps < jumpMax)
        {
            jumps++;
            Jump();
        }
        Ground();
    }

    private void MovePlayer()
    {
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
    }

    private void Jump()
    {
        am.SetTrigger("Jump");
        playerRB.AddForce(new Vector2(0, jumpSpeed));
    }

    private void Ground()
    {
        var groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 0.4f, groundMask);
        if (groundCheck.collider.gameObject != null)
        {
            if (groundCheck.collider.CompareTag("Ground")) {
                jumps = 0;
                am.ResetTrigger("Jump");
            }
        }
    }
}
