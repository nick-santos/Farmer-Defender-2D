using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("References")]
    public GameObject spawnPoint;

    [Header("Attributes")]
    public EnemyMode mode;
    public float speed = 2f;
    public float direction = 1f;
    public bool isSeeing = false; 
    public bool isClose = false;

    public Transform targetTransform;

    private Rigidbody2D rb;
    GameObject baseObject;
    private string baseTag = "Base";

    public enum EnemyMode
    {
        Normal,
        Wave
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        baseObject = GameObject.FindGameObjectWithTag(baseTag);
    }

    // Update is called once per frame
    void Update()
    {
        switch (mode)
        {
            case EnemyMode.Normal:
                NormalBehavior();
                break;

            case EnemyMode.Wave:
                WaveBehavior();
                break;
        }

        
    }

    void NormalBehavior()
    {
        if (isSeeing)
        {
            Chase();
        }
        else if (isClose)
        {
            //Attack();
        }
        else
        {
            //Idle();
        }
    }

    void WaveBehavior()
    {
        if (!isSeeing)
        {
            targetTransform = baseObject.transform;
        }
        Chase();
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
