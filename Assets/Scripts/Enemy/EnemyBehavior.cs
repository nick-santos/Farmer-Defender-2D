using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("References")]
    public GameObject spawnPoint;

    [Header("Attributes")]
    public float speed = 2f;
    public float direction = 1f;
    public bool isSeeing = false; 
    public bool isClose = false;

    public Transform targetTransform;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isSeeing)
        {
            Chase();
        }
        else if(isClose)
        {
            //Attack();
        }
        else
        {
            //Idle();
        }
    }

    void Chase()
    {
        if (targetTransform != null)
        {
            Vector2 direction = (targetTransform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);
        }
    }
}
