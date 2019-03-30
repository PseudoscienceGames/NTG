﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour
{
	public Vector2Int gridLoc;
	public List<Vector2Int> influence = new List<Vector2Int>();
	public List<City> cities = new List<City>();
	public List<Building> buildings = new List<Building>();
	public Army army;

	public void StartFaction(Vector2Int loc)
	{

		gridLoc = loc;
		AddCity(loc);
	}

	public void AddCity(Vector2Int loc)
	{
		Vector3 worldLoc = Island.instance.GridToWorld(loc);
		City city = (Instantiate(Resources.Load("City"), worldLoc, Quaternion.identity) as GameObject).GetComponent<City>();
		city.transform.SetParent(transform);
		cities.Add(city);
		city.StartCity(loc, this);
	}
	public void AddBuilding(Vector2Int loc)
	{

	}
	public void AddInfluence(Vector2Int loc)
	{
		if (!influence.Contains(loc))		
			influence.Add(loc);
		foreach (Vector2Int adj in HexGrid.FindAdjacentGridLocs(loc))
		{
			if(!influence.Contains(adj))
			{
				influence.Add(adj);
			}
		}
		UpdateInfluence();
	}
	public void UpdateInfluence()
	{
		GetComponent<InfluenceMesh>().DrawInfluence(influence);
	}

	public void Work()
	{
		foreach(City c in cities)
		{
			c.Work();
		}
	}

	public void AddSquad()
	{
		Instantiate(Resources.Load("Unit"));
	}
}