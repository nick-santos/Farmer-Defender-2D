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

    [Header("Visual Effects")]
    public GameObject healEffectPrefab;
    public GameObject damageEffectPrefab;
    public Transform effectSpawnPoint;

    private bool isDestroyed = false;

    // Individual Timer for overtime damage from each attacker
    private Dictionary<DamageDealer, float> damageTimers = new Dictionary<DamageDealer, float>();

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

    void SpawnEffect(GameObject effectPrefab)
    {
        if (effectPrefab == null) return;

        if (effectSpawnPoint == null) return;

        Instantiate(effectPrefab, effectSpawnPoint.position, Quaternion.identity);
    }

    public void Heal(float amount)
    {
        if (isDestroyed) return;

        healthPoints += amount;
        healthPoints = Mathf.Clamp(healthPoints, 0, maxHealthPoints);

        SpawnEffect(healEffectPrefab);
    }

    public void TakeDamage(float damage)
    {
        if (isDestroyed) return;

        healthPoints -= damage;

        SpawnEffect(damageEffectPrefab);

        //Debug.Log($"{transform.name} recebeu {damage} de dano. Vida restante: {healthPoints}");

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

    //-----------------//
    // INSTANT DAMAGE //

    void OnTriggerEnter2D (Collider2D collision)
    {
        if (!collision.CompareTag("Colliders/Hitbox")) return;

        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer == null) return;

        if (damageDealer.team == this.team) return;

        if (damageDealer.damageType != DamageType.Instant) return;

        TakeDamage(damageDealer.damage);
        Debug.Log($"Instant Damage in {transform.name}");
    }   

    //-----------------//
    // OVERTIME DAMAGE //

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Colliders/Hitbox")) return;

        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer == null) return;

        // deal with teams
        if (damageDealer.team == this.team) return;

        // overtime damage
        if (damageDealer.damageType != DamageType.OverTime) return;

        // creates individual timer
        if (!damageTimers.ContainsKey(damageDealer))
        {
            damageTimers.Add(damageDealer, 0f);
        }

        // Add time in all timers
        damageTimers[damageDealer] += Time.deltaTime;

        if (damageTimers[damageDealer] >= damageDealer.damageInterval)
        {
            TakeDamage(damageDealer.damage);
            damageTimers[damageDealer] = 0f;

            Debug.Log($"Damage Over Time in {transform.name}");
        }
    }

    //-----------------//
    // EXIT CONTACT //

    private void OnTriggerExit2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer != null && damageTimers.ContainsKey(damageDealer))
        {
            damageTimers.Remove(damageDealer);
        }
    }
}
