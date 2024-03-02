using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int depth = 20;

    public int width = 256;
    public int height = 256;

    public float scale = 20f;

    public float offsetX = 100f;
    public float offsetY = 100f;

    // Start is called before the first frame update
    void Start()
    {
        // Set random offset values
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);
    }

    // Update is called once per frame
    void Update()
    {
        // Get the Terrain component
        Terrain terrain = GetComponent<Terrain>();
        // Generate and assign new terrain data
        terrain.terrainData = GenerateTerrain(terrain.terrainData);

        // Update the offset value over time
        offsetX += Time.deltaTime * 5;
    }

    // Generate the terrain data
    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        // Set the heightmap resolution and size
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);

        // Generate the heights for the terrain
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    // Generate the heights for the terrain
    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Calculate the height using Perlin noise
                heights[x, y] = CalculateHeight(x, y);
            }
        }
        return heights;
    }

    // Calculate the height using Perlin noise
    float CalculateHeight(int x, int y)
    {
        // Calculate the coordinates based on the scale and offset values
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        // Return the Perlin noise value as the height
        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
