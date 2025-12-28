using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    public float health;
    //public float maxHealth;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            WaveManager.onEnemyDestroy.Invoke();
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
