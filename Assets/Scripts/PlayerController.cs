using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public HealthBar healthBar;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Spikes")
        {
            Debug.Log("Spikes Touched");

            healthBar.Damage(0.002f);
        }
    }
}
