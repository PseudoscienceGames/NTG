using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(BattleMapMesh))]
public class BattleMapData : MonoBehaviour
{
	public static BattleMapData Instance;
	private void Awake() { Instance = this; }
	public Dictionary<Vector2Int, BattleMapTile> tiles = new Dictionary<Vector2Int, BattleMapTile>();
	public int radius;

	public void GenMapData(WorldTile wt)
	{
		foreach (Vector2Int loc in HexGrid.FindWithinRadius(Vector2Int.zero, radius))
		{
			tiles.Add(loc, new BattleMapTile(loc));
		}
		GetComponent<BattleMapMesh>().GenMesh();
	}
}

