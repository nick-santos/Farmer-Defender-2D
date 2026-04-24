using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Instant,
    OverTime
}

public class DamageDealer : MonoBehaviour
{
    [Header("Damage")]
    public float damage;

    [Header("Damage")]
    public Team team;

    [Header("Damage Type")]
    public DamageType damageType = DamageType.Instant;
    public float damageInterval = 1f;
}
