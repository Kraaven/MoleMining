/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Background : MonoBehaviour
{
    public Tilemap terrainTilemap;

    public TileBase[] backgroundTiles;  // Array of tile options
    public Tilemap backgroundTilemap;  // Reference to the background Tilemap
    public int width = 100;            // Width of the Tilemap
    public int height = 100;           // Height of the Tilemap
    public float noiseScale = 0.6f;    // Scale of noise for background variation




    private void Start()
    {

        GenerateBackground();
    }


    //Make caves and ground

    void GenerateBackground()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x+100 , y , 0);
                float noise = Mathf.PerlinNoise(x * noiseScale, y * noiseScale);

                // Select a tile based on noise value
                int tileIndex = Mathf.FloorToInt(noise * (backgroundTiles.Length - 1));
                tileIndex = Mathf.Clamp(tileIndex, 0, backgroundTiles.Length - 1);

                TileBase selectedTile = backgroundTiles[tileIndex];


                backgroundTilemap.SetTile(tilePosition, selectedTile);
            }
        }
    }
}
*/