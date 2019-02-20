using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
	public Vector2Int startLoc;
	public Dictionary<Vector2Int, int> buildings = new Dictionary<Vector2Int, int>();

	public int pop;
	public int food;
	public int wood;
	public int stone;

	public GameObject town;

	public bool full;

	public void StartCity(Vector2Int startingLoc)
	{
		AddBuilding(startingLoc, 0);
	}

	void AddBuilding(Vector2Int loc, int building)
	{
		if (Island.instance.IsBuildable(loc))
		{
			buildings.Add(loc, building);
			Instantiate(town, HexGrid.GridToWorld(loc, Island.instance.tiles[loc]), Quaternion.Euler(0, Random.Range(0, 360), 0));
		}
		else
			Debug.Log("CAN'T BUILD HERE");
	}

	void Grow()
	{
		List<Vector2Int> poss = new List<Vector2Int>();
		foreach (Vector2Int l in buildings.Keys)
		{
			foreach (Vector2Int adj in HexGrid.FindAdjacentGridLocs(l))
			{
				if (!poss.Contains(adj) && !buildings.ContainsKey(adj) && Island.instance.IsBuildable(adj))
					poss.Add(adj);
			}
		}
		if (poss.Count == 0)
			full = true;
		else
			AddBuilding(poss[Random.Range(0, poss.Count)], 0);
	}

	private void Update()
	{
		if (!full)
		{
			pop++;
			if (pop > buildings.Count * 100)
				Grow();
		}
	}

}
