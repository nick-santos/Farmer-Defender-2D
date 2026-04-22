using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Random=UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    //public static WaveManager main;

    [Header("References")]
    public Transform startPoint;
    public Transform baseTransform; // Center of radius
    public Camera targetCamera;
    public GameObject[] enemyPrefabs;
    public WorldTime worldTime;

    [Header("Attributes")]
    public int baseEnemies = 8;
    public float enemiesPerSecond = 0.5f;
    public float timeBetweenWaves = 5f;
    public float difficultyScalingFactor = 0.75f;

    public float minSpawnDistance = 10f; // Minimum distance from the base
    public float maxSpawnDistance = 15f; // Maximum distance from the base

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    
    void Awake()
    {
        //main = this;
        if (onEnemyDestroy == null)
        {
            onEnemyDestroy = new UnityEvent();
        }
        onEnemyDestroy.AddListener(EnemyDestroyed);

        worldTime.NightTime += OnNightTime;
    }

    void Start()
    {
        //StartCoroutine(StartWave());
    }

    void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if(timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            if (SpawnEnemy())
            {
                enemiesLeftToSpawn--;
                enemiesAlive++;
                timeSinceLastSpawn = 0f;
            }
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void OnDestroy()
    {
        worldTime.NightTime -= OnNightTime;
    }

    private void OnNightTime(object sender, EventArgs e)
    {
        Debug.Log("NIGHT");
        StartCoroutine(StartWave());
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        yield return null;
    }

    void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
    }

    bool SpawnEnemy()
    {
        // Calculate a random point in the circle
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector2 spawnPosition = (Vector2)baseTransform.position + randomDirection * randomDistance;

        GameObject prefabToSpawn = enemyPrefabs[0]; //Later will be randomized
        
        if(!IsPositionVisible(spawnPosition))
        {
            if(!Physics2D.OverlapCircle(spawnPosition, 0.5f))
            {
                Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity); // Instantiate the enemy    
                return true;
            }
        }
        
        return false; // could not spawn (overlap with other obj)

        //Instantiate(prefabToSpawn, startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    public bool IsPositionVisible(Vector3 worldPosition)
    {
        if (targetCamera == null)
        {
            Debug.LogError("Target camera not assigned!");
            return false;
        }

        // Convert world position to viewport point
        Vector3 viewportPoint = targetCamera.WorldToViewportPoint(worldPosition);

        // Check if the point is within the viewport bounds and in front of the camera
        bool inViewportWidth = viewportPoint.x >= 0f && viewportPoint.x <= 1f;
        bool inViewportHeight = viewportPoint.y >= 0f && viewportPoint.y <= 1f;
        bool inFrontOfCamera = viewportPoint.z > 0f;

        return inViewportWidth && inViewportHeight && inFrontOfCamera;
    }

}
