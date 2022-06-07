using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float playerSpeed = 5.0f;
    public float jumpPower = 1f;
    private Rigidbody2D playerRB;
    private Animator am;
    private SpriteRenderer sr;
    private LayerMask groundMask;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        groundMask = LayerMask.GetMask("Default");
        am = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        if ( Input.GetButtonUp("Jump")) Jump();
    }

    void MovePlayer()
    {
        if (Input.GetButton("Horizontal"))
        {
            // am.SetBool("Walking",true);
            float axis = Input.GetAxis("Horizontal");
            if (axis < 0) sr.flipX = true; else sr.flipX = false;
            transform.Translate( axis * Vector2.right * playerSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 0);
        }
        else {
            // am.SetBool("Walking",false);
        }
    }

    void Jump()
    {
        // am.SetTrigger("Jump");
        // if (Ground())   playerRB.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        // playerRB.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        playerRB.AddForce(new Vector2(0,jumpPower));
    }

    Boolean Ground()
    {
        var groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, groundMask);
        if (groundCheck.collider.gameObject != null)
        {
            if (groundCheck.collider.CompareTag("Ground")) {
                Debug.Log(true);
                // am.ResetTrigger("Jump");
                return true;
            }
        }
        Debug.Log("test");
        return false;
    }

}
