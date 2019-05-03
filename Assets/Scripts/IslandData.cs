using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IslandData
{
	public Dictionary<Vector2Int, WorldTile> tiles = new Dictionary<Vector2Int, WorldTile>();
	public int tileCount;

}
