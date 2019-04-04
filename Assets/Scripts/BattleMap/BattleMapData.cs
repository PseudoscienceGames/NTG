using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMapData : MonoBehaviour
{
	public static BattleMapData Instance;
	void Awake() { Instance = this; }
	public List<Vector2Int> gridLocs = new List<Vector2Int>();
	public List<Vector2Int> occupied = new List<Vector2Int>();
	public int mapSize;
	public int obstacleCount;
	private void Start()
	{
		GenMap();
		AddObstacles();
	}

	void GenMap()
	{
		gridLocs.AddRange(HexGrid.FindWithinRadius(Vector2Int.zero, mapSize));
		GetComponent<BattleMapMesh>().GenMesh();
	}
	void AddObstacles()
	{
		List<Vector2Int> left = new List<Vector2Int>(gridLocs);
		for (int i = 0; i < obstacleCount; i++)
		{
			Vector2Int loc = left[Random.Range(0, left.Count)];
			occupied.Add(loc);
			Instantiate(Resources.Load("Obstacle"), HexGrid.GridToWorld(loc), Quaternion.identity);
			left.Remove(loc);
		}
	}
}
