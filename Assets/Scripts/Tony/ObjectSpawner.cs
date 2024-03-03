using System.Collections.Generic;
using UnityEngine;

enum ESpawnableType
{
    Campfire,
    Tree,
    Stone
}

public class ObjectSpawner : MonoBehaviour
{
    // Referenz auf den Spieler-Transform
    public Transform playerTransform;

    // Prefabs f�r die zu spawnenden Objekte
    public GameObject campfirePrefab;
    public GameObject treePrefab;
    public GameObject stonePrefab;

    // Distanzschwellwert f�r das Spawnen von Objekten
    private float spawnDistanceThreshold = 20f;

    // Intervalle f�r das Spawnen von Campfires, B�umen und Steinen
    private float campfireSpawnInterval = 10f;
    private float treeSpawnInterval = 5f;
    private float stoneSpawnInterval = 8f;

    // Timer f�r die Intervalle
    private float campfireTimer = 0f;
    private float treeTimer = 0f;
    private float stoneTimer = 0f;

    private int Offset = 0;

    System.Random RandomPosition = new System.Random();

    // Liste der gespawnten Objekte
    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        Instantiate(this.campfirePrefab, transform.position + new Vector3(2, 0, 0), Quaternion.Euler(0, 0, 0));
        Instantiate(this.treePrefab, transform.position + new Vector3(0, 0, 1), Quaternion.Euler(0, 0, 0));
        Instantiate(this.stonePrefab, transform.position + new Vector3(1, 0, 1), Quaternion.Euler(0, 0, 0));
    }

    void Update()
    {
        campfireTimer += Time.deltaTime;
        treeTimer += Time.deltaTime;
        stoneTimer += Time.deltaTime;

        if (campfireTimer >= campfireSpawnInterval && spawnedObjects.Count < 50)
        {
            SpawnObject(campfirePrefab, playerTransform.forward, ESpawnableType.Campfire );
            campfireTimer = 1f; // Zur�cksetzen des Timers
        }

        if (treeTimer >= treeSpawnInterval && spawnedObjects.Count < 50)
        {
            SpawnObject(treePrefab, playerTransform.forward, ESpawnableType.Tree);
            treeTimer = 1f; // Zur�cksetzen des Timers
        }

        if (stoneTimer >= stoneSpawnInterval && spawnedObjects.Count < 50)
        {
            SpawnObject(stonePrefab, playerTransform.forward, ESpawnableType.Stone);
            stoneTimer = 1f; // Zur�cksetzen des Timers
        }
    }

    void SpawnObject(GameObject prefab, Vector3 direction, ESpawnableType type)
    {
        if (type == ESpawnableType.Campfire) Offset++;

        int random = RandomPosition.Next(-10, 11); 
        Vector3 spawnPosition = this.playerTransform.position + this.playerTransform.forward * (10 + this.Offset) + this.playerTransform.right * random;
        spawnPosition.y = 0;
        
        GameObject newObject = Instantiate(prefab, spawnPosition, Quaternion.identity);

        float randomXRotation = Random.Range(0f, 360f);
        float randomZRotation = Random.Range(0f, 360f);
        if(type == ESpawnableType.Stone) newObject.transform.Rotate(randomXRotation, 0f, randomZRotation);

        spawnedObjects.Add(newObject);

        if (spawnedObjects.Count > 30)
        {
            Destroy(spawnedObjects[0]);
            spawnedObjects.RemoveAt(0);
        }
    }

}
