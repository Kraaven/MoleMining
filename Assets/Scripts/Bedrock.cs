using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bedrock : MonoBehaviour
{
    public Tilemap backgroundTilemap;  // Reference to the background Tilemap
    public TileBase[] backgroundTiles;  // Array of tile options (including ores)
    public int width = 100;            // Width of the Tilemap
    public int height = 100;           // Height of the Tilemap
    public float noiseScale = 0.1f;    // Scale of noise for background variation
    public float caveThreshold = 0.3f; // Threshold for cave creation
    public float caveScale = 0.1f;     // Scale of noise for caves

    [System.Serializable]
    public class Ore
    {
        public TileBase tile;
        public int baseRarity; // Rarity from 0 to 100 at the surface level
        public float rarityDecay; // How much rarity decreases per depth unit
    }

    public Ore[] ores;  // Array of ore types with their rarities

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

                // Determine tile selection based on depth and rarity
                TileBase selectedTile = SelectTileBasedOnDepthAndRarity(y);

                // Generate cave noise
                float caveNoise = Mathf.PerlinNoise(x * caveScale, y * caveScale);

                // Apply cave generation with adjusted threshold
                if (caveNoise < caveThreshold)
                {
                    backgroundTilemap.SetTile(tilePosition, null); // Remove tile for cave
                }
                else
                {
                    backgroundTilemap.SetTile(tilePosition, selectedTile);
                }
            }
        }
    }

    TileBase SelectTileBasedOnDepthAndRarity(int depth)
    {
        // Calculate total rarity weight adjusted for depth
        int totalRarity = 0;
        foreach (var ore in ores)
        {
            // Adjust rarity based on depth
            int adjustedRarity = Mathf.Max(0, ore.baseRarity - Mathf.RoundToInt(depth * ore.rarityDecay));
            totalRarity += adjustedRarity;
        }

        // Choose a random value based on total adjusted rarity weight
        int randomValue = Random.Range(0, totalRarity);
        int cumulativeRarity = 0;

        foreach (var ore in ores)
        {
            int adjustedRarity = Mathf.Max(0, ore.baseRarity - Mathf.RoundToInt(depth * ore.rarityDecay));
            cumulativeRarity += adjustedRarity;
            if (randomValue < cumulativeRarity)
            {
                return ore.tile;
            }
        }

        // Default to a background tile if no ore is selected
        float noise = Mathf.PerlinNoise(Random.value * noiseScale, Random.value * noiseScale);
        int tileIndex = Mathf.FloorToInt(noise * (backgroundTiles.Length - 1));
        tileIndex = Mathf.Clamp(tileIndex, 0, backgroundTiles.Length - 1);
        return backgroundTiles[tileIndex];
    }
}
