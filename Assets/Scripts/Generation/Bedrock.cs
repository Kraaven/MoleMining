using UnityEngine;
using UnityEngine.Tilemaps;

public class Bedrock : MonoBehaviour
{
    public Tilemap backgroundTilemap;
    public TileBase[] backgroundTiles;
    public int width = 100;
    public int height = 100;
    public float noiseScale = 0.1f;
    public float caveThreshold = 0.3f;
    public float caveScale = 0.1f;

    [System.Serializable]
    public class Ore
    {
        public TileBase tile;
        public float spawnChance; // Chance of spawning (0.0 to 1.0)
        public int minDepth; // Minimum depth at which this ore can spawn
        public int maxDepth; // Maximum depth at which this ore can spawn
    }

    public Ore[] ores;
    public TileBase defaultTile; // Default background tile

    private void Start()
    {
        GenerateBackground();
    }

    void GenerateBackground()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);

                // Generate cave noise
                float caveNoise = Mathf.PerlinNoise(x * caveScale, y * caveScale*2);

                // Apply cave generation
                if (caveNoise < caveThreshold)
                {
                    backgroundTilemap.SetTile(tilePosition, null); // Remove tile for cave
                }
                else
                {
                    TileBase selectedTile = SelectTileBasedOnDepthAndRarity(y);
                    backgroundTilemap.SetTile(tilePosition, selectedTile);
                }
            }
        }
    }

    TileBase SelectTileBasedOnDepthAndRarity(int depth)
    {
        foreach (var ore in ores)
        {
            if (depth >= ore.minDepth && depth <= ore.maxDepth)
            {
                if (Random.value < ore.spawnChance)
                {
                    if (Random.value < 0.35f)
                    {
                        return ore.tile; 
                    }
                }
            }
        }

        // If no ore is selected, use noise to select a background tile
        // float noise = Mathf.PerlinNoise(Random.value * noiseScale, Random.value * noiseScale);
        // int tileIndex = Mathf.FloorToInt(noise * (backgroundTiles.Length - 1));
        // tileIndex = Mathf.Clamp(tileIndex, 0, backgroundTiles.Length - 1);
        // return backgroundTiles[tileIndex];

        return ores[0].tile;
    }
}