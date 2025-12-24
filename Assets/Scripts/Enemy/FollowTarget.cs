using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public float speed;
    public Transform targetTransform;

    string targetTag = "Base";
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject target = GameObject.FindGameObjectWithTag(targetTag);
        if (target != null)
        {
            targetTransform = target.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTransform != null)
        {
            Vector2 direction = (targetTransform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);
        }
    }

}
