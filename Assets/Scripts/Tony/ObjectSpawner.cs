using System.Collections.Generic;
using UnityEngine;

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

    // Die vorherige Position des Spielers
    private Vector3 previousPlayerPosition;

    // Liste der gespawnten Objekte
    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        // Setze die vorherige Spielerposition auf die aktuelle Spielerposition
        previousPlayerPosition = playerTransform.position;
    }

    void Update()
    {
        // Inkrementiere die Timer
        campfireTimer += Time.deltaTime;
        treeTimer += Time.deltaTime;
        stoneTimer += Time.deltaTime;

        // �berpr�fe, ob es Zeit ist, ein Lagerfeuer zu spawnen
        if (campfireTimer >= campfireSpawnInterval && spawnedObjects.Count < 50)
        {
            // Spawnen eines Lagerfeuers
            SpawnObject(campfirePrefab, playerTransform.forward);
            campfireTimer = 1f; // Zur�cksetzen des Timers
        }

        // �berpr�fe, ob es Zeit ist, einen Baum zu spawnen
        if (treeTimer >= treeSpawnInterval && spawnedObjects.Count < 50)
        {
            // Spawnen eines Baums
            SpawnObject(treePrefab, playerTransform.forward);
            treeTimer = 1f; // Zur�cksetzen des Timers
        }

        // �berpr�fe, ob es Zeit ist, einen Stein zu spawnen
        if (stoneTimer >= stoneSpawnInterval && spawnedObjects.Count < 50)
        {
            // Spawnen eines Steins
            SpawnObject(stonePrefab, playerTransform.forward);
            stoneTimer = 1f; // Zur�cksetzen des Timers
        }

        // Aktualisiere die vorherige Spielerposition
        previousPlayerPosition = playerTransform.position;
    }

    // Methode zum Spawnen eines Objekts
    void SpawnObject(GameObject prefab, Vector3 direction)
    {
        // Erh�he den Offset
        Offset++;

        // Berechne die Spawnposition basierend auf der Spielerposition und der Richtung
        Vector3 spawnPosition = playerTransform.position + new Vector3((Random.Range(0, 2) * 2 - 1) * (direction.x * 10 + Offset), 0f, direction.z * 10 + Offset);
        spawnPosition.y = 0f; // Setze die y-Koordinate auf 0, um Objekte auf dem Boden zu spawnen

        // Instanziere das Objekt an der berechneten Position
        GameObject newObject = Instantiate(prefab, spawnPosition, Quaternion.identity);

        // Zuf�llige Rotation um die x- und z-Achse
        float randomXRotation = Random.Range(0f, 360f);
        float randomZRotation = Random.Range(0f, 360f);
        newObject.transform.Rotate(randomXRotation, 0f, randomZRotation);

        // F�ge das gespawnte Objekt der Liste hinzu
        spawnedObjects.Add(newObject);

        // �berpr�fe, ob die maximale Anzahl an Objekten �berschritten wurde
        if (spawnedObjects.Count > 30)
        {
            // Zerst�re das �lteste Objekt in der Liste
            Destroy(spawnedObjects[0]);
            spawnedObjects.RemoveAt(0);
        }
    }

}
