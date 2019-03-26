using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMapData : MonoBehaviour
{
	public List<Vector2Int> gridLocs = new List<Vector2Int>();
	public int mapSize;

	private void Start()
	{
		GenMap();
	}

	void GenMap()
	{
		gridLocs.AddRange(HexGrid.FindWithinRadius(Vector2Int.zero, mapSize));
		GetComponent<BattleMapMesh>().GenMesh();
	}
}
