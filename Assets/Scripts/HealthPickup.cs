using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject whatHit = collision.gameObject;
        if(whatHit.CompareTag("Player"))
        {
            Health.totalHealth = 1f;
            Destroy(gameObject);

        }
    }
}
