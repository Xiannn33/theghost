using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Player : MonoBehaviour
{
    private float speed = 10;
    private int damage = 10;
    private float destroyDistance = 5;

    private Rigidbody2D rb;
    private Vector3 startPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = -transform.right * speed;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - startPos).sqrMagnitude > destroyDistance)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<FSM>().TakeDamage(damage);
        }
    }
}
