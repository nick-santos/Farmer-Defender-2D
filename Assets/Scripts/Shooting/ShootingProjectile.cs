using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingProjectile : MonoBehaviour
{
    public GameObject projectile;

    public Vector3 spawnPos;

    public bool isSeeing = false;

    private float startDelay = 2;
    private float coolDown = 2f;

    // Start is called before the first frame update
    void Start()
    {
        float spawnPosY = transform.position.y + 0.25f;
        float spawnPosX = transform.position.x + 0.5f;
        spawnPos = new Vector3(spawnPosX, spawnPosY, 0);
        InvokeRepeating("Shoot", startDelay, coolDown);
    }


    void Shoot ()
    {
        if (isSeeing)
        {
            Instantiate(projectile, spawnPos, projectile.transform.rotation);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
