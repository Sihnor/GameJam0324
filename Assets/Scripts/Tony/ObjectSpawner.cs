using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject campfirePrefab;
    public GameObject treePrefab;
    public GameObject stonePrefab;

    private float spawnDistanceThreshold = 20f;
    private float campfireSpawnInterval = 10f;
    private float treeSpawnInterval = 5f;
    private float stoneSpawnInterval = 8f;

    private float campfireTimer = 0f;
    private float treeTimer = 0f;
    private float stoneTimer = 0f;

    private Vector3 previousPlayerPosition;
    private float previousPlaneHeight;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        // Setzt die vorherige Spielerposition beim Start auf die aktuelle Position
        previousPlayerPosition = playerTransform.position;
    }

    void Update()
    {
        // Überprüft, ob genügend Zeit vergangen ist, um ein Objekt zu spawnen
        campfireTimer += Time.deltaTime;
        treeTimer += Time.deltaTime;
        stoneTimer += Time.deltaTime;

        // Spawnt Campfires
        if (campfireTimer >= campfireSpawnInterval && spawnedObjects.Count < 20)
        {
            SpawnObject(campfirePrefab, playerTransform.forward);
            campfireTimer = 1f;
        }

        // Spawnt Trees
        if (treeTimer >= treeSpawnInterval && spawnedObjects.Count < 20)
        {
            SpawnObject(treePrefab, playerTransform.forward);
            treeTimer = 1f;
        }

        // Spawnt Stones
        if (stoneTimer >= stoneSpawnInterval && spawnedObjects.Count < 20)
        {
            SpawnObject(stonePrefab, playerTransform.forward);
            stoneTimer = 1f;
        }

        previousPlayerPosition = playerTransform.position;
    }

    // Spawnt ein neues Objekt anhand des übergebenen Prefabs und der Richtung
    void SpawnObject(GameObject prefab, Vector3 direction)
    {
        // Berechnet die Position, um das Objekt zu spawnen
        Vector3 spawnPosition = playerTransform.position + direction * 10f;
        spawnPosition.y = previousPlaneHeight;
        GameObject newObject = Instantiate(prefab, spawnPosition, Quaternion.identity);

        // Überprüft, ob das Objekt zu weit entfernt ist und es gegebenenfalls zerstört
        if (Vector3.Distance(playerTransform.position, newObject.transform.position) > spawnDistanceThreshold)
        {
            Destroy(newObject);
        }
        else
        {
            // Fügt das Objekt der Liste hinzu
            spawnedObjects.Add(newObject);

            // Überprüft, ob die maximale Anzahl überschritten wurde und gegebenenfalls das älteste Objekt zerstört
            if (spawnedObjects.Count > 20)
            {
                Destroy(spawnedObjects[0]);
                spawnedObjects.RemoveAt(0);
            }
        }
    }
}
