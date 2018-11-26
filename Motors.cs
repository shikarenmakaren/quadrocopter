using UnityEngine;
using System.Collections;

public class Motors : MonoBehaviour
{
    public float power = 0.0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.AddRelativeForce(Vector2.up*power);
    }

}
