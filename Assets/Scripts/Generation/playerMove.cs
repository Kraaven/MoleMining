using UnityEngine;
using UnityEngine.Tilemaps;

public class TileInteraction : MonoBehaviour
{
    public Tilemap terrainTilemap;
    public float digDistance = 2f;

    public Vector3Int GridStartPosition;


    private void Awake()
    {
        Vector3 pos = GameObject.Find("CaveGrid").transform.position;
        GridStartPosition = new Vector3Int((int)pos.x,(int)pos.y,(int)pos.z); 
      
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

        GameObject test = new GameObject();
        test.transform.position = WorldTilePosition;

        float DistanceToTile = Vector3.Distance(WorldTilePosition, transform.position);

        TileBase clickedTile = terrainTilemap.GetTile(cellPosition);




        if (terrainTilemap.HasTile(cellPosition) && DistanceToTile <= digDistance)
        {
            terrainTilemap.SetTile(cellPosition, null);

            Debug.Log("Removed tile at: " + cellPosition);
        }

        else
        {
            Debug.Log(DistanceToTile);
        }

        if (clickedTile != null)
        {
            if (clickedTile.name == "Stone")
            {
                Debug.Log("Stone" + cellPosition);
            }
            else if (clickedTile.name == "Coal")
            {
                Debug.Log("Coal" + cellPosition);
            }
            else if (clickedTile.name == "Silver")
            {
                Debug.Log("Silver" + cellPosition);
            }
            else if (clickedTile.name == "Gold")
            {
                Debug.Log("Gold" + cellPosition);
            }

        }
    }
}