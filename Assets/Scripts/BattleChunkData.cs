using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleChunkData : MonoBehaviour
{
	public 
	public WorldTile wt;
	public List<BattleTile> tiles = new List<BattleTile>();


	public void GenChunkData()
	{
		foreach (Vector2Int loc in HexGrid.FindWithinRadius(Vector2Int.zero, ChunkMath.chunkRadius))
		{
			tiles.Add(new BattleTile(loc));
		}

	}
}

[System.Serializable]
public struct BattleTile
{
	public Vector2Int gridLoc;
	public int height;
	public BattleTileType type;
	public List<Vector3> verts;
	public List<int> tris;

	public BattleTile(Vector2Int loc)
	{
		gridLoc = loc;
		height = 0;
		type = BattleTileType.Grass;
		verts = HexGrid.GetVertLocs(loc);
		tris = new List<int> { 0, 1, 2, 0, 2, 3, 0, 3, 4, 0, 4, 5 };
	}
}

public enum BattleTileType { Grass, Dirt, Sand }
