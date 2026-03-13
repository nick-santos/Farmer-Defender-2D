using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    //public static WaveManager main;

    [Header("References")]
    public Transform startPoint;
    public Transform baseTransform; // Center of radius
    public GameObject[] enemyPrefabs;

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
    }

    void Start()
    {
        StartCoroutine(StartWave());
    }

    void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if(timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    void SpawnEnemy()
    {
        // Calculate a random point in the circle
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector2 spawnPosition = (Vector2)baseTransform.position + randomDirection * randomDistance;

        GameObject prefabToSpawn = enemyPrefabs[0]; //Later will be randomized
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity); // Instantiate the enemy
        
        //Instantiate(prefabToSpawn, startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

}
