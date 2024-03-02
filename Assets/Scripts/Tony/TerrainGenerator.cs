using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    // Die Tiefe des Terrains
    public int depth = 20;

    // Die Breite und H�he des Terrains
    public int width = 256;
    public int height = 256;

    // Die Skalierung des Terrains
    public float scale = 20f;

    // Die horizontalen und vertikalen Offset-Werte
    public float offsetX = 100f;
    public float offsetY = 100f;

    void Start()
    {
        // Setze zuf�llige Offset-Werte beim Start
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);
    }

    void Update()
    {
        // Holen des Terrain-Komponenten
        Terrain terrain = GetComponent<Terrain>();
        // Generiere und weise neue Terrain-Daten zu
        terrain.terrainData = GenerateTerrain(terrain.terrainData);

        // Aktualisieren des Offset-Werts �ber die Zeit
        //offsetX += Time.deltaTime * 5;
    }

    // Generiere die Terrain-Daten
    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        // Setze die H�henmap-Aufl�sung und Gr��e
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);

        // Generiere die H�hen f�r das Terrain
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    // Generiere die Hoehen f�r das Terrain
    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Berechne die H�he unter Verwendung von Perlin-Noise
                heights[x, y] = CalculateHeight(x, y);
            }
        }
        return heights;
    }

    // Berechne die H�he unter Verwendung von Perlin-Noise
    float CalculateHeight(int x, int y)
    {
        // Berechne die Koordinaten basierend auf den Skalierungs- und Offset-Werten
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        // Gib den Perlin-Noise-Wert als H�he zur�ck
        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
