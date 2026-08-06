using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
    [Serializable]
    public struct WeightedAsteroid
    {
        public GameObject asteroidPrefab; // Prefab of the asteroid
        [Range(1, 100)]
        public int weight; // Weight for spawning this asteroid
    }
    
    [Header("Configuração dos Asteroides")]
    [SerializeField] private List<WeightedAsteroid> asteroids; // Array of weighted asteroids
    [SerializeField] private float initialSpawnInterval = 2f; // Time interval between spawns
    [SerializeField] private int maxAsteroids = 20;

    private float _currentSpawnInterval;
    private float _timer;
    private int _totalWeight;

    private void Start()
    {
        _currentSpawnInterval = initialSpawnInterval;

        // Calculate the total weight of all asteroids
        calculateTotalWeight();
    }

    private void calculateTotalWeight()
    {
        _totalWeight = 0;
        foreach (var asteroid in asteroids)
        {
            _totalWeight += asteroid.weight;
        }
    }

    public GameObject GetRandomAsteroidPrefab()
    {
        if (asteroids == null || asteroids.Count == 0) return null;

        int randomValue = Random.Range(0, _totalWeight);
        int currentSum = 0;

        foreach (var item in asteroids)
        {
            currentSum += item.weight;
            if (randomValue < currentSum)
            {
                return item.asteroidPrefab;
            }
        }

        return asteroids[0].asteroidPrefab; // Fallback de segurança
    }

    // Update is called once per frame
    private void Update()
    {
        // For now spawn an asteroid every spawnInterval seconds
        _timer += Time.deltaTime;
        if (_timer >= _currentSpawnInterval)
        {
            if (GameObject.FindGameObjectsWithTag("Asteroid").Length < maxAsteroids)
                SpawnAsteroid();

            _timer = 0f;
        }
    }

    private void SpawnAsteroid()
    {
        // Random spawn area for the asteroid
        var spawnArea = (SpawnArea)Random.Range(0, Enum.GetValues(typeof(SpawnArea)).Length - 1);
        float minX = 0, maxX = 0, minY = 0, maxY = 0;

        // Set spawn position based on the chosen spawn area
        switch (spawnArea)
        {
            case SpawnArea.Top:
                minX = -9.5f;
                maxX = 9.5f;
                minY = 5.5f;
                maxY = 5.5f;
                break;
            case SpawnArea.Bottom:
                minX = -9.5f;
                maxX = 9.5f;
                minY = -5.5f;
                maxY = -5.5f;
                break;
            case SpawnArea.Left:
                minX = -9.5f;
                maxX = -9.5f;
                minY = -5.5f;
                maxY = 5.5f;
                break;
            case SpawnArea.Right:
                minX = 9.5f;
                maxX = 9.5f;
                minY = -5.5f;
                maxY = 5.5f;
                break;
            default:
                break;
        }

        // Instantiate the asteroid at a random position within the chosen spawn area
        Instantiate(GetRandomAsteroidPrefab(), new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0), Quaternion.identity);
    }
}
