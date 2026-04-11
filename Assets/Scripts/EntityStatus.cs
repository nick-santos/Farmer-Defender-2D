using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EntityStatus : MonoBehaviour
{
    [Header("Attributes")]
    public float healthPoints;
    public float maxHealthPoints;
    public float baseDamage;
    //public Image Heart;

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
        healthPoints -= damage;

        if(healthPoints <= 0 && !isDestroyed)
        {
            WaveManager.onEnemyDestroy.Invoke();
            isDestroyed = true;

            if (transform.tag != "Player")
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.transform.tag == "Colliders/Hitbox")
        {
            Transform adversary = collision.transform.parent;
            EntityStatus adversaryStatus = adversary.GetComponent<EntityStatus>();
            TakeDamage(adversaryStatus.baseDamage);
            //healthPoints -= adversaryStatus.baseDamage;
            Debug.Log($"Damage in {transform.name}");
        }
    }   
}
