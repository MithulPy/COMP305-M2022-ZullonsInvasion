using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Animator camAnim;
    public int health;
    public GameObject deathEffect;
    public GameObject explosion;
    public int cooldown, cooldownMax;
    private LayerMask mask;
    public GameObject enemyProjectile, player;
    public Vector3 playerPos;
    AudioSource enemyShoot;
    public float speed;
    public Vector3[] positions;
    public int index;
    private SpriteRenderer sr;
    private void Start()
    {
        InvokeRepeating("Reload",1,1);
        mask = LayerMask.GetMask("GroundLayer")|LayerMask.GetMask("Player");
        player = GameObject.Find("Player Sprite");
        sr = GetComponent<SpriteRenderer>();
        enemyShoot = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // playerPos = player.transform.GetChild(0).position;
        playerPos = player.transform.position;
        if (( playerPos - transform.position ).x > 0 ) sr.flipX = true;
        else sr.flipX = false;
        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if ( cooldown <= 0 && (playerPos-transform.position).magnitude <= 20.0f )
        {
            Fire();
            cooldown = cooldownMax;
        }

        if ( positions.Length > 0 )
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition,positions[index],speed*Time.deltaTime);
            if ( transform.localPosition == positions[index])
            {
                if ( index == positions.Length - 1 ) index = 0;
                else index++;
            }
        }
        
    }

    public void TakeDamage(int damage)
    {
        //camAnim.SetTrigger("shake");
        Instantiate(explosion, transform.position, Quaternion.identity);
        health -= damage;
    }

    void Fire()
    {
        Vector3 direction = (playerPos - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast( transform.position , direction, 20f , LayerMask.GetMask("Player"));
        Debug.DrawLine(transform.position,transform.position+direction*20,Color.green,5);
        // Debug.DrawLine(transform.position,hit.point,Color.green,5);
        if ( hit.collider != null )
        {
            if ( hit.collider.CompareTag("Player"))
            {
                // GameObject newBullet = Instantiate(enemyProjectile, transform.position,Quaternion.identity);
            }
        }
        GameObject newBullet = Instantiate(enemyProjectile, transform.position, Quaternion.LookRotation(direction));
        newBullet.transform.up = direction;
        enemyShoot.Play();
    }

    void Reload()
    {
        if ( cooldown > 0 ) cooldown--;
    }
}