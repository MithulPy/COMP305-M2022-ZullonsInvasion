using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 8f;
    private float direction = 0f;
    private Rigidbody2D player;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    private Animator playerAnimation;

    private Vector3 respawnPoint;
    public GameObject fallDetector;

    public Text scoreText;
    public HealthBar healthBar;
   
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        respawnPoint = transform.position;
        scoreText.text = "Score: " + Scoring.totalScore;
        
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        direction = Input.GetAxis("Horizontal");

        if (direction > 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(0.2469058f, 0.2469058f);
        }
        else if (direction < 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(-0.2469058f, 0.2469058f);
        }
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        }

        playerAnimation.SetFloat("Speed", Mathf.Abs(player.velocity.x));
        playerAnimation.SetBool("OnGround", isTouchingGround);

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Gems")
        {
            Scoring.totalScore += 1;
            scoreText.text = "Score: " + Scoring.totalScore;
            collision.gameObject.SetActive(false);
        }


        if (collision.tag == "Spaceship")
        {
            Health.totalHealth = 1f;
            SceneManager.LoadScene("Level2");

    }
        if (collision.tag == "Gate")
        {
            Health.totalHealth = 1f;
            SceneManager.LoadScene("Level3");

        }
        if (collision.tag == "Queen")
    {
            Health.totalHealth = 1f;
            SceneManager.LoadScene("YouWin");

        }
        if (collision.tag == "FallDetector")
        {
            SceneManager.LoadScene("GameOver");
            Health.totalHealth = 1f;

        }
 

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Spikes")
        {
            healthBar.Damage(0.002f);
            
        }
        if (collision.tag == "Health1")
        {
            if (Health.totalHealth < 1f)
            {

                healthBar.Damage(-0.02f);
                Destroy(collision.gameObject);
            }
        }
        if (collision.tag == "Health2")
        {
            if (Health.totalHealth < 1f)
            {

                healthBar.Damage(-0.02f);
                Destroy(collision.gameObject);
            }
        }
        if (collision.tag == "Health3")
        {
            if (Health.totalHealth < 1f)
            {

                healthBar.Damage(-0.02f);
                Destroy(collision.gameObject);
            }
        }
    }

    public void TakeDamage( float damage )
    {
        healthBar.Damage(damage);
        
    }
}