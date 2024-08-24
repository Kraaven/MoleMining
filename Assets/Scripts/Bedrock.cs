using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Bedrock : MonoBehaviour
{
    public Tilemap terrainTilemap;
    public TileBase[] Dirt;
    public TileBase[] Ores;

    public int worldWidth = 1000;
    public int worldHeight = 250;
    public float scale = 0.1f;
    public float noiseFreq = 0.05f;
    public float seed;
    public float oreChance = 0.1f;
    public Texture2D noiseTexture;

    public Sprite tile;
    public int WSize = 100;




    private void Start()
    {
        seed = Random.Range(-1000, 1000);
        GenerateNoiseTexture();
        GenerateBedrock();

        /*        GenerateTerrain();*/
    }

    //Make caves and ground
    public void GenerateBedrock()
    {
        for (int x = 0; x < WSize; x++)
        {

            for (int y = 0; y < WSize; y++)
            {
                if (noiseTexture.GetPixel(x, y).r < 0.6)
                {
                    if (Random.value < oreChance)
                    {
                        terrainTilemap.SetTile(new Vector3Int(x-40, y-40, 0), Ores[Random.Range(0, 2)]);
                    }
                    else
                    {
                        terrainTilemap.SetTile(new Vector3Int(x - 40, y - 40, 0), Dirt[Random.Range(0, 3)]);
                    }
                    
                }

            }
        }
    }
    //make noise map
    private void GenerateNoiseTexture()
    {
        noiseTexture = new Texture2D(worldWidth, worldHeight);

        for (int x = 0; x < noiseTexture.width; x++)
        {

            for (int y = 0; y < noiseTexture.height; y++)
            {
                float v = Mathf.PerlinNoise((x + seed) * noiseFreq, (y + seed) * noiseFreq);
                noiseTexture.SetPixel(x, y, new Color(v, v, v));
            }
        }
        noiseTexture.Apply();

    }

    /*    void GenerateTerrain()
        {
            float[] heightMap = GenerateNoiseMap(worldWidth);

            for (int x = 0; x < worldHeight; x++)
            {
                int groundHeight = Mathf.RoundToInt(Mathf.PerlinNoise(x * scale, 0) * worldWidth / 2);

                for (int y = 0; y < worldWidth; y++)
                {
                    if (y < groundHeight)
                    {
                        terrainTilemap.SetTile(new Vector3Int(x, y, 0), stoneTile);
                    }
                    else if (y == groundHeight)
                    {
                        terrainTilemap.SetTile(new Vector3Int(x, y, 0), dirtTile);
                    }
                }
            }
        }
    */
    /*    float[] GenerateNoiseMap(int width)
        {
            float[] noiseMap = new float[width];
            for(int x=0;x< width; x++)
            {
                float sampleX = x * scale;
                float noiseValue = Mathf.PerlinNoise(sampleX, 0f);
                noiseMap[x] = noiseValue*worldHeight/2;
            }
            return noiseMap;
        }*/

}
