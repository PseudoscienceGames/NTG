using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManager : MonoBehaviour
{
	public int cityCount;
	public GameObject cityPrefab;
	public List<Transform> cities = new List<Transform>();

	private void Start()
	{
		Invoke("AddCities", 1);
	}

	void AddCities()
	{
		while (cities.Count < cityCount)
		{
			Transform currentCity = (Instantiate(cityPrefab) as GameObject).transform;
			
			Vector2Int loc = Island.instance.RandomGridLoc();
			while (!Island.instance.IsBuildable(loc))
				loc = Island.instance.RandomGridLoc();
			currentCity.transform.position = HexGrid.GridToWorld(loc, Island.instance.tiles[loc]);
			currentCity.GetComponent<City>().StartCity(loc);
			currentCity.GetComponent<City>().startLoc = loc;
			cities.Add(currentCity);

		}
	}
}
