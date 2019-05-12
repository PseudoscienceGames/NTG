using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IslandData
{
	public List<Vector2Int> gridLocs = new List<Vector2Int>();
	public List<WorldTile> tiles = new List<WorldTile>();
	public void CalcHeights()
	{
		List<Vector2Int> tempTiles = new List<Vector2Int>(gridLocs);
		List<Vector2Int> toRemove = new List<Vector2Int>();
		Dictionary<Vector2Int, int> heights = new Dictionary<Vector2Int, int>();
		float h = 0.1f;
		while(tempTiles.Count > 0)
		{
			foreach(Vector2Int loc in tempTiles)
			{
				int a = 0;
				foreach(Vector2Int adj in HexGrid.FindAdjacentGridLocs(loc))
				{
					if(tempTiles.Contains(adj))
					{
						a++;
					}
				}
				if(a < 6)
				{
					toRemove.Add(loc);
					for(int i = 0; i < 6; i++)
					{
						Vector2Int k = new Vector2Int(Mathf.RoundToInt(tiles[gridLocs.IndexOf(loc)].verts[i].x * 10), Mathf.RoundToInt(tiles[gridLocs.IndexOf(loc)].verts[i].z * 10));
						if(!heights.ContainsKey(k))
						{
							heights.Add(k, Mathf.RoundToInt(h));
						}
						else
						{
							heights[k] = Mathf.RoundToInt(h);
						}
					}
					if (h < 2)
						tiles[gridLocs.IndexOf(loc)].type = WorldTileType.Sand;
				}
			}
			h += 1f;
			foreach(Vector2Int v in toRemove)
			{
				tempTiles.Remove(v);
			}
			toRemove.Clear();
		}

		foreach (WorldTile w in tiles)
		{
			for(int i = 0; i < w.heights.Count; i++)
			{
				w.heights[i] = heights[new Vector2Int(Mathf.RoundToInt(w.verts[i].x * 10), Mathf.RoundToInt(w.verts[i].z * 10))];
			}
			w.CalcTopo();
		}
	}
}
