using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
	public Vector2Int startLoc;
	public Faction faction;
	public List<Building> buildings = new List<Building>();

	public int pop;
	public int food;
	public int wood;
	public int stone;

	public bool grow;

	public void StartCity(Vector2Int startingLoc, Faction f)
	{
		startLoc = startingLoc;
		faction = f;
		AddBuilding(startingLoc);
	}

	void AddBuilding(Vector2Int loc)
	{
		if (Island.instance.IsBuildable(loc))
		{
			Building b = (Instantiate(Resources.Load("Homes")) as GameObject).GetComponent<Building>();
			Island.instance.occupied.Add(loc);
			buildings.Add(b);
			b.city = this;
			b.faction = faction;
			b.gridLoc = loc;
			b.transform.SetParent(transform);
			b.transform.position = Island.instance.GridToWorld(loc);
		}
		else
			Debug.Log("CAN'T BUILD HERE");
		faction.AddInfluence(loc);
	}

	void Grow()
	{
		List<Vector2Int> poss = new List<Vector2Int>();
		foreach (Building l in buildings)
		{
			foreach (Vector2Int adj in HexGrid.FindAdjacentGridLocs(l.gridLoc))
			{
				if (!poss.Contains(adj) && Island.instance.IsBuildable(adj))
					poss.Add(adj);
			}
		}
		Vector2Int loc = poss[Random.Range(0, poss.Count)];
		Debug.Log(poss.Count);
		AddBuilding(loc);
	}

	private void Update()
	{
		if(grow)
		{
			grow = false;
			Grow();
		}
	}
}
