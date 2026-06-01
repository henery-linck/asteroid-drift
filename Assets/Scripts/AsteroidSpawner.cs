using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab; // Prefab of the asteroid to spawn
    [SerializeField] private float initialSpawnInterval = 2f; // Time interval between spawns
    [SerializeField] private int maxAsteroids = 20;

    private float _currentSpawnInterval;
    private float _timer;

    private void Start()
    {
        _currentSpawnInterval = initialSpawnInterval;
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
        Instantiate(asteroidPrefab, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0), Quaternion.identity);
    }
}
