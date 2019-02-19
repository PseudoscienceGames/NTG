using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
	public Dictionary<Vector2Int, int> buildings = new Dictionary<Vector2Int, int>();

	public int pop;
	public int food;
	public int wood;
	public int stone;

	public void StartCity(Vector2Int startingLoc)
	{
		AddBuilding(startingLoc, 0);
		List<Vector2Int> poss = HexGrid.FindAdjacentGridLocs(startingLoc);
		AddBuilding(poss[Random.Range(0, poss.Count)], 1);
	}

	void AddBuilding(Vector2Int loc, int building)
	{
		if (Island.instance.IsBuildable(loc))
			buildings.Add(loc, building);
		else
			Debug.Log("CAN'T BUILD HERE");
	}
}
