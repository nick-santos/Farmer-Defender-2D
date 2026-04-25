using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    public float lifetime = 1f;

    void Start()
    {
        Vector3 randomOffset = new Vector3(
            Random.Range(-0.3f, 0.3f),
            Random.Range(-0.2f, 0.2f),
            0f
        );

        transform.position += randomOffset;
        Destroy(gameObject, lifetime);
    }
}
