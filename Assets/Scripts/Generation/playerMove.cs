using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileInteraction : MonoBehaviour
{
    public Tilemap terrainTilemap;
    public float digDistance = 2f;

    public Vector3Int GridStartPosition;

    /*private Dictionary<string, float> gemWeights = new Dictionary<string, float>
    {
        // Hardcoded weights for each crystal
        { "Peridot_Crystal", 1.0f },
        { "Garnet_Crystal", 1.2f },
        { "Amber_Crystal", 0.8f },
        { "Blue_Sapphire_Crystal", 2.5f },
        { "Pink_Tourmaline_Crystal", 3.0f },
        { "Amethyst_Crystal", 1.1f },
        { "Diamond_Crystal", 5.0f }
    };*/

    private Dictionary<Material, float> gemWeights = new Dictionary<Material, float>
{
    { Material.Peridot, 35f },
    { Material.Garnet, 25f },
    { Material.Amber, 20f },
    { Material.Amethyst, 10f },
    { Material.Blue_Sapphire, 5f },
    { Material.Pink_Tormaline, 3f },
    { Material.Diamond, 2f }
};

    private List<Material> gems;
    private List<float> cumulativeProbabilities;

    private void Awake()
    {
        Vector3 pos = GameObject.Find("CaveGrid").transform.position;
        GridStartPosition = new Vector3Int((int)pos.x, (int)pos.y, (int)pos.z);

        gems = new List<Material>(gemWeights.Keys);
        cumulativeProbabilities = new List<float>(gems.Count);

        foreach (var item in gems)
        {
            print(item.ToString());
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            DetectTile();

        }
    }

    private void DetectTile()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = 0;
        Vector3Int cellPosition = terrainTilemap.WorldToCell(worldPoint);
        
        Vector3 WorldTilePosition = terrainTilemap.CellToWorld(cellPosition) + new Vector3(0.5f, 0.5f, 0.5f);

        float DistanceToTile = Vector3.Distance(WorldTilePosition, transform.position);

        TileBase clickedTile = terrainTilemap.GetTile(cellPosition);




        if (terrainTilemap.HasTile(cellPosition) && DistanceToTile <= digDistance)
        {
            terrainTilemap.SetTile(cellPosition, null);

          
        }

        else
        {
            
        }

        if (clickedTile != null && DistanceToTile <= digDistance)
        {
            if (clickedTile.name == "Stone")
            {
                
                Material gemMat = GetGem((int)terrainTilemap.CellToWorld(cellPosition).y);

                if (gemMat == Material.Default) return;
                GameObject Drop = new GameObject();
                Drop.transform.position = terrainTilemap.CellToWorld(cellPosition);
                Drop.transform.localScale *= 2f;
                Drop.AddComponent<Item>().Initialize($"{clickedTile.name}", gemMat, ItemCategory.Crystal, $"Crystal{Random.Range(1,5)}");
                print(gemMat.ToString());



            }
            else if (clickedTile.name == "Coal")
            {
               

                GameObject Drop = new GameObject();
                Drop.transform.position = terrainTilemap.CellToWorld(cellPosition);
                Drop.transform.localScale *= 2f;
                Drop.AddComponent<Item>().Initialize($"{clickedTile.name}", Material.Default, ItemCategory.Ore,"Coal");
            }
            else if (clickedTile.name == "Silver")
            {
                

                GameObject Drop = new GameObject();
                Drop.transform.position = terrainTilemap.CellToWorld(cellPosition);
                Drop.transform.localScale *= 2f;
                Drop.AddComponent<Item>().Initialize($"{clickedTile.name}", Material.Default, ItemCategory.Ore, "Silver");
            }
            else if (clickedTile.name == "Gold")
            {
                

                GameObject Drop = new GameObject();
                Drop.transform.position = terrainTilemap.CellToWorld(cellPosition);
                Drop.transform.localScale *= 2f;
                Drop.AddComponent<Item>().Initialize($"{clickedTile.name}", Material.Default, ItemCategory.Ore, "Gold");
            }

        }
    }




    public Material GetGem(int y)
    {
        // Determine if a crystal should drop (1 in 7 chance)
        if (UnityEngine.Random.Range(0, 7) != 0)
        {
            return Material.Default; // No crystal drops
        }

        float totalWeight = 0f;
        cumulativeProbabilities.Clear();

        foreach (var gem in gems)
        {
            // Adjust weight based on depth (y-value)
            // Change depthMultiplier to make the drop chance steeper for better gems
            float depthMultiplier = Mathf.Clamp01((y - 50f) / 50f); // y=50 starts affecting drops, y=100 max effect

            // Scale weights to make them more distinct and less likely at higher y values
            float adjustedWeight = gemWeights[gem] * Mathf.Pow(depthMultiplier, 3); // Cube the multiplier for a steeper drop curve

            totalWeight += adjustedWeight;
            cumulativeProbabilities.Add(totalWeight);
        }

        // Get a random value and find the corresponding gem
        float randomValue = UnityEngine.Random.value * totalWeight;

        for (int i = 0; i < cumulativeProbabilities.Count; i++)
        {
            if (randomValue <= cumulativeProbabilities[i])
            {
                return gems[i];
            }
        }

        // Fallback to the last gem if something goes wrong
        return gems[gems.Count - 1];
    }

}