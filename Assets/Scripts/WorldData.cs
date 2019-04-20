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
	public Dictionary<Vector2Int, WorldTile> tiles = new Dictionary<Vector2Int, WorldTile>();
	public int radius;

	private void Start()
	{
		GenWorldData();
	}

	void GenWorldData()
	{
		foreach(Vector2Int loc in HexGrid.FindWithinRadius(Vector2Int.zero, radius))
		{
			tiles.Add(loc, new WorldTile(loc));
		}
		WorldMesh.Instance.GenMesh();
	}
}
