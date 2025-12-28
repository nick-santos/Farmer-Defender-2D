using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    
    [Header("Attributes")]
    public float speed = 10.0f;
    public int projectileDamage = 1;

    private Rigidbody2D rb;
    private Transform target;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetTarget (Transform _target) {
        target = _target;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
