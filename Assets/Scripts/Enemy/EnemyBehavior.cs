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

    Transform targetTransform;
    Rigidbody2D rb;
    List<Transform> targetsInRange = new List<Transform>();

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

    void SearchNewTarget()
    {
        Transform closestTarget = null;
        float minDistance = Mathf.Infinity;

        foreach (Transform target in targetsInRange)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestTarget = target.transform;
            }
        }

        if (closestTarget != null)
        {
            targetTransform = closestTarget;
            isSeeing = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Plant" || collision.transform.tag == "Player")
        {
            targetsInRange.Add(collision.transform);

            if (targetTransform == null)
            {
                targetTransform = collision.transform;
                isSeeing = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.tag == "Plant" || collision.transform.tag == "Player")
        {
            targetsInRange.Remove(collision.transform);

            if (targetTransform == collision.transform)
            {
                targetTransform = null;
                isSeeing = false;
                SearchNewTarget();
            }
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
