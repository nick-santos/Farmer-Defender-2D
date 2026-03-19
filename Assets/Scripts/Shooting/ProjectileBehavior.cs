using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    
    [Header("Attributes")]
    public float speed = 10.0f;
    public int projectileDamage = 1;
    public float range = 5f;

    private Rigidbody2D rb;
    private Transform target;
    private Vector3 origin;
    private float distance;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        origin = transform.position;
    }

    public void SetTarget (Transform _target) {
        target = _target;
    }

    void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, origin);
        if (Vector2.Distance(transform.position, origin) >= range)
        {
            // Debug.Log(Vector2.Distance(transform.position, origin));
            Destroy(this.gameObject);
        }

        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(projectileDamage);
        }
        Debug.Log("Hit " + collision.name);
        Destroy(this.gameObject);
    }
}
