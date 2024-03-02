using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public Transform playerTransform; // Referenz zum Spielerobjekt
    public GameObject planePrefab; // Prefab f�r Plane
    public GameObject campfirePrefab; // Prefab f�r Campfire
    public GameObject treePrefab; // Prefab f�r Tree

    private float spawnDistanceThreshold = 20f; // Abstandsschwelle f�r das L�schen von Objekten
    private float planeSpawnInterval = 1f; // Intervall f�r das Spawnen von Planes
    private float campfireSpawnInterval = 10f; // Intervall f�r das Spawnen von Campfires
    private float treeSpawnInterval = 5f; // Intervall f�r das Spawnen von Trees

    private float planeTimer = 0f;
    private float campfireTimer = 0f;
    private float treeTimer = 0f;

    void Update()
    {
        // �berpr�fen, ob genug Zeit vergangen ist, um ein Objekt zu spawnen
        planeTimer += Time.deltaTime;
        campfireTimer += Time.deltaTime;
        treeTimer += Time.deltaTime;

        // Spawnen von Planes
        if (planeTimer >= planeSpawnInterval)
        {
            SpawnObject(planePrefab, playerTransform.forward);
            planeTimer = 0f;
        }

        // Spawnen von Campfires
        if (campfireTimer >= campfireSpawnInterval)
        {
            SpawnObject(campfirePrefab, playerTransform.forward);
            campfireTimer = 0f;
        }

        // Spawnen von Trees
        if (treeTimer >= treeSpawnInterval)
        {
            SpawnObject(treePrefab, playerTransform.forward);
            treeTimer = 0f;
        }
    }

    void SpawnObject(GameObject prefab, Vector3 direction)
    {
        // Berechnen der Position, um das Objekt zu spawnen
        Vector3 spawnPosition = playerTransform.position + direction * 10f; // Alle 10 Felder vor dem Spieler spawnen
        GameObject newObject = Instantiate(prefab, spawnPosition, Quaternion.identity);

        // �berpr�fen, ob das Objekt zu weit entfernt ist und es gegebenenfalls zerst�ren
        if (Vector3.Distance(playerTransform.position, newObject.transform.position) > spawnDistanceThreshold)
        {
            Destroy(newObject);
        }
    }
}
