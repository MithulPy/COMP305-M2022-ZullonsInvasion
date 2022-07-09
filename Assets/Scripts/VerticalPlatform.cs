using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float RestartTimer;
    // Start is called before the first frame update
    private void MoveUp()
    {
        rb.velocity = transform.up * speed;
    }

    private void MoveDown()
    {
        rb.velocity = -transform.up * speed;
    }

    // Update is called once per frame
    private void Start()
    {
        InvokeRepeating("MoveUp", 0, RestartTimer);
        InvokeRepeating("MoveDown", RestartTimer / 2, RestartTimer);


    }
}
