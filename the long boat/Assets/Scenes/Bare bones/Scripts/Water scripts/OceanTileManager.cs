using UnityEngine;
using System.Collections.Generic;

public class OceanTileManager : MonoBehaviour
{
    public Transform player;
    public GameObject waterTilePrefab;
    public int tileSize = 100;
    public int tilesVisible = 1; // 1 = 3x3 grid

    private Vector2Int currentPlayerTile;
    private Dictionary<Vector2Int, GameObject> spawnedTiles = new Dictionary<Vector2Int, GameObject>();

    void Update()
    {
        Vector2Int playerTile = new Vector2Int(
            Mathf.FloorToInt(player.position.x / tileSize),
            Mathf.FloorToInt(player.position.z / tileSize)
        );

        if (playerTile != currentPlayerTile)
        {
            currentPlayerTile = playerTile;
            UpdateTiles();
        }
    }

    void UpdateTiles()
    {
        HashSet<Vector2Int> neededTiles = new HashSet<Vector2Int>();

        for (int x = -tilesVisible; x <= tilesVisible; x++)
        {
            for (int z = -tilesVisible; z <= tilesVisible; z++)
            {
                Vector2Int tileCoord = new Vector2Int(currentPlayerTile.x + x, currentPlayerTile.y + z);
                neededTiles.Add(tileCoord);

                if (!spawnedTiles.ContainsKey(tileCoord))
                {
                    Vector3 tilePosition = new Vector3(tileCoord.x * tileSize, WaveManager.baseWaterLevel, tileCoord.y * tileSize);
                    GameObject tile = Instantiate(waterTilePrefab, tilePosition, Quaternion.identity, transform);
                    spawnedTiles.Add(tileCoord, tile);
                }
            }
        }

        // Remove old tiles
        var keys = new List<Vector2Int>(spawnedTiles.Keys);
        foreach (var tileCoord in keys)
        {
            if (!neededTiles.Contains(tileCoord))
            {
                Destroy(spawnedTiles[tileCoord]);
                spawnedTiles.Remove(tileCoord);
            }
        }
    }
}
