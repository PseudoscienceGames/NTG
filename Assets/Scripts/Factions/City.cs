using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
	public Vector2Int startLoc;
	public Faction faction;
	public List<Building> buildings = new List<Building>();

	public int pop;

	public List<float> resCount = new List<float>();

	public void StartCity(Vector2Int startingLoc, Faction f)
	{
		pop = 10;
		for (int i = 0; i < (int)ResourceType.Count; i++)
			resCount.Add(0);
		startLoc = startingLoc; 
		faction = f;
		AddBuilding(startingLoc, BuildingType.Homes);
	}

	public void AddBuilding(Vector2Int loc, BuildingType bt)
	{
		if (Island.instance.IsBuildable(loc))
		{
			Building b = (Instantiate(Resources.Load(bt.ToString())) as GameObject).GetComponent<Building>();
			Island.instance.tiles[loc].occupants.Add(b.gameObject);
			buildings.Add(b);
			b.city = this;
			b.faction = faction;
			b.gridLoc = loc;
			b.transform.SetParent(transform);
			b.transform.position = Island.instance.GridToWorld(loc);
			b.transform.eulerAngles = new Vector3(0, Random.Range(0, 360f), 0);
			faction.AddInfluence(loc);
		}
		else
			Debug.Log("CAN'T BUILD HERE");
		
	}

	public void Work()
	{
		foreach(Building b in buildings)
		{
			b.Work();
		}
	}
}


public enum ResourceType { Food, Wood, Stone, Count }