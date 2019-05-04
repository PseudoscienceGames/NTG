using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IslandData
{
	public Dictionary<Vector2Int, WorldTile> tiles = new Dictionary<Vector2Int, WorldTile>();
	public int tileCount;

	public void CalcHeights()
	{
		List<Vector3> verts = new List<Vector3>();
		foreach(WorldTile w in tiles.Values)
		{
			foreach(Vector3 v in w.verts)
			{
				here
			}
		}
	}
}
