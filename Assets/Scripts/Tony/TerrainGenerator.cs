using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    // Die Tiefe des Terrains
    public int depth = 20;

    // Die Breite und Höhe des Terrains
    public int width = 256;
    public int height = 256;

    // Die Skalierung des Terrains
    public float scale = 20f;

    // Die horizontalen und vertikalen Offset-Werte
    public float offsetX = 100f;
    public float offsetY = 100f;

    void Start()
    {
        // Setze zufällige Offset-Werte beim Start
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);
    }

    void Update()
    {
        // Holen des Terrain-Komponenten
        Terrain terrain = GetComponent<Terrain>();
        // Generiere und weise neue Terrain-Daten zu
        terrain.terrainData = GenerateTerrain(terrain.terrainData);

        // Aktualisieren des Offset-Werts über die Zeit
        //offsetX += Time.deltaTime * 5;
    }

    // Generiere die Terrain-Daten
    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        // Setze die Höhenmap-Auflösung und Größe
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);

        // Generiere die Höhen für das Terrain
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    // Generiere die Hoehen für das Terrain
    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Berechne die Höhe unter Verwendung von Perlin-Noise
                heights[x, y] = CalculateHeight(x, y);
            }
        }
        return heights;
    }

    // Berechne die Höhe unter Verwendung von Perlin-Noise
    float CalculateHeight(int x, int y)
    {
        // Berechne die Koordinaten basierend auf den Skalierungs- und Offset-Werten
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        // Gib den Perlin-Noise-Wert als Höhe zurück
        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
