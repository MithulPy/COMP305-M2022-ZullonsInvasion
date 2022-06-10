using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private float playerSpeed = 5.0f;
    private float jumpSpeed = 9f;
    private Boolean grounded = true;
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
        if (Input.GetButtonUp("Jump"))
        {
            am.SetBool("Jumping",true);
            if ( grounded )
            {
                jumps++;
                Jump();
            }
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
        playerRB.AddForce(new Vector2(0, jumpSpeed),ForceMode2D.Impulse);
    }

    private void Ground()
    {
        var groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, groundMask);
        if (groundCheck.collider.gameObject != null)
        {
            Debug.Log("Not Null");

            if (groundCheck.collider.CompareTag("Ground")) {
                jumps = 0;
                grounded = true;
                am.SetBool("Jumping",false);
            }
            else grounded = false;
        } else grounded = false;
    }


}
