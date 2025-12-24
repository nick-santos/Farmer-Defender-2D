using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrivalChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            Debug.Log("GAME OVER :(");
        }
    }
}
