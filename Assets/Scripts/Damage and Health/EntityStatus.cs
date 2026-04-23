using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Team
{
    Player,
    Enemy,
    Neutral
}

public class EntityStatus : MonoBehaviour
{
    [Header("Attributes")]
    public float healthPoints;
    public float maxHealthPoints;
    //public Image Heart;

    [Header("Combat")]
    public Team team;

    private bool isDestroyed = false;

    void Start()
    {
        healthPoints = maxHealthPoints;
    }

    void Update()
    {
        // if (Heart != null)
        // {
        //     Heart.fillAmount = healthPoints / maxHealthPoints;
        // }
    }

    public void TakeDamage(float damage)
    {
        if (isDestroyed) return;

        healthPoints -= damage;

        if(healthPoints <= 0)
        {
            isDestroyed = true;

            if (transform.tag == "Enemy")
            {
                WaveManager.onEnemyDestroy.Invoke();
                if (transform.parent.gameObject != null)
                {
                    Destroy(transform.parent.gameObject);
                    return;
                }
            }
            if (transform.tag != "Player")
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        if (!collision.CompareTag("Colliders/Hitbox")) return;

        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer == null) return;

        if (damageDealer.team == this.team) return;

        TakeDamage(damageDealer.damage);
        Debug.Log($"Damage in {transform.name}");
    }   
}
