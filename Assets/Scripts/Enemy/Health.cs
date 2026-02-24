using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    public float health;
    //public float maxHealth;

    private bool isDestroyed = false;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0 && !isDestroyed)
        {
            WaveManager.onEnemyDestroy.Invoke();
            isDestroyed = true;
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
