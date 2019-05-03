using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(WorldMesh))]
public class WorldData : MonoBehaviour
{
	public static WorldData Instance;
	private void Awake(){ Instance = this; }

	public int worldSize;
	public int islandCount;
	public int maxIslandSize;
	public int minIslandSize;
	public List<IslandData> islands = new List<IslandData>(); 

	private void Start()
	{
		GenWorldData();
	}

	void GenWorldData()
	{
		List<Vector2Int> usedTiles = new List<Vector2Int>();
		for (int i = 0; i < islandCount; i++)
		{
			List<Vector2Int> tiles = new List<Vector2Int>();
			List<Vector2Int> possTiles = new List<Vector2Int>();
			
			IslandData currentIsland = new IslandData();
			islands.Add(currentIsland);
			Vector2Int loc = Vector2Int.zero;
			while (usedTiles.Contains(loc))
			{
				Vector3 spot = new Vector3(Random.Range(-worldSize, worldSize), 0, Random.Range(-worldSize, worldSize));
				loc = HexGrid.RoundToGrid(spot);
			}
			tiles.Add(loc);
			
			foreach (Vector2Int v in HexGrid.FindAdjacentGridLocs(loc))
			{
				if(!tiles.Contains(v) && !possTiles.Contains(v) && !usedTiles.Contains(v))
					possTiles.Add(v);
			}
			int islandSize = Random.Range(minIslandSize, maxIslandSize);
			while (tiles.Count < islandSize && possTiles.Count > 0)
			{
				Vector2Int tile = possTiles[Random.Range(0, possTiles.Count)];
				foreach (Vector2Int v in HexGrid.FindAdjacentGridLocs(tile))
				{
					if (!tiles.Contains(v) && !possTiles.Contains(v) && !usedTiles.Contains(v))
						possTiles.Add(v);
				}
				possTiles.Remove(tile);
				tiles.Add(tile);
			}
			foreach (Vector2Int v in FindOutline(tiles))
			{
				if(!usedTiles.Contains(v))
					tiles.Add(v);
			}
			List<Vector2Int> remove = new List<Vector2Int>();
			remove.AddRange(tiles);
			remove.AddRange(FindOutline(remove));
			remove.AddRange(FindOutline(remove));

			foreach (Vector2Int v in remove)
				usedTiles.Add(v);
			foreach (Vector2Int v in tiles)
			{
				currentIsland.tiles.Add(v, new WorldTile(v));
			}
			currentIsland.tileCount = currentIsland.tiles.Count;
		}
		Debug.Log(usedTiles.Count);
		GetComponent<WorldMesh>().GenMesh();
	}
	List<Vector2Int> FindOutline(List<Vector2Int> island)
	{
		List<Vector2Int> outline = new List<Vector2Int>();
		foreach(Vector2Int v in island)
		{
			foreach(Vector2Int adj in HexGrid.FindAdjacentGridLocs(v))
			{
				if (!island.Contains(adj) && !outline.Contains(adj))
				{
					outline.Add(adj);
				}
			}
		}
		return outline;
	}
}
