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

    private void Start()
    {
        InvokeRepeating("Reload",1,1);
        mask = LayerMask.GetMask("GroundLayer")|LayerMask.GetMask("Player");
        player = GameObject.Find("Player Sprite");
        
    }

    private void Update()
    {
        // playerPos = player.transform.GetChild(0).position;
        playerPos = player.transform.position;
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
        Debug.Log("Fire");
        GameObject newBullet = Instantiate(enemyProjectile, transform.position, Quaternion.LookRotation(direction));
        newBullet.transform.up = direction;
    }

    void Reload()
    {
        if ( cooldown > 0 ) cooldown--;
    }
}