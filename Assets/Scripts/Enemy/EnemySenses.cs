using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySenses : MonoBehaviour
{
    EnemyBehavior parent;

    List<Transform> targetsInRange = new List<Transform>();

    void Start()
    {
        Transform parentTransform = transform.parent; 
        parent = parentTransform.GetComponent<EnemyBehavior>();
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
            parent.targetTransform = closestTarget;
            parent.isSeeing = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Plant" || collision.transform.tag == "Player")
        {
            targetsInRange.Add(collision.transform);

            if (parent.isSeeing == false)
            {
                parent.targetTransform = collision.transform;
                parent.isSeeing = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.tag == "Plant" || collision.transform.tag == "Player")
        {
            targetsInRange.Remove(collision.transform);

            if (parent.targetTransform == collision.transform)
            {
                parent.targetTransform = null;
                parent.isSeeing = false;
                SearchNewTarget();
            }
        }
    }
}
